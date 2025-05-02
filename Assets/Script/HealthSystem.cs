using UnityEngine;
using UnityEngine.UI;

public class HealthSystem : MonoBehaviour
{
    public int maxHealth = 3;
    private int currentHealth;

    [SerializeField] private Image[] hearts;
    [SerializeField] private Sprite fullHeart;
    [SerializeField] private Sprite emptyHeart;

    [SerializeField] private UIGameManager uiGameManager; // <- ลากจาก Inspector
    [SerializeField] private GameObject youDeadPanel;     // <- สำหรับแสดงหน้าจอ You Dead


    void Start()
    {
        //set currentHealth ให้เท่ากับ maxHealth เมื่อตัวเกมเริ่ม
        //อัพเดตรูปหัวใจบน UI ให้แสดงพลังชีวิตเต็ม
        //currentHealth = maxHealth;
        ResetHealth();
    }
    public void ResetHealth()
    {
        currentHealth = maxHealth;
        UpdateHearts();
    }

    //เพิ่มหัวใจ
    public void AddHealth(int amount)
    {
        //เพิ่มพลังชีวิตทีละค่าที่รับมา set ใน PlayerDamagecalculate
        //ใช้ Mathf.Clamp เพื่อไม่ให้ค่าพลังชีวิตเกิน maxHealth
        //อัพเดต UI และพิมพ์ข้อความแสดงผลลง Console
        currentHealth += amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth); // ป้องกันเกิน max
        UpdateHearts();
        Debug.Log("Health added! Current health: " + currentHealth);
    }

    //โดนดาเมจ
    public void TakeDamage(int amount)
    {
        //ลดพลังชีวิตทีละค่าที่รับมา
        //ไม่ให้ค่าต่ำกว่า 0 ด้วย Clamp
        currentHealth -= amount;
        currentHealth = Mathf.Clamp(currentHealth, 0, maxHealth);
        UpdateHearts();
    }

    //void UpdateHearts()
    //{
    //    //วนลูปตามจำนวนหัวใจ

    //    for (int i = 0; i < hearts.Length; i++)
    //    {
    //        //ถ้า index น้อยกว่าค่า currentHealth → แสดงรูปหัวใจเต็ม
    //        if (i < currentHealth)
    //            hearts[i].sprite = fullHeart;
    //        //ถ้า index มากกว่าหรือเท่ากับ currentHealth → แสดงรูปหัวใจว่าง
    //        else
    //            hearts[i].sprite = emptyHeart;
    //    }
    //}
    void UpdateHearts()
    {
        for (int i = 0; i < hearts.Length; i++)
        {
            hearts[i].sprite = (i < currentHealth) ? fullHeart : emptyHeart;
        }

        if (currentHealth <= 0)
        {   
            Debug.Log("Player Dead");
            if (youDeadPanel != null)
                youDeadPanel.SetActive(true); // แสดงหน้า You Dead
           

            //if (restartButton != null)
            //    restartButton.gameObject.SetActive(true);

            Time.timeScale = 0f; // ❗ หยุดเวลาเกมทั้งหมด
        }
    }
    

}
