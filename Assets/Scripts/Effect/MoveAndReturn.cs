using UnityEngine;
using DG.Tweening;

public class MoveAndReturn : SingletonMonoBehaviour<MoveAndReturn>
{
    [Header("�ړ��ݒ�")]
    [SerializeField] private Transform startPoint; // �J�n�n�_
    [SerializeField] private Transform endPoint;   // �ړI�n
    [SerializeField] private float moveDuration = 2f; // �ړI�n�܂ł̈ړ�����
    [SerializeField] private float waitTime = 1f; // �ړI�n�ł̑ҋ@����

    [Header("�߂�ݒ�")]
    [SerializeField] private float returnDuration = 2f; // ���̈ʒu�ɖ߂鎞��

    [Header("�A�j���[�V�����Ώ�")]
    [SerializeField] private GameObject targetObject; // �A�j���[�V������������I�u�W�F�N�g

    private Sequence moveSequence;

    private void Start()
    {
        // �A�j���[�V�����ΏۃI�u�W�F�N�g���A�N�e�B�u�ɐݒ�
        if (targetObject != null)
        {
            targetObject.transform.position = startPoint.position;
            targetObject.SetActive(false);
        }
        // �ړ��V�[�P���X�̍쐬
        CreateMovementSequence();
    }

    private void CreateMovementSequence()
    {
        // �����̃V�[�P���X������΁A�폜����
        if (moveSequence != null)
        {
            moveSequence.Kill();
        }

        // DOTween���g�p���Ĉړ��V�[�P���X���쐬
        moveSequence = DOTween.Sequence()
            .Append(targetObject.transform.DOMove(endPoint.position, moveDuration).SetEase(Ease.InOutQuad)) // �ړ�
            .AppendInterval(waitTime) // �ҋ@
            .Append(targetObject.transform.DOMove(startPoint.position, returnDuration).SetEase(Ease.InOutQuad)) // �߂�
            .OnComplete(OnAnimationComplete) // �A�j���[�V�����������Ƀ��\�b�h���Ăяo��
            .SetAutoKill(false) // �V�[�P���X�������I�ɍ폜���Ȃ�
            .Pause(); // �V�[�P���X���ꎞ��~���Ă���
    }

    public void StartAnimation()
    {
        if (targetObject != null)
        {
            // �A�j���[�V�����ΏۃI�u�W�F�N�g���A�N�e�B�u�ɂ���
            targetObject.SetActive(true);
        }

        if (!moveSequence.IsPlaying())
        {
            moveSequence.Restart();
        }
    }

    private void OnAnimationComplete()
    {
        if (targetObject != null)
        {
            // �A�j���[�V����������A�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            targetObject.SetActive(false);
        }

        ScenesLoader.Instance.LoadGameOver(0);
    }

    public void StopAnimation()
    {
        if (moveSequence != null)
        {
            moveSequence.Pause();
        }

        if (targetObject != null)
        {
            targetObject.transform.position = startPoint.position;
            // �A�j���[�V������~��A�I�u�W�F�N�g���A�N�e�B�u�ɂ���
            targetObject.SetActive(false);
        }
    }
}
