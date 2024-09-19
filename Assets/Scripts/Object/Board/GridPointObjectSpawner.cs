using UnityEngine;
using System.Collections.Generic;

public class GridPointObjectSpawner : MonoBehaviour
{
    /*[SerializeField] private GameObject objectToSpawn; // ��������I�u�W�F�N�g
    [SerializeField] private string generatorTag = "DefaultTag"; // ������̃^�O
    [SerializeField] private int indexToUse = 0; // �g�p����ʒu�̃C���f�N�X
    [SerializeField] private Vector3 offset;

    bool isTrigger = false;

    private void LateUpdate()
    {
        if (!isTrigger)
        {
            SpawnObjectAtGridPoint();
            Destroy(this);
        }
    }

    private void SpawnObjectAtGridPoint()
    {
        // �V�[�����̑S�Ă̐�������擾
        GridObjectGenerator[] generators = FindObjectsByType<GridObjectGenerator>(FindObjectsSortMode.None);
        // �w�肵���^�O�̐���������X�g��
        List<GridObjectGenerator> generatorList = new List<GridObjectGenerator>();
        foreach (GridObjectGenerator generatorObj in generators)
        {
            if (generatorObj.IsTagMatching(generatorTag))
            {
                generatorList.Add(generatorObj);
            }
        }

        // �����킪������Ȃ��ꍇ�̓G���[���b�Z�[�W
        if (generatorList.Count == 0)
        {
            Debug.LogError($"No GridObjectGenerator found with tag '{generatorTag}'");
            return;
        }

        // �w�肵���C���f�N�X�̈ʒu���m�F���A�I�u�W�F�N�g�𐶐�
        foreach (GridObjectGenerator generator in generatorList)
        {
            if (indexToUse >= 0 && indexToUse < generator.GeneratedPositions.Count)
            {
                Transform spawnPosition = generator.GeneratedPositions[indexToUse];
                spawnPosition.localPosition += offset;
                if (objectToSpawn != null)
                {
                    Instantiate(objectToSpawn, spawnPosition.position, Quaternion.identity);
                }
                else
                {
                    Debug.LogError("ObjectToSpawn is not assigned.");
                }
            }
            else
            {
                Debug.LogWarning($"Index {indexToUse} is out of range for generator with tag '{generatorTag}'.");
            }
        }
    }*/
}
