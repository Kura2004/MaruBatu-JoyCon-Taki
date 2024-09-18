using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GridObjectGenerator : MonoBehaviour
{
    [SerializeField] private GameObject[] objectPrefabs; // ��������I�u�W�F�N�g�̃v���n�u�̔z��
    [SerializeField] private int rows = 5; // �i�q�̍s�� (n)
    [SerializeField] private int columns = 5; // �i�q�̗� (m)
    [SerializeField] private float spacing = 1.0f; // �i�q�_�̊Ԋu
    [SerializeField] private Transform gridReferenceTransform; // �i�q�_�̊�ƂȂ�Transform
    [SerializeField] private float spawnInterval = 2f; // n�b���Ƃ̊Ԋu
    private bool onGenerate = false;
    private List<GameObject> generatedObjects = new List<GameObject>(); // �������ꂽ�I�u�W�F�N�g��ێ�

    private void Start()
    {
        GenerateGrid();
    }
    private void Update()
    {
        if (GameTurnManager.Instance.IsGameStarted && !onGenerate)
        {
            onGenerate = true;
            StartCoroutine(ActivateObjects()); // �R���[�`���ŃI�u�W�F�N�g�������\��
        }
    }

    private void GenerateGrid()
    {
        if (gridReferenceTransform == null)
        {
            Debug.LogError("GridReferenceTransform is not assigned.");
            return;
        }

        if (objectPrefabs.Length == 0)
        {
            Debug.LogError("ObjectPrefabs array is empty.");
            return;
        }

        // �i�q�_�̌Q�̒��S����ɂ���
        Vector3 gridCenter = gridReferenceTransform.position;

        // �v���n�u�̃C���f�b�N�X��ǐՂ��邽�߂̕ϐ�
        int prefabIndex = 0;

        // ���ׂẴI�u�W�F�N�g�𐶐�����Renderer���I�t�ɂ���
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                // �i�q�_�̈ʒu���v�Z
                Vector3 position = gridCenter + new Vector3((column - (columns - 1) / 2f) * spacing, 0, (row - (rows - 1) / 2f) * spacing);

                // �v���n�u���擾������
                GameObject prefab = objectPrefabs[prefabIndex];
                if (prefab != null)
                {
                    GameObject newObj = Instantiate(prefab, position, Quaternion.identity);
                    newObj.SetActive(false);
                    generatedObjects.Add(newObj);
                }
                else
                {
                    Debug.LogWarning($"Prefab at index {prefabIndex} is not assigned.");
                }

                // �C���f�b�N�X���X�V�i�v�f���� `objectPrefabs.Length` �Ń��Z�b�g�j
                prefabIndex = (prefabIndex + 1) % objectPrefabs.Length;
            }
        }
    }

    private IEnumerator ActivateObjects()
    {
        foreach (GameObject obj in generatedObjects)
        {
            if (obj != null)
            {
                obj.SetActive(true);
                ScenesAudio.SetSe(); // �T�E���h�G�t�F�N�g���Đ�
                yield return new WaitForSeconds(spawnInterval); // ���̃I�u�W�F�N�g��\������܂őҋ@
            }
        }

        //�z�u���I�������폜
        Destroy(this);
    }
}
