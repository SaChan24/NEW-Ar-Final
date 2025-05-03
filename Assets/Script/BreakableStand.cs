using UnityEngine;
using System.Collections;

public class BreakablePillar : MonoBehaviour
{
    public int hitsToBreak = 3; // จำนวนครั้งที่ต้องชนถึงจะพัง
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
        // ตรวจสอบว่าเป็น Player และ Player กำลัง Dash อยู่
        PlayerController player = collision.gameObject.GetComponent<PlayerController>();
        if (player != null && player.IsDashing())
        {
            currentHits++;

            Debug.Log($"ชนเสา! ตอนนี้โดนไปแล้ว {currentHits} ครั้ง");

            // เริ่มสั่น
            StartCoroutine(Shake());




            if (currentHits >= hitsToBreak)
            {
                Destroy(gameObject); // ทำลายเสา
                Debug.Log("เสาพังแล้ว!");
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

        transform.position = originalPosition; // กลับที่เดิม
    }
}
