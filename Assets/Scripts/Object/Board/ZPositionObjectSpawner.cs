using UnityEngine;

public class ZPositionObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab; // ��������I�u�W�F�N�g�̃v���n�u

    [SerializeField]
    private float zSpacing = 2f; // z�����̊Ԋu

    [SerializeField]
    private int objectCount = 4; // ��������I�u�W�F�N�g�̐�

    private void Start()
    {
        SpawnObjects();
        //�z�u���I�������폜
        Destroy(this);
    }

    private void SpawnObjects()
    {
        float zOffset = (objectCount - 1) * zSpacing / 2;

        for (int i = 0; i < objectCount; i++)
        {
            Vector3 position = new Vector3(0, 0, i * zSpacing - zOffset);
            Quaternion rotation = Quaternion.Euler(0, 90, 0); // y������90�x��]
            Instantiate(prefab, position, rotation);
        }
    }
}

