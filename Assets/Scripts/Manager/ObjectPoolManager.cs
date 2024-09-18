using UnityEngine.Pool;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    // �v�[������I�u�W�F�N�g�̃v���n�u
    [SerializeField] private GameObject prefab;

    // ObjectPool�̃C���X�^���X
    private ObjectPool<GameObject> pool;

    private void Start()
    {
        // ObjectPool�̏�����
        pool = new ObjectPool<GameObject>(
            // �I�u�W�F�N�g�𐶐�������@
            createFunc: () => Instantiate(prefab),

            // �I�u�W�F�N�g���v�[��������o���ۂɌĂ΂��A�N�V����
            actionOnGet: (obj) => obj.SetActive(true),

            // �I�u�W�F�N�g���v�[���ɖ߂��ۂɌĂ΂��A�N�V����
            actionOnRelease: (obj) => obj.SetActive(false),

            // �v�[�����N���A����ۂɌĂ΂��A�N�V����
            actionOnDestroy: (obj) => Destroy(obj),

            // �v�[���̏����e��
            defaultCapacity: 10,

            // �v�[���̍ő�e��
            maxSize: 20
        );
    }

    // �I�u�W�F�N�g���v�[������擾���郁�\�b�h
    public GameObject GetPooledObject()
    {
        return pool.Get();
    }

    // �I�u�W�F�N�g���v�[���ɖ߂����\�b�h
    public void ReleasePooledObject(GameObject obj)
    {
        pool.Release(obj);
    }
}

