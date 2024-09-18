using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.EventSystems;

public class ButtonScaler : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private Button targetButton; // ����Ώۂ̃{�^��

    [SerializeField]
    float scaleRate = 1.3f;

    private Vector3 enlargedScale;

    [SerializeField]
    private float scaleDuration = 0.3f; // �g��E�k���ɂ����鎞��

    private Vector3 originalScale; // ���̃X�P�[��

    private void Start()
    {
        if (targetButton == null)
        {
            targetButton = GetComponent<Button>();
        }

        // ���̃X�P�[����ۑ�
        originalScale = targetButton.transform.localScale;
        enlargedScale = originalScale * scaleRate; // �g���̃X�P�[��
    }

    // �}�E�X���{�^���ɐG�ꂽ���ɌĂ΂�郁�\�b�h
    public void OnPointerEnter(PointerEventData eventData)
    {
        EnlargeButton();
    }

    // �}�E�X���{�^�����痣�ꂽ���ɌĂ΂�郁�\�b�h
    public void OnPointerExit(PointerEventData eventData)
    {
        ResetButtonSize();
    }

    // �{�^���̃T�C�Y��傫�����郁�\�b�h
    public void EnlargeButton()
    {
        targetButton.transform.DOScale(enlargedScale, scaleDuration).SetEase(Ease.OutBack);
    }

    // �{�^���̃T�C�Y�����X�Ɍ��ɖ߂����\�b�h
    public void ResetButtonSize()
    {
        targetButton.transform.DOScale(originalScale, scaleDuration).SetEase(Ease.InOutQuad);
    }
}

