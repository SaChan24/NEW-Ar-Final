using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 10f;         // ความเร็วของกระสุน
    public float homingDuration = 1f; // ระยะเวลาที่กระสุนลอยไปหาผู้เล่น
    public float maxLifeTime = 3f;    // เวลาสูงสุดที่กระสุนจะอยู่ก่อนทำลายตัวเอง
    public GameObject target;         // ผู้เล่นหรือเป้าหมายที่กระสุนจะพุ่งไปหา

    private bool homing = true;       // ให้กระสุนลอยไปหาผู้เล่น
    private float homingTime;         // เวลาเริ่มลอยไปหาผู้เล่น
    private float lifeTime;           // เวลาในการมีชีวิตของกระสุน
    private Vector3 lastDirection;    // ทิศทางล่าสุดที่กระสุนกำลังเคลื่อนที่ไป

    void Start()
    {
        // เริ่มต้นที่เวลา homing
        homingTime = Time.time;
        lifeTime = Time.time + maxLifeTime;

        if (target == null)
        {
            // ถ้าไม่พบ target ให้หาผู้เล่น
            target = GameObject.FindWithTag("Player");
        }

        // เริ่มต้นทิศทางล่าสุด
        lastDirection = transform.forward;
    }
    void OnTriggerEnter(Collider other)
    {
        // ไม่ทำลายตัวเองหากชนกับ ShootArea
        if (other.CompareTag("ShootArea")) return;

        Destroy(gameObject);  // ทำลายตัวเองหลังจากชน
    }

    void OnCollisionEnter(Collision collision)
    {
        // ไม่ทำลายตัวเองหากชนกับ ShootArea
        if (collision.gameObject.CompareTag("ShootArea")) return;


        Destroy(gameObject);  // ทำลายตัวเองหลังจากชน
    }

    void Update()
    {
        if (homing && target != null)
        {
            // คำนวณเวลาในการลอยไปหาผู้เล่น
            float t = (Time.time - homingTime) / homingDuration;

            // ถ้าผ่านเวลา homingDuration แล้ว กระสุนจะพุ่งไปตามทิศทาง
            if (t >= 1f)
            {
                homing = false;
                t = 1f; // เริ่มพุ่งไป
            }

            // คำนวณทิศทางที่กระสุนเคลื่อนที่ไปหาผู้เล่น
            Vector3 direction = (target.transform.position - transform.position).normalized;
            lastDirection = direction;  // เก็บทิศทางล่าสุดที่กระสุนเคลื่อนที่

            // ใช้ MoveTowards เพื่อให้การเคลื่อนที่ของกระสุนค่อยๆ เข้ามาหาผู้เล่น
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            // หลังจากหมดเวลา homing กระสุนจะพุ่งไปในทิศทางล่าสุดที่มันเคลื่อนที่
            transform.Translate(lastDirection * speed * Time.deltaTime);
        }

        // เช็คว่ากระสุนหมดเวลาแล้วหรือยัง
        if (Time.time >= lifeTime)
        {
            Destroy(gameObject);  // ทำลายตัวเอง
        }
    }
}
