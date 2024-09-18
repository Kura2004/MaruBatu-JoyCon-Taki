using UnityEngine;
using UnityEngine.Pool;

public class CustomCursor : MonoBehaviour
{
    [SerializeField] private GameObject imagePrefab; // ��������摜�̃v���n�u
    [SerializeField] private float initialScale = 0.5f; // �摜�̏����X�P�[��
    [SerializeField] private Vector3 initialRotation = Vector3.zero; // �摜�̏�����]�p�x
    [SerializeField] private float spawnInterval = 0.1f; // �����Ԋu
    [SerializeField] private float yOffset = 1f; // �摜��Y���W�I�t�Z�b�g
    [SerializeField] private string targetTag = "Target"; // Raycast��������^�[�Q�b�g�̃^�O

    private bool isSpawning = false; // ���������ǂ����̃t���O
    private float nextSpawnTime = 0f; // ���ɐ������鎞��

    // ObjectPool�̃C���X�^���X
    private ObjectPool<GameObject> pool;

    private void Start()
    {
        // ObjectPool�̏�����
        pool = new ObjectPool<GameObject>(
            // �I�u�W�F�N�g�𐶐�������@
            createFunc: () => Instantiate(imagePrefab),

            // �I�u�W�F�N�g���v�[��������o���ۂɌĂ΂��A�N�V����
            actionOnGet: (obj) => obj.SetActive(true),

            // �I�u�W�F�N�g���v�[���ɖ߂��ۂɌĂ΂��A�N�V����
            actionOnRelease: (obj) => obj.SetActive(false),

            // �I�u�W�F�N�g���v�[���ɖ߂��ۂɌĂ΂��A�N�V����
            actionOnDestroy: (obj) => Destroy(obj),

            // �v�[���̏����e��
            defaultCapacity: 10,

            // �v�[���̍ő�e��
            maxSize: 20
        );
    }

    private void Update()
    {
        // �}�E�X�{�^����������Ă����
        if (Input.GetMouseButton(0))
        {
            if (!isSpawning)
            {
                isSpawning = true;
                nextSpawnTime = Time.time;
            }

            // �w�肵���Ԋu�ŃI�u�W�F�N�g�𐶐�
            if (Time.time >= nextSpawnTime)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out hit, Mathf.Infinity))
                {
                    // �^�O����v����ꍇ�ɂ̂݃I�u�W�F�N�g�𐶐�
                    if (hit.collider.CompareTag(targetTag))
                    {
                        // �q�b�g�����ʒu�ɃI�u�W�F�N�g�𐶐�
                        SpawnImage(hit.point);
                    }
                }
                nextSpawnTime = Time.time + spawnInterval;
            }
        }

        // �}�E�X�{�^���������ꂽ�琶�����~
        if (Input.GetMouseButtonUp(0))
        {
            isSpawning = false;
        }
    }

    private void SpawnImage(Vector3 position)
    {
        // Y���W�𒲐�
        Vector3 spawnPosition = new Vector3(position.x, yOffset, position.z);

        // �v�[������I�u�W�F�N�g���擾
        GameObject newImage = pool.Get();

        // �ʒu�Ɖ�]��ݒ�
        newImage.transform.position = spawnPosition;
        newImage.transform.rotation = Quaternion.Euler(initialRotation);

        // �����̃X�P�[����ݒ�
        newImage.transform.localScale = Vector3.one * initialScale;
    }

    private void OnDestroy()
    {
        // �v�[����j��
        pool.Clear();
    }
}
