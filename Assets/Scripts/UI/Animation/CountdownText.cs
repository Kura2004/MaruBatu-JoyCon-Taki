using UnityEngine;
using TMPro;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class CountdownText : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI countdownText;
    [SerializeField] private Image uiPanel; // UI�p�l����Image�N���X�Őݒ�
    [SerializeField] private float initialScale = 2f;
    [SerializeField] private float endScale = 0.5f;
    [SerializeField] private float initialAlpha = 1f;
    [SerializeField] private float endAlpha = 0f;
    [SerializeField] private List<float> durations = new List<float>(); // �e�A�j���[�V�����̕⊮����
    [SerializeField] private List<Color> countdownColors = new List<Color>(); // �e�J�E���g�_�E���̐F

    [SerializeField] private Ease panelFadeEase = Ease.OutCubic; // �p�l���t�F�[�h�̃C�[�W���O�^�C�v
    [SerializeField] private float panelDuration = 1.0f;

    [SerializeField] PlayerImageAnimator[] animator;

    private Vector3 savedInitialScale;

    private void Start()
    {
        // �����̃��[�J���X�P�[����ۑ�
        savedInitialScale = countdownText.transform.localScale;
    }

    // �S�Ă̕⊮���Ԃ����v���ĕԂ����\�b�h
    public float GetTotalDuration()
    {
        float totalDuration = 0f;
        foreach (float duration in durations)
        {
            totalDuration += duration;
        }
        return totalDuration;
    }

    public IEnumerator StartCountdown()
    {
        string[] countdownNumbers = { "3", "2", "1", "Start" };

        // �p�l�����t�F�[�h�A�E�g
        FadeOutPanelAfterCountdown();

        for (int i = 0; i < countdownNumbers.Length; i++)
        {
            countdownText.text = countdownNumbers[i];

            // �e�L�X�g�̐F��ύX
            ChangeTextColor(countdownColors[i]);

            // �⊮���Ԃ����X�g����擾
            float duration = durations[i];

            // �C���f�b�N�X���Ƃ̏������Ăяo��
            ExecuteCountdownAction(i, duration);

            yield return new WaitForSeconds(duration);
        }
        AnimateFade(endAlpha, 0.3f);

    }

    // �C���f�b�N�X�ɉ��������������s
    private void ExecuteCountdownAction(int index, float duration)
    {
        switch (index)
        {
            case 0: // "3"
                ResetScale(initialScale);
                ResetAlpha(initialAlpha);
                AnimateScale(endScale, duration);
                AnimateFade(endAlpha, duration);
                animator[0].StartMovement();
                animator[1].StartMovement();
                break;

            case 1: // "2"
                ResetScale(initialScale);
                ResetAlpha(initialAlpha);
                AnimateScale(endScale, duration);
                AnimateFade(endAlpha, duration);
                break;

            case 2: // "1"
                ResetScale(initialScale);
                ResetAlpha(initialAlpha);
                AnimateScale(endScale, duration);
                AnimateFade(endAlpha, duration);
                GameTurnManager.Instance.IsGameStarted = true;
                break;

            case 3: // "Start"�i�Ō�̃C���f�b�N�X�j
                ResetScale(endScale);
                ResetAlpha(endAlpha);
                AnimateScale(initialScale, duration);
                AnimateFade(initialAlpha, duration);
                animator[0].ChangeSpritesColorToWhite();
                animator[1].ChangeSpritesColor(Color.gray, 0.8f);
                break;

            default:
                break;
        }
    }

    // �X�P�[���̃��Z�b�g
    private void ResetScale(float scaleMultiplier)
    {
        countdownText.transform.localScale = savedInitialScale * scaleMultiplier;
    }

    // �A���t�@�l�̃��Z�b�g
    private void ResetAlpha(float alpha)
    {
        countdownText.alpha = alpha;
    }

    // �X�P�[���A�j���[�V����
    private void AnimateScale(float targetScale, float duration)
    {
        countdownText.transform.DOScale(targetScale, duration);
    }

    private void AnimateFade(float targetAlpha, float duration)
    {
        countdownText.DOFade(targetAlpha, duration); // �A���t�@�l�̕⊮
    }

    // �e�L�X�g�̐F��ύX
    private void ChangeTextColor(Color color)
    {
        countdownText.color = color; // �w�肳�ꂽ�F�ɕύX
    }

    // �S�Ă̕⊮���Ԃ��g����UI�p�l�����t�F�[�h�A�E�g
    public void FadeOutPanelAfterCountdown()
    {
        float totalDuration = GetTotalDuration(); // �S�Ă̕⊮���Ԃ��擾
        uiPanel.DOFade(0f, panelDuration).SetEase(panelFadeEase); // �C�[�W���O��ݒ�
    }
}
