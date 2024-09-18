using UnityEngine;
using Cinemachine;
using System.Collections.Generic;

[System.Serializable]
public class CameraMapping
{
    public CameraSwitcher.CameraType cameraType;
    public CinemachineVirtualCamera camera;
}

public class CameraSwitcher : SingletonMonoBehaviour<CameraSwitcher>
{
    public enum CameraType
    {
        Main,
        Secondary,
        Action,
        // �K�v�ɉ����đ��̃J�����^�C�v��ǉ�
    }

    [Header("Camera Settings")]
    [SerializeField] private List<CameraMapping> cameraMappings = new List<CameraMapping>();
    [SerializeField] private CameraType defaultCamera = CameraType.Main;

    private Dictionary<CameraType, CinemachineVirtualCamera> cameraDictionary;

    protected override void OnEnable()
    {
        InitializeCameraDictionary();
        base.OnEnable();
    }

    private void InitializeCameraDictionary()
    {
        cameraDictionary = new Dictionary<CameraType, CinemachineVirtualCamera>();

        foreach (var mapping in cameraMappings)
        {
            if (mapping.camera != null)
            {
                cameraDictionary[mapping.cameraType] = mapping.camera;
            }
        }
    }

    private void Start()
    {
        // �f�t�H���g�̃J�������A�N�e�B�u�ɂ���
        SwitchCamera(defaultCamera);
    }

    /// <summary>
    /// �J������؂�ւ��郁�\�b�h
    /// </summary>
    /// <param name="cameraType">�؂�ւ���J�����̃^�C�v</param>
    public void SwitchCamera(CameraType cameraType)
    {
        // �S�ẴJ�������A�N�e�B�u�ɂ���
        foreach (var cam in cameraDictionary.Values)
        {
            cam.gameObject.SetActive(false);
        }

        // �w�肳�ꂽ�J�����������A�N�e�B�u�ɂ���
        if (cameraDictionary.TryGetValue(cameraType, out var camera))
        {
            camera.gameObject.SetActive(true);
        }
    }

    /// <summary>
    /// �J�������X�g�ɃJ������ǉ����郁�\�b�h
    /// </summary>
    /// <param name="camera">�ǉ�����J����</param>
    public void AddCamera(CameraType type, CinemachineVirtualCamera camera)
    {
        if (camera != null)
        {
            if (!cameraDictionary.ContainsKey(type))
            {
                cameraDictionary[type] = camera;
                cameraMappings.Add(new CameraMapping { cameraType = type, camera = camera });
            }
        }
    }

    /// <summary>
    /// �J�������X�g����J�������폜���郁�\�b�h
    /// </summary>
    /// <param name="camera">�폜����J����</param>
    public void RemoveCamera(CinemachineVirtualCamera camera)
    {
        if (camera != null)
        {
            var entry = cameraMappings.Find(mapping => mapping.camera == camera);
            if (entry != null)
            {
                cameraDictionary.Remove(entry.cameraType);
                cameraMappings.Remove(entry);
            }
        }
    }
}
