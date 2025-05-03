using UnityEngine;

public class PillarManager : MonoBehaviour
{
    public static PillarManager Instance;

    private int destroyedPillars = 0;
    public int targetDestroyedCount = 2;

    public GameObject victoryDoor; // ��еٷ��������
    public GameObject DoorOpen;
    public GameObject[] objectsToDestroy; // �ѵ�ط���ͧ��÷������ѧ�ҡ�ú�ӹǹ���

    private void Awake()
    {
        Instance = this;
        if (victoryDoor != null)
        {
            victoryDoor.SetActive(false); // ��͹��е�����͹
        }
    }

    public void PillarDestroyed()
    {
        destroyedPillars++;
        Debug.Log("Pillar destroyed! Total: " + destroyedPillars);

        if (destroyedPillars >= targetDestroyedCount)
        {
            ShowVictoryDoor();
            ShowDoorOpen();
        }
    }

    void ShowVictoryDoor()
    {
        Debug.Log("Victory Door appears!");
        if (victoryDoor != null)
        {
            victoryDoor.SetActive(true);
            DestroySpecifiedObjects();
        }
    }

    void ShowDoorOpen()
    {
        Debug.Log("Door Open");
        if (DoorOpen != null)
        {
            DoorOpen.SetActive(true);
            DestroySpecifiedObjects();
        }
    }


    void DestroySpecifiedObjects()
    {
        foreach (GameObject obj in objectsToDestroy)
        {
            if (obj != null)
            {
                Destroy(obj);
            }
        }
    }
}
