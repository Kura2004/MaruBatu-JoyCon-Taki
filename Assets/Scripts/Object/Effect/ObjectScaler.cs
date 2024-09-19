using UnityEngine;
using DG.Tweening;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject; // ����Ώۂ̃I�u�W�F�N�g

    [SerializeField]
    private float scaleRate = 1.3f; // �g�嗦

    [SerializeField]
    private float scaleDuration = 0.3f; // �g��E�k���ɂ����鎞��

    [SerializeField]
    private string targetTag = "Player"; // �G���Ώۂ̃^�O

    private Vector3 originalScale; // ���̃X�P�[��
    private Vector3 enlargedScale; // �g���̃X�P�[��

    // �^�O�����I�u�W�F�N�g���G��Ă��邩�̃t���O
    private bool isTouchingTarget = false;

    private Tween scaleTween; // ���݂̃X�P�[���A�j���[�V�����pTween

    private void OnEnable()
    {
        if (targetObject == null)
        {
            targetObject = transform;
        }

        // ���̃X�P�[����ۑ�
        originalScale = targetObject.localScale;
        enlargedScale = originalScale * scaleRate; // �g���̃X�P�[��
    }

    // �^�O�����I�u�W�F�N�g���G�ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnTriggerEnter(Collider other)
    {
        if ((GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup) ||
            GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup))) return;
        if (other.CompareTag(targetTag) && CanProcessInput())
        {
            isTouchingTarget = true;
            EnlargeObject();
        }
    }

    // �^�O�����I�u�W�F�N�g�����ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && CanProcessInput())
        {
            isTouchingTarget = false;
            ResetObjectSize();
        }
    }

    // �^�O�����I�u�W�F�N�g���G��Ă���ꍇ�ɂ̂ݏ��������s���郁�\�b�h
    private void LateUpdate()
    {
        //if (TimeControllerToggle.isTimeStopped || GameStateManager.Instance.IsRotating)
        //{
        //    return;
        //}

        //if (isTouchingTarget)
        //{
        //    EnlargeObject();
        //}
        //else
        //{
        //    ResetObjectSize();
        //}
    }

    // �I�u�W�F�N�g�̃T�C�Y��傫�����郁�\�b�h
    private void EnlargeObject()
    {
        // ���݂̃A�j���[�V�������~
        if (scaleTween != null && scaleTween.IsPlaying())
        {
            scaleTween.Kill();
        }

        scaleTween = targetObject.DOScale(enlargedScale, scaleDuration).SetEase(Ease.OutBack);
    }

    // �I�u�W�F�N�g�̃T�C�Y�����X�Ɍ��ɖ߂����\�b�h
    private void ResetObjectSize()
    {
        // ���݂̃A�j���[�V�������~
        if (scaleTween != null && scaleTween.IsPlaying())
        {
            scaleTween.Kill();
        }

        scaleTween = targetObject.DOScale(originalScale, scaleDuration).SetEase(Ease.InOutQuad);
    }

    // �����Ŏw�肵���X�P�[���ɐݒ肷�郁�\�b�h
    public void SetObjectScale(bool isEnlarged)
    {
        // ���݂̃A�j���[�V�������~
        if (scaleTween != null && scaleTween.IsPlaying())
        {
            scaleTween.Kill();
        }

        Vector3 targetScale = isEnlarged ? enlargedScale : originalScale;
        scaleTween = targetObject.DOScale(targetScale, scaleDuration).SetEase(isEnlarged ? Ease.OutBack : Ease.InOutQuad);
    }

    // ���͂������ł��邩�ǂ����𔻒肷�郁�\�b�h
    private bool CanProcessInput()
    {
        return !TimeControllerToggle.isTimeStopped && !GameStateManager.Instance.IsRotating;
    }
}
