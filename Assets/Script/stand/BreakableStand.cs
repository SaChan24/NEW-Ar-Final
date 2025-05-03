using UnityEngine;
using System.Collections;

public class BreakableStand : MonoBehaviour
{
    public int hitsToBreak = 3; // �ӹǹ���駷���ͧ���֧�оѧ
    private int currentHits = 0;


    public float shakeDuration = 0.2f;
    public float shakeMagnitude = 0.1f;

    private Vector3 originalPosition;


    private void Start()
    {
        originalPosition = transform.position;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // ��Ǩ�ͺ����� Player ��� Player ���ѧ Dash ����
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsDashing())
        {
            currentHits++;

            Debug.Log($"�����! �͹���ⴹ����� {currentHits} ����");

            // ��������
            StartCoroutine(Shake());

                if (currentHits >= hitsToBreak)
                {
                    Debug.Log("��Ҿѧ����!");
                    PillarManager.Instance.PillarDestroyed(); // �� PillarManager
                    Destroy(gameObject);
                }
        }
    }

    IEnumerator Shake()
    {
        float elapsed = 0f;

        while (elapsed < shakeDuration)
        {
            Vector3 randomOffset = Random.insideUnitSphere * shakeMagnitude;
            transform.position = originalPosition + randomOffset;

            elapsed += Time.deltaTime;
            yield return null;
        }

        transform.position = originalPosition; // ��Ѻ������
    }
}
