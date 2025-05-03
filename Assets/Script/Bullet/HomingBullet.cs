using UnityEngine;

public class HomingBullet : MonoBehaviour
{
    public float speed = 10f;         // �������Ǣͧ����ع
    public float homingDuration = 1f; // �������ҷ�����ع�����Ҽ�����
    public float maxLifeTime = 3f;    // �����٧�ش������ع�������͹����µ���ͧ
    public GameObject target;         // ����������������·�����ع�о�����

    private bool homing = true;       // ������ع�����Ҽ�����
    private float homingTime;         // ��������������Ҽ�����
    private float lifeTime;           // ����㹡���ժ��Ե�ͧ����ع
    private Vector3 lastDirection;    // ��ȷҧ����ش������ع���ѧ����͹����

    void Start()
    {
        // ������鹷������ homing
        homingTime = Time.time;
        lifeTime = Time.time + maxLifeTime;

        if (target == null)
        {
            // �����辺 target ����Ҽ�����
            target = GameObject.FindWithTag("Player");
        }

        // ������鹷�ȷҧ����ش
        lastDirection = transform.forward;
    }
    void OnTriggerEnter(Collider other)
    {
        // ������µ���ͧ�ҡ���Ѻ ShootArea
        if (other.CompareTag("ShootArea")) return;

        Destroy(gameObject);  // ����µ���ͧ��ѧ�ҡ��
    }

    void OnCollisionEnter(Collision collision)
    {
        // ������µ���ͧ�ҡ���Ѻ ShootArea
        if (collision.gameObject.CompareTag("ShootArea")) return;


        Destroy(gameObject);  // ����µ���ͧ��ѧ�ҡ��
    }

    void Update()
    {
        if (homing && target != null)
        {
            // �ӹǳ����㹡�������Ҽ�����
            float t = (Time.time - homingTime) / homingDuration;

            // ��Ҽ�ҹ���� homingDuration ���� ����ع�о��仵����ȷҧ
            if (t >= 1f)
            {
                homing = false;
                t = 1f; // ���������
            }

            // �ӹǳ��ȷҧ������ع����͹�����Ҽ�����
            Vector3 direction = (target.transform.position - transform.position).normalized;
            lastDirection = direction;  // �纷�ȷҧ����ش������ع����͹���

            // �� MoveTowards �������������͹���ͧ����ع����� ������Ҽ�����
            transform.position = Vector3.MoveTowards(transform.position, target.transform.position, speed * Time.deltaTime);
        }
        else
        {
            // ��ѧ�ҡ������� homing ����ع�о���㹷�ȷҧ����ش����ѹ����͹���
            transform.Translate(lastDirection * speed * Time.deltaTime);
        }

        // ����ҡ���ع����������������ѧ
        if (Time.time >= lifeTime)
        {
            Destroy(gameObject);  // ����µ���ͧ
        }
    }
}
