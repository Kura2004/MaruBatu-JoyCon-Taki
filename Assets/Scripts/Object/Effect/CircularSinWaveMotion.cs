using UnityEngine;
using System.Collections.Generic;

public class CircularSinWaveMotion : MonoBehaviour
{
    [SerializeField] private List<GameObject> targetPrefabList; // ��������^�[�Q�b�g�̃v���n�u���X�g
    [SerializeField] private Transform centerPoint; // ���S�_
    [SerializeField] private float baseRadius = 5f; // ��{�̉~�̔��a
    [SerializeField] private float amplitude = 1f; // �㉺�^���̐U��
    [SerializeField] private float baseFrequency = 1f; // ��{�̏㉺�^���̎��g��
    [SerializeField] private float radiusFrequency = 1f; // ���a�̕ω��̎��g��
    [SerializeField] private float rotationSpeed = 1f; // �~�^���̑��x

    private Transform[] targetArray;
    private Vector3[] initialPositions;

    void Start()
    {
        int numberOfTargets = targetPrefabList.Count;
        targetArray = new Transform[numberOfTargets];
        initialPositions = new Vector3[numberOfTargets];

        GenerateTargets(numberOfTargets);
        InitializeTargetPositions(numberOfTargets);
    }

    void Update()
    {
        AnimateTargets();
    }

    // �^�[�Q�b�g�𐶐�
    private void GenerateTargets(int numberOfTargets)
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            GameObject newTarget = Instantiate(targetPrefabList[i]);
            targetArray[i] = newTarget.transform;
        }
    }

    // �^�[�Q�b�g���~����ɋϓ��ɔz�u
    private void InitializeTargetPositions(int numberOfTargets)
    {
        for (int i = 0; i < numberOfTargets; i++)
        {
            float angle = i * Mathf.PI * 2 / numberOfTargets;
            Vector3 newPosition = centerPoint.position + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * baseRadius;
            initialPositions[i] = newPosition;
            targetArray[i].position = newPosition;
        }
    }

    // �^�[�Q�b�g�̏㉺�^���Ɖ~�^�����X�V
    private void AnimateTargets()
    {
        for (int i = 0; i < targetArray.Length; i++)
        {
            Transform target = targetArray[i];
            if (target != null)
            {
                float time = Time.time;
                float frequency = baseFrequency * (1 + i * 0.1f); // �C���f�b�N�X�ɂ���Ď��g���𒲐�

                // �㉺�^��
                float verticalOffset = Mathf.Sin(time * frequency) * amplitude * 0.3f;

                // ���a�̕ω�
                float dynamicRadius = baseRadius + Mathf.Sin(time * radiusFrequency) * amplitude;

                // �~�^��
                float angle = i * Mathf.PI * 2 / targetArray.Length + time * rotationSpeed;
                Vector3 offset = new Vector3(Mathf.Cos(angle), verticalOffset, Mathf.Sin(angle)) * dynamicRadius;
                target.position = centerPoint.position + offset;
            }
        }
    }
}





