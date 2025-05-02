using UnityEngine;
using UnityEngine.XR.ARFoundation;
using System;
using System.Collections;
using System.Collections.Generic;

public class GameManager : MonoBehaviour
{
    // จัดการตรวจจับพื้น AR
    [SerializeField] private ARPlaneManager planeManager;

    private ARSession _arSession; // สำหรับ reset session AR
    private bool _gameStarted = false; // เช็คว่าเริ่มเกมไปแล้วหรือยัง

    // Prefab ของ Player ที่จะถูกสร้าง
    [SerializeField] private GameObject playerPrefab;
    private GameObject playerInstance; // อ้างอิง instance ของ Player ที่ถูกสร้าง

    // Prefab ของแผนที่ (เช่น พื้นหรือฉาก)
    [SerializeField] private GameObject mapPrefab;
    private GameObject mapInstance; // instance ของแผนที่ที่ถูกสร้าง

    void Start()
    {
        // หา ARSession ใน Scene
        _arSession = FindFirstObjectByType<ARSession>();

        // ฟัง event จากปุ่ม Start และ Restart
        UIGameManager.OnStartButtonPressed += StartGame;
        UIGameManager.OnRestartButtonPressed += RestartGame;
    }

    void StartGame()
    {
        // ถ้าเกมเริ่มแล้ว ไม่ต้องเริ่มซ้ำ
        if (_gameStarted) return;
        _gameStarted = true;
        print("Game started!!!");

        // สร้างแผนที่ถ้ายังไม่มี
        if (!mapInstance)
        {
            mapInstance = Instantiate(mapPrefab, new Vector3(0, 0, 0), Quaternion.identity);
        }

        // หา SpawnPoint ภายใน map ที่สร้างขึ้น
        Transform spawnPoint = mapInstance.transform.Find("SpawnPoint");

        // สร้าง Player ที่ตำแหน่ง SpawnPoint ถ้ายังไม่มี
        if (!playerInstance && spawnPoint != null)
        {
            playerInstance = Instantiate(playerPrefab, spawnPoint.position, Quaternion.identity);
        }



        // ปิดการตรวจจับ plane ของ AR (เมื่อเริ่มเกมแล้วไม่ต้อง track ต่อ)
        planeManager.enabled = false;

        // ปิดการแสดงผล plane ทั้งหมดที่ตรวจเจอไปแล้ว
        foreach (var plane in planeManager.trackables)
        {
            var meshVisual = plane.GetComponent<ARPlaneMeshVisualizer>();
            if (meshVisual) meshVisual.enabled = false;

            var lineVisual = plane.GetComponent<LineRenderer>();
            if (lineVisual) lineVisual.enabled = false;
        }
    }

    void RestartGame()
    {
        _gameStarted = false;

        // ลบแผนที่ออกจาก scene ถ้ามีอยู่
        if (mapInstance)
        {
            Destroy(mapInstance);
            mapInstance = null;
        }

        // ลบ player ออกจาก scene ถ้ามีอยู่
        if (playerInstance)
        {
            Destroy(playerInstance);
            playerInstance = null;
        }

        // เรียก coroutine เพื่อ reset AR session
        StartCoroutine(RestartGameCoroutine());
    }

    IEnumerator RestartGameCoroutine()
    {
        // รอจนกว่า AR session กลับมาเป็นสถานะ "Tracking"
        while (ARSession.state != ARSessionState.SessionTracking)
        {
            yield return null;
        }

        // reset session AR ใหม่
        _arSession.Reset();

        // เปิดการตรวจจับ plane ใหม่
        planeManager.enabled = true;
    }
}
