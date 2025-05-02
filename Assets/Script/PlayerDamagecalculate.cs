using UnityEngine;

public class PlayerTriggerHandler : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        //�ҵ�� HealthSystem ����á���������㹩ҡ
        //�������� HealthSystem ����ش��÷ӧҹ�ͧ�ѧ��ѹ(���͡ѹ Error)
        HealthSystem health = FindFirstObjectByType<HealthSystem>();
        if (health == null) return;

        // ���ⴹ damage
        if (other.CompareTag("damage 1 heart"))
        {
            //��Ѻ������
            health.TakeDamage(1);
            Debug.Log("�ѹ����! ����Ŵ");
        }
        // ���ⴹ heal
        else if (other.CompareTag("Heal"))
        {
            //��Ѻ���
            health.AddHealth(1);
            Debug.Log("����������!");
        }
    }
}

