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

    private Vector3 originalScale; // ���̃X�P�[��
    private Vector3 enlargedScale; // �g���̃X�P�[��

    // �}�E�X���͂𖳎����邩�ǂ������Ǘ�����
    public bool ignoreMouseInput = false;

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

    // �}�E�X���͂���������Ă��Ȃ��ꍇ�ŁA���Ԃ��~�܂��Ă��Ȃ��ꍇ�ɏ��������s���邩�ǂ����𔻒肷�郁�\�b�h
    protected virtual bool CanProcessMouseInput()
    {
        return !ignoreMouseInput && !TimeControllerToggle.isTimeStopped && !GameStateManager.Instance.IsRotating;
    }

    // �}�E�X���I�u�W�F�N�g�ɐG�ꂽ���ɌĂ΂�郁�\�b�h
    protected virtual void OnMouseEnter()
    {
        if (CanProcessMouseInput()) // �}�E�X���͂���������Ă��Ȃ��ꍇ�̂ݏ��������s
        {
            EnlargeObject();
        }
    }

    // �}�E�X���I�u�W�F�N�g��ɂ���ԂɌĂ΂�郁�\�b�h
    protected virtual void OnMouseOver()
    {
        if (CanProcessMouseInput() && !targetObject.DOScale(enlargedScale, scaleDuration).IsPlaying()) // �}�E�X���͂���������Ă��炸�A�A�j���[�V���������s���łȂ��ꍇ�̂ݏ��������s
        {
            EnlargeObject();
        }
    }

    // �}�E�X���I�u�W�F�N�g���痣�ꂽ���ɌĂ΂�郁�\�b�h
    protected virtual void OnMouseExit()
    {
        if (CanProcessMouseInput()) // �}�E�X���͂���������Ă��Ȃ��ꍇ�̂ݏ��������s
        {
            ResetObjectSize();
        }
    }

    // �I�u�W�F�N�g�̃T�C�Y��傫�����郁�\�b�h
    protected virtual void EnlargeObject()
    {
        targetObject.DOScale(enlargedScale, scaleDuration).SetEase(Ease.OutBack);
    }

    // �I�u�W�F�N�g�̃T�C�Y�����X�Ɍ��ɖ߂����\�b�h
    protected virtual void ResetObjectSize()
    {
        targetObject.DOScale(originalScale, scaleDuration).SetEase(Ease.InOutQuad);
    }

    // �}�E�X���͂𖳎����郁�\�b�h
    public void IgnoreMouseInput()
    {
        ignoreMouseInput = true;
        // ���ׂĂ�Tween�𖳌��ɂ���
        targetObject.DOKill();
    }

    // �}�E�X���͂����ɖ߂����\�b�h
    public void ResumeMouseInput()
    {
        ignoreMouseInput = false;
    }

    // �I�u�W�F�N�g�̑傫�������ɖ߂��Ă��邩�ǂ����𔻒肷�郁�\�b�h
    protected virtual bool IsObjectSizeReset()
    {
        return targetObject.localScale != originalScale && !GetComponent<MouseHoverChecker>().IsMouseOver()
            && !targetObject.DOScale(originalScale, scaleDuration).IsActive();
    }
}
