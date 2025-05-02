using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using System.Collections;

public class UIGameManager : MonoBehaviour
{
    [Header("Button Setup")]
    [SerializeField] private Button startButton;     // ปุ่มเริ่มเกม
    [SerializeField] private Button restartButton;   // ปุ่มรีสตาร์ทเกม
    [SerializeField] private Button jumpButton;      // ปุ่มกระโดด
    
    [Header("UI Setup")]
    [SerializeField] private TMP_Text greetingText;  // ข้อความต้อนรับ
    [SerializeField] private PlayerController player; // ตัวแปรอ้างอิงไปยัง PlayerController
    [SerializeField] private GameObject youDeadPanel;     // <- สำหรับแสดงหน้าจอ You Dead

    [SerializeField] private GameObject joystickObject; // GameObject ที่เป็น joystick (FixedJoystick)


    // Event แบบ static สำหรับให้ component อื่นเรียกใช้งานได้
    public static event Action OnStartButtonPressed;
    public static event Action OnRestartButtonPressed;


    void Start()
    {
        // ผูกฟังก์ชันให้ทำงานเมื่อคลิกปุ่ม
        startButton.onClick.AddListener(OnUIStartButtonPressed);
        restartButton.onClick.AddListener(OnUIRestartButtonPressed);

        
        
        
        
        SetMovementButtonsActive(false);          // ซ่อนปุ่มควบคุมตอนเริ่ม
        restartButton.gameObject.SetActive(false); // ซ่อนปุ่ม restart
        joystickObject.SetActive(false);          // ซ่อน joystick
        youDeadPanel.SetActive(false);
    }

    void Update()
    {
       
    }

    void OnUIStartButtonPressed()
    {
        // ส่งสัญญาณ Event ออกไป
        OnStartButtonPressed?.Invoke();

        // ค้นหา PlayerController ในฉาก (กรณีถูก Instantiate ทีหลัง)
        player = FindObjectOfType<PlayerController>();

        // ผูก player ตัวที่ค้นหาเข้ากับปุ่มกระโดด
        SetPlayer(player);

        // ซ่อนปุ่ม start และแสดงปุ่ม restart
        startButton.gameObject.SetActive(false);
        restartButton.gameObject.SetActive(true);

        // แสดงปุ่ม jump และ joystick
        SetMovementButtonsActive(true);
        greetingText.gameObject.SetActive(false);
        joystickObject.SetActive(true);
        youDeadPanel.SetActive(false);
    }




    // Restart UI
    void OnUIRestartButtonPressed()
    {
        // ส่งสัญญาณ Event ออกไป
        OnRestartButtonPressed?.Invoke();

        // กลับสู่สถานะเริ่มต้น
        startButton.gameObject.SetActive(true);
        restartButton.gameObject.SetActive(false);
        SetMovementButtonsActive(false);
        greetingText.gameObject.SetActive(true);
        HealthSystem HealthSystem = FindObjectOfType<HealthSystem>();

        HealthSystem.ResetHealth(); // เรียกใช้งาน

        youDeadPanel.SetActive(false);
        Time.timeScale = 1f;
    }

   


    // set movement button active
    void SetMovementButtonsActive(bool isActive)
    {
        // กำหนดให้ปุ่ม jump แสดงหรือซ่อนตาม isActive
        jumpButton.gameObject.SetActive(isActive);

        // ซ่อน joystick ทุกครั้งที่เรียกใช้ (บรรทัดนี้อาจต้องเปลี่ยนถ้าจะควบคุมแบบแสดง/ซ่อนจริง ๆ)
        joystickObject.SetActive(isActive);
    }
    //set player
    public void SetPlayer(PlayerController newPlayer)
    {
        player = newPlayer;

        // ผูกฟังก์ชัน Jump ของ player เข้ากับปุ่ม jump
        jumpButton.onClick.AddListener(player.Jump);
    }
}
