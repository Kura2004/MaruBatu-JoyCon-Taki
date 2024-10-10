using UnityEngine;
using DG.Tweening; // DOTween�̖��O���

public class CanvasFader : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private CanvasGroup mainCanvasGroup; // �t�F�[�h�C���E�A�E�g�Ώۂ�CanvasGroup�R���|�[�l���g

    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 0.5f; // �t�F�[�h�̎�������

    [SerializeField] bool onStart = false;

    private void OnEnable()
    {
        // CanvasGroup��RectTransform�̐ݒ���m�F
        if (mainCanvasGroup == null)
        {
            Debug.LogError("CanvasGroup is not assigned in the inspector.");
        }

        // ������ԂŃL�����o�X���\���ɐݒ�
        if (mainCanvasGroup != null)
        {
            mainCanvasGroup.alpha = 0; // �A���t�@�l��0�ɐݒ�
            mainCanvasGroup.interactable = false;
            mainCanvasGroup.blocksRaycasts = false;
        }

        if (onStart)
        {
            ShowCanvas();
        }
    }

    /// <summary>
    /// �L�����o�X���t�F�[�h�C���ŕ\�����郁�\�b�h
    /// </summary>
    public void ShowCanvas()
    {
        if (mainCanvasGroup != null)
        {
            // �A���t�@�l��1�Ƀt�F�[�h
            mainCanvasGroup.DOFade(1, fadeDuration).OnStart(() =>
            {
                mainCanvasGroup.interactable = true;
                mainCanvasGroup.blocksRaycasts = true;
                Debug.Log("Canvas is fading in.");
            });
        }
    }

    /// <summary>
    /// �L�����o�X���t�F�[�h�A�E�g�Ŕ�\���ɂ��郁�\�b�h
    /// </summary>
    public void HideCanvas()
    {
        if (mainCanvasGroup != null)
        {
            // �A���t�@�l��0�Ƀt�F�[�h
            mainCanvasGroup.DOFade(0, fadeDuration).OnComplete(() =>
            {
                mainCanvasGroup.interactable = false;
                mainCanvasGroup.blocksRaycasts = false;
                Debug.Log("Canvas is faded out.");
            });
        }
    }

    /// <summary>
    /// �L�����o�X�̕\���E��\�����t�F�[�h�Ő؂�ւ��郁�\�b�h
    /// </summary>
    public void ToggleCanvas()
    {
        if (mainCanvasGroup != null)
        {
            if (mainCanvasGroup.alpha > 0)
            {
                HideCanvas();
            }
            else
            {
                ShowCanvas();
            }
        }
    }
}
