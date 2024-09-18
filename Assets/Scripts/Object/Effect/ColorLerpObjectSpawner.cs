using DG.Tweening;
using UnityEngine.Pool;
using UnityEngine;

public class ColorLerpObjectSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabToSpawn;       // ��������I�u�W�F�N�g�̃v���n�u
    [SerializeField] private float spawnInterval = 1f;       // ��������Ԋu�i�b�j
    [SerializeField] private float spawnDuration = 5f;       // �I�[�g�I�t�܂ł̎��ԁi�b�j
    [SerializeField] private float yOffset = 0f;             // �����ʒu��Y���W�̃I�t�Z�b�g
    [SerializeField] private Color startColor = Color.white; // �⊮����ŏ��̐F
    [SerializeField] private Color endColor = Color.red;     // �⊮����Ō�̐F

    private ObjectPool<GameObject> objectPool;               // �I�u�W�F�N�g�v�[��
    private bool isSpawning = false;                         // �����̃I��/�I�t���
    private Sequence spawnSequence;                          // �����̃V�[�P���X

    private void Start()
    {
        // ObjectPool�̏�����
        objectPool = new ObjectPool<GameObject>(
            createFunc: () => Instantiate(prefabToSpawn),
            actionOnGet: (obj) => obj.SetActive(true),
            actionOnRelease: (obj) => obj.SetActive(false),
            actionOnDestroy: (obj) => Destroy(obj),
            defaultCapacity: 10,
            maxSize: 20
        );
    }

    /// <summary>
    /// �������J�n����
    /// </summary>
    public void StartSpawning()
    {
        if (isSpawning) return;

        isSpawning = true;

        // DOTween�V�[�P���X�̍쐬
        spawnSequence = DOTween.Sequence()
            .AppendInterval(spawnInterval)
            .AppendCallback(() => SpawnObject())
            .SetLoops(-1, LoopType.Restart) // ���[�v������
            .OnKill(() => isSpawning = false); // �V�[�P���X����~�����Ƃ��ɐ������I�t�ɂ���

        // ��莞�Ԍ�ɐ����������I�ɒ�~
        DOVirtual.DelayedCall(spawnDuration, StopSpawning);
    }

    /// <summary>
    /// �������~����
    /// </summary>
    public void StopSpawning()
    {
        if (spawnSequence != null && isSpawning)
        {
            spawnSequence.Kill(); // �V�[�P���X���~���Ĕj��
        }
    }

    /// <summary>
    /// �I�u�W�F�N�g�𐶐�����
    /// </summary>
    private void SpawnObject()
    {
        // ��������ʒu��ݒ�
        Vector3 spawnPosition = transform.position + new Vector3(0, yOffset, 0);

        // �I�u�W�F�N�g���v�[������擾
        GameObject spawnedObject = objectPool.Get();

        // �I�u�W�F�N�g�̈ʒu��ݒ�
        spawnedObject.transform.position = spawnPosition;

        // ���ԂɊ�Â��ĐF��⊮
        float t = (Time.time % spawnDuration) / spawnDuration;
        Color currentColor = Color.Lerp(startColor, endColor, t);

        // �I�u�W�F�N�g�̐F��ݒ�
        SetObjectColor(spawnedObject, currentColor);

        // ��莞�Ԍ�ɃI�u�W�F�N�g���v�[���ɖ߂�
        DOVirtual.DelayedCall(spawnDuration, () => objectPool.Release(spawnedObject));
    }

    /// <summary>
    /// �I�u�W�F�N�g�̐F��ݒ肷��
    /// </summary>
    private void SetObjectColor(GameObject obj, Color color)
    {
        Renderer renderer = obj.GetComponent<Renderer>();
        if (renderer != null)
        {
            renderer.material.color = color;
        }
    }

    /// <summary>
    /// �⊮�F��ݒ肷��
    /// </summary>
    public void SetLerpColors(Color newStartColor, Color newEndColor)
    {
        startColor = newStartColor;
        endColor = newEndColor;
    }
}

