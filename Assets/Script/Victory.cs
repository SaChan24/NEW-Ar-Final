using UnityEngine;

public class Victory : MonoBehaviour
{
    public GameObject youWinPanel; // ลาก Panel You Win มาวางจาก Inspector

    private void Start()
    {
        if (youWinPanel != null)
            youWinPanel.SetActive(false); // ซ่อนไว้ก่อน
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player reached the victory!");

            if (youWinPanel != null)
                youWinPanel.SetActive(true); // แสดงหน้า You Win

            Time.timeScale = 0f; // ❗ หยุดเวลาเกม
        }
    }
}
