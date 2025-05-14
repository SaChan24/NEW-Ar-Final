using UnityEngine;
using UnityEngine.UI; // ใช้สำหรับจัดการ UI เช่น ปุ่ม (แต่ในคลาสนี้ยังไม่ได้ใช้)

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 2.0f; // ความเร็วการเคลื่อนที่ของ Player

    private Joystick joystick; // ตัวควบคุม Joystick (FixedJoystick จะหาแบบอัตโนมัติจาก Scene)

    // สำหรับการกระโดด
    public float jumpForce = 10f; // แรงกระโดด
    private Rigidbody rb; // อ้างอิง Rigidbody ของ Player
    private bool isGrounded = true; // ตรวจสอบว่าอยู่บนพื้นไหม (เพื่อไม่ให้กระโดดลอย)


    //Dash พุ่งตัว
    private bool isDashing = false;
    public float dashForce = 10f;     // แรงในการ Dash
    public float dashCooldown = 1f;   // เวลา cooldown ระหว่าง dash
    private bool canDash = true;      // เอาไว้เช็คว่า dash ได้หรือยัง

    private void Start()
    {
        // ค้นหา Rigidbody บนตัวเอง (Player)
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody not found on PlayerController!"); // แจ้งเตือนถ้าไม่มี Rigidbody
        }

        // ค้นหา FixedJoystick ที่อยู่ใน Scene
        joystick = FindObjectOfType<FixedJoystick>();
        if (joystick == null)
        {
            Debug.LogError("FixedJoystick not found in the scene!"); // แจ้งเตือนถ้าไม่มี Joystick
        }
    }

    private void Update()
    {
        if (joystick != null)
        {
            // สร้างทิศทางจากการเลื่อน Joystick
            Vector3 direction = new Vector3(joystick.Horizontal, 0, joystick.Vertical);

            // ขยับตำแหน่ง Player ตามทิศทางและความเร็ว
            transform.position += direction * moveSpeed * Time.deltaTime;

            // หมุนตัว Player ไปในทิศทางที่เคลื่อนที่
            if (direction != Vector3.zero)
            {
                transform.forward = direction;
            }
        }
    }

    public void Jump()
    {
        // กระโดดเฉพาะตอนอยู่บนพื้น
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse); // ใส่แรงพุ่งขึ้น
            isGrounded = false; // ขณะอยู่กลางอากาศจะไม่สามารถกระโดดซ้ำได้
        }
    }

    public void Dash()
    {
        if (!canDash || rb == null) return;
        isDashing = true; // เริ่ม dash
        // พุ่งไปข้างหน้าตามที่หันอยู่
        rb.AddForce(transform.forward * dashForce, ForceMode.Impulse);

        canDash = false;
        Invoke(nameof(ResetDash), dashCooldown); // เรียกให้ dash ได้อีกครั้งหลังจาก cooldown
        Invoke(nameof(EndDash), 0.5f); // Dash นาน 0.2 วินาที
    }

    void ResetDash()
    {
        canDash = true;
    }
    void EndDash()
    {
        isDashing = false;
    }
    public bool IsDashing()
    {
        return isDashing;
    }

    void OnCollisionEnter(Collision other)
    {
        // ถ้าสัมผัสกับวัตถุที่มีแท็ก "Ground" ให้กระโดดได้อีกครั้ง
        if (other.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
