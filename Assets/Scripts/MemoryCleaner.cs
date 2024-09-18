using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Runtime.InteropServices;
using System.Collections;

public class MemoryCleaner : SingletonMonoBehaviour<MemoryCleaner>
{
    [DllImport("kernel32.dll")]
    private static extern bool SetProcessWorkingSetSize(IntPtr process, int minSize, int maxSize);

    [SerializeField]
    private float cleanupInterval = 60f;

    private long lastUsedMemory;

    public void SimpleCleanMemory()
    {
        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            try
            {
                IntPtr processHandle = System.Diagnostics.Process.GetCurrentProcess().Handle;
                SetProcessWorkingSetSize(processHandle, -1, -1);
            }
            catch (Exception e)
            {
                Debug.LogError("�������̉�����ɃG���[���������܂���: " + e.Message);
            }
        }

        Debug.Log("��������������܂��� (SimpleCleanMemory)");
    }

    public void CleanMemory()
    {
        Resources.UnloadUnusedAssets();
        GC.Collect();
        GC.WaitForPendingFinalizers();
        GC.Collect();

        if (Application.platform == RuntimePlatform.WindowsPlayer || Application.platform == RuntimePlatform.WindowsEditor)
        {
            try
            {
                IntPtr processHandle = System.Diagnostics.Process.GetCurrentProcess().Handle;
                SetProcessWorkingSetSize(processHandle, -1, -1);
            }
            catch (Exception e)
            {
                Debug.LogError("�������̉�����ɃG���[���������܂���: " + e.Message);
            }
        }

        Debug.Log("��������������܂��� (CleanMemory)");
    }

    private void Awake()
    {
        CleanMemory();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        CleanMemory();
    }

    private void OnDestroy()
    {
        StopAllCoroutines();
    }

    private void Start()
    {
        StartCoroutine(PeriodicCleanMemory());
    }

    private IEnumerator PeriodicCleanMemory()
    {
        while (true)
        {
            yield return new WaitForSeconds(cleanupInterval);
            SimpleCleanMemory();
            //MonitorMemoryUsage();
        }
    }

    private void MonitorMemoryUsage()
    {
        long currentUsedMemory = GC.GetTotalMemory(false);
        float currentUsedMemoryMB = currentUsedMemory / (1024f * 1024f); // ��������MB�ɕϊ�

        Debug.Log($"���݂̎g�p������: {currentUsedMemoryMB:F2} MB");

        if (currentUsedMemory > lastUsedMemory)
        {
            Debug.Log("�������g�p�ʂ��������Ă��܂�");
        }

        lastUsedMemory = currentUsedMemory;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        CleanMemory(); 
    }
}
