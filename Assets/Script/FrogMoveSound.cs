using UnityEngine;

public class FrogMoveSound : MonoBehaviour
{
    public Joystick joystick;          // ลากตัว Joystick มาวางใน Inspector
    public AudioSource walkSound;      // ลาก AudioSource ที่มีเสียงเดิน

    public float threshold = 0.1f;     // ความแรงที่ถือว่าเริ่มเดิน

    void Update()
    {
        Vector2 moveInput = new Vector2(joystick.Horizontal, joystick.Vertical);

        if (moveInput.magnitude > threshold)
        {
            if (!walkSound.isPlaying)
            {
                walkSound.Play();
            }
        }
        else
        {
            if (walkSound.isPlaying)
            {
                walkSound.Stop();
            }
        }
    }
}