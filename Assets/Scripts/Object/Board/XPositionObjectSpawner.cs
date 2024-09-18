using UnityEngine;

public class XPositionObjectSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject prefab; // ��������I�u�W�F�N�g�̃v���n�u

    [SerializeField]
    private float xSpacing = 2f; // x�����̊Ԋu

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
        // �z��̒�����0�Ƃ��邽�߂̃I�t�Z�b�g���v�Z
        float xOffset = (objectCount - 1) * xSpacing / 2f;

        for (int i = 0; i < objectCount; i++)
        {
            // �I�t�Z�b�g�������Ē��S�ɑ�����
            Vector3 position = new Vector3(i * xSpacing - xOffset, 0, 0);
            Instantiate(prefab, position, Quaternion.identity);
        }
    }
}


