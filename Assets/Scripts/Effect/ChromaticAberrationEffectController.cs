using UnityEngine;
using UnityEngine.Rendering.PostProcessing;  // Post-processing�̖��O���
using DG.Tweening;  // DoTween�̖��O���

public class ChromaticAberrationEffectController : SingletonMonoBehaviour<ChromaticAberrationEffectController>
{
    [Header("Chromatic Aberration Effect Settings")]
    [SerializeField] private PostProcessVolume postProcessVolume; // �C���X�y�N�^�[�Őݒ肷��PostProcessVolume
    [SerializeField] private float maxIntensity = 1f; // �C���X�y�N�^�[�ŕҏW�\�ȍő�Intensity
    [SerializeField] private float increaseDuration = 2f; // maxIntensity�Ɍ������A�j���[�V�����̎��ԁi�b�j
    [SerializeField] private float decreaseDuration = 2f; // ���ɖ߂�A�j���[�V�����̎��ԁi�b�j
    [SerializeField] private float waitBetweenAnimations = 1f; // �A�j���[�V�����̊Ԃ̑ҋ@���ԁi�b�j
    [SerializeField] private AnimationType increaseType = AnimationType.Exponential; // �����̃A�j���[�V�����^�C�v
    [SerializeField] private AnimationType decreaseType = AnimationType.Logarithmic; // �����̃A�j���[�V�����^�C�v

    private ChromaticAberration chromaticAberration;
    private bool isEffectActive = false;
    private Tween currentTween;

    public enum AnimationType
    {
        Linear,
        Exponential,
        Logarithmic
    }

    // ������
    private void Start()
    {
        if (postProcessVolume != null)
        {
            // ChromaticAberration�̐ݒ���擾
            if (!postProcessVolume.profile.TryGetSettings(out chromaticAberration))
            {
                Debug.LogError("PostProcessVolume��Chromatic Aberration�ݒ肪������܂���B�������v���t�@�C�����ݒ肳��Ă��邩�m�F���Ă��������B");
            }
        }
        else
        {
            Debug.LogError("PostProcessVolume���ݒ肳��Ă��܂���B�C���X�y�N�^�[�Őݒ肵�Ă��������B");
        }
        StopChromaticAberrationEffect();  // ������ԂŃG�t�F�N�g���~
    }

    // �G�t�F�N�g��On/Off��؂�ւ��郁�\�b�h
    public void ToggleChromaticAberrationEffect()
    {
        if (isEffectActive)
        {
            StopChromaticAberrationEffect();
        }
        else
        {
            StartChromaticAberrationEffect();
        }
    }

    // �G�t�F�N�g���J�n����
    public void StartChromaticAberrationEffect()
    {
        if (chromaticAberration != null && !isEffectActive)
        {
            isEffectActive = true;
            AnimateChromaticAberrationEffect();
        }
    }

    // �G�t�F�N�g���~����
    public void StopChromaticAberrationEffect()
    {
        if (chromaticAberration != null)
        {
            isEffectActive = false;
            if (currentTween != null)
            {
                currentTween.Kill();  // ���݂�Tween���~
                currentTween = null;
            }
            chromaticAberration.intensity.value = 0;  // Intensity�����Z�b�g
            Debug.Log("�G�t�F�N�g���~���܂���");
        }
    }

    // Chromatic Aberration��Intensity���A�j���[�V�����ŉi���I�ɑ���������
    private void AnimateChromaticAberrationEffect()
    {
        Sequence sequence = DOTween.Sequence();

        // Intensity��maxIntensity�Ɍ������đ���������A�j���[�V����
        sequence.Append(AnimateToValue(maxIntensity, increaseDuration, increaseType));

        // �A�j���[�V�����̊Ԃ̑ҋ@����
        sequence.AppendInterval(waitBetweenAnimations);

        // Intensity��0�Ɍ������Č���������A�j���[�V����
        sequence.Append(AnimateToValue(0, decreaseDuration, decreaseType));

        // ���[�v������
        sequence.SetLoops(-1);

        // ���݂�Tween��ێ����Ă���
        currentTween = sequence;
    }

    // Intensity��ڕW�l�܂ŃA�j���[�V����������
    private Tween AnimateToValue(float targetValue, float duration, AnimationType animationType)
    {
        Ease easeType = Ease.Linear;
        switch (animationType)
        {
            case AnimationType.Exponential:
                easeType = Ease.InQuad;  // �����A�j���[�V����
                break;
            case AnimationType.Logarithmic:
                easeType = Ease.OutQuad;  // �����A�j���[�V����
                break;
            case AnimationType.Linear:
            default:
                easeType = Ease.Linear;
                break;
        }

        return DOTween.To(() => chromaticAberration.intensity.value, x => chromaticAberration.intensity.value = x, targetValue, duration)
                      .SetEase(easeType);
    }
}
