using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using System.Collections;

public class UIObjectShaker : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 2f;  // �k����b��
    [SerializeField] private float shakeIntensity = 1f;  // �k���̋���
    [SerializeField] private int vibrato = 10;  // �k���̉�
    [SerializeField] private RectTransform uiElementToShake;  // �k����UI�v�f�iRectTransform�j

    private Vector3 originalPosition;  // UI�v�f�̌��̈ʒu
    private Tween currentShakeTween;  // ���݂̐k���A�j���[�V�������Ǘ�����Tween
    private Coroutine spawnCoroutine;  // �v���n�u�����R���[�`��

    public void ShakeUIElement()
    {
        SaveOriginalPosition();

        // �����̃A�j���[�V����������Β��f
        StopShake();

        // �V�����k���A�j���[�V�������J�n
        currentShakeTween = uiElementToShake.DOShakePosition(shakeDuration, shakeIntensity, vibrato)
            .SetEase(Ease.OutQuad)
            .OnKill(() =>
            {
                // �A�j���[�V�������I�������猳�̈ʒu�ɖ߂�
                uiElementToShake.anchoredPosition = originalPosition;
            });
    }

    // �k�����~�߂�
    public void StopShake()
    {
        if (currentShakeTween != null && currentShakeTween.IsPlaying())
        {
            // �A�j���[�V�������Đ����ł���ꍇ�A��~����
            currentShakeTween.Kill();
            // ���̈ʒu�ɖ߂�
            uiElementToShake.anchoredPosition = originalPosition;
        }
    }

    // ���̈ʒu���L�^
    private void SaveOriginalPosition()
    {
        originalPosition = uiElementToShake.anchoredPosition;
    }
}
