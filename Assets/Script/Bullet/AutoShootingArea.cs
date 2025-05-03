using UnityEngine;

public class AutoShootingArea : MonoBehaviour
{
    public GameObject bulletPrefab;    // ����ع�����Ҩ��ԧ
    public Transform[] firePoints;     // ���˹觷�����ع�ж١�ԧ�͡�� (����ö��˹����¨ش��)
    public float fireRate = 2f;        // �ѵ�ҡ���ԧ����ع (�ԧ�ء� 2 �Թҷ�)

    private bool isPlayerInArea = false;  // ����� Player ����㹾�鹷���������
    private float nextFireTime = 0f;

    void Update()
    {
        if (isPlayerInArea && Time.time >= nextFireTime)
        {
            FireBullet();
            nextFireTime = Time.time + 1f / fireRate;
        }
    }

    // ����� Player �Թ�����㹾�鹷���������
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))  // ������� Player
        {
            isPlayerInArea = true;   // ��駤����Ҽ���������㹾�鹷������
        }
    }

    // ����� Player �͡�ҡ��鹷��
    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))  // ������� Player
        {
            isPlayerInArea = false;  // ��駤����Ҽ������͡�ҡ��鹷������
        }
    }

    void FireBullet()
    {
        if (bulletPrefab != null && firePoints != null && firePoints.Length > 0)
        {
            // ���ҧ����ع�ҡ���˹觢ͧ firePoints
            foreach (Transform firePoint in firePoints)
            {
                // ���ҧ����ع�ҡ���˹觷���˹�� firePoint
                GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
                bullet.GetComponent<HomingBullet>().target = GameObject.FindWithTag("Player"); // ��駤�� target �� Player
            }
        }
    }
}
