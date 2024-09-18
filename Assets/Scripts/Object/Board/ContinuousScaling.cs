using UnityEngine;
using DG.Tweening;

public class ContinuousScaling : MonoBehaviour
{
    [SerializeField] private float scalingRate = 1.1f;
    [SerializeField] private float scalingDuration = 1.0f; // �X�P�[���̑����ɂ����鎞��
    [SerializeField] private bool loopScaling = true; // �X�P�[�������[�v�����邩�ǂ���

    private void Start()
    {
        StartScaling();
    }

    private void StartScaling()
    {
        // �I�u�W�F�N�g�̃X�P�[���𑝉�������
        transform.DOScale(transform.localScale * scalingRate, scalingDuration)
            .SetEase(Ease.Linear)
            .SetLoops(loopScaling ? -1 : 0, LoopType.Incremental); // ���[�v�����ăX�P�[���𑝉���������
    }
}

