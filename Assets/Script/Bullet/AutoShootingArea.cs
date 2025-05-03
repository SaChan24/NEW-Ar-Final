using UnityEngine;

public class AutoShootingArea : MonoBehaviour
{
    public GameObject bulletPrefab;    // กระสุนที่เราจะยิง
    public Transform[] firePoints;     // ตำแหน่งที่กระสุนจะถูกยิงออกมา (สามารถกำหนดหลายจุดได้)
    public float fireRate = 2f;        // อัตราการยิงกระสุน (ยิงทุกๆ 2 วินาที)

    private bool isPlayerInArea = false;  // เช็คว่า Player อยู่ในพื้นที่หรือไม่
    private float nextFireTime = 0f;

    void Update()
    {
        if (isPlayerInArea && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    // เช็คว่า Player เดินเข้ามาในพื้นที่หรือไม่
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // เช็คว่าเป็น Player
        {
            isPlayerInArea = true;   // ตั้งค่าว่าผู้เล่นอยู่ในพื้นที่แล้ว
        }
    }

    // เช็คว่า Player ออกจากพื้นที่
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // เช็คว่าเป็น Player
        {
            isPlayerInArea = false;  // ตั้งค่าว่าผู้เล่นออกจากพื้นที่แล้ว
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && firePoints != null && firePoints.Length > 0)
        {
            // สร้างกระสุนจากตำแหน่งของ firePoints
            foreach (Transform firePoint in firePoints)
            {
                // สร้างกระสุนจากตำแหน่งที่กำหนดใน firePoint
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<HomingBullet>().target = GameObject.FindWithTag("Player"); // ตั้งค่า target เป็น Player
            }
        }
    }
}
