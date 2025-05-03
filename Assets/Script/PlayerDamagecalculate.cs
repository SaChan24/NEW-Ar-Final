using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //หาตัว HealthSystem ตัวแรกที่มีอยู่ในฉาก
        //ถ้าไม่เจอ HealthSystem ก็หยุดการทำงานของฟังก์ชัน(เพื่อกัน Error)
        HealthSystem health = FindFirstObjectByType<HealthSystem>();
        if (health == null) return;

        // ถ้าโดน damage
        if (other.CompareTag("damage 1 heart"))
        {
            //ปรับดามเมจ
            health.TakeDamage(1);
            Debug.Log("อันตราย! หัวใจลด");


            Destroy(other.gameObject); // ลบวัตถุที่ทำดาเมจ
        }
        // ถ้าโดน heal
        else if (other.CompareTag("Heal"))
        {
            //ปรับฮิล
            health.AddHealth(1);
            
            Debug.Log("ได้หัวใจเพิ่ม!");
            

            Destroy(other.gameObject); // ลบวัตถุที่ทำดาเมจ
        }
        // ถ้าโดน damage จาก Bullet
        if (other.CompareTag("Bullet"))
        {
            //ปรับดามเมจ
            health.TakeDamage(1);
            Debug.Log("อันตราย! หัวใจลด");


            Destroy(other.gameObject); // ลบวัตถุที่ทำดาเมจ
        }
    }
}

