using UnityEngine;
using UnityEngine.Rendering.PostProcessing;  // Post-processing�̖��O���
using DG.Tweening;  // DoTween�̖��O���

public class VignetteEffectController : SingletonMonoBehaviour<VignetteEffectController>
{
    [Header("Vignette Effect Settings")]
    [SerializeField] private PostProcessVolume postProcessVolume; // �C���X�y�N�^�[�Őݒ肷��PostProcessVolume
    [SerializeField] private float maxIntensity = 1f; // �C���X�y�N�^�[�ŕҏW�\�ȍő�Intensity
    [SerializeField] private float increaseDuration = 2f; // maxIntensity�Ɍ������A�j���[�V�����̎��ԁi�b�j
    [SerializeField] private float decreaseDuration = 2f; // ���ɖ߂�A�j���[�V�����̎��ԁi�b�j
    [SerializeField] private float waitDuration = 1f; // �A�j���[�V�����Ԃ̑ҋ@���ԁi�b�j
    [SerializeField] private AnimationType increaseType = AnimationType.Exponential; // �����̃A�j���[�V�����^�C�v
    [SerializeField] private AnimationType decreaseType = AnimationType.Logarithmic; // �����̃A�j���[�V�����^�C�v

    private Vignette vignette;
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
            // Vignette�̐ݒ���擾
            if (!postProcessVolume.profile.TryGetSettings(out vignette))
            {
                Debug.LogError("PostProcessVolume��Vignette�ݒ肪������܂���B�������v���t�@�C�����ݒ肳��Ă��邩�m�F���Ă��������B");
            }
        }
        else
        {
            Debug.LogError("PostProcessVolume���ݒ肳��Ă��܂���B�C���X�y�N�^�[�Őݒ肵�Ă��������B");
        }
        StopVignetteEffect();  // ������ԂŃG�t�F�N�g���~
    }

    // �G�t�F�N�g���J�n����
    public void StartVignetteEffect()
    {
        if (vignette != null && !isEffectActive)
        {
            isEffectActive = true;
            AnimateVignetteEffect();
        }
    }

    // �G�t�F�N�g���~����
    public void StopVignetteEffect()
    {
        if (vignette != null)
        {
            isEffectActive = false;
            if (currentTween != null)
            {
                currentTween.Kill();  // ���݂�Tween���~
                currentTween = null;
            }
            vignette.intensity.value = 0;  // Intensity�����Z�b�g
            Debug.Log("�G�t�F�N�g���~���܂���");
        }
    }

    // Vignette��Intensity���A�j���[�V�����ŉi���I�ɑ���������
    private void AnimateVignetteEffect()
    {
        Sequence sequence = DOTween.Sequence();

        // Intensity��maxIntensity�Ɍ������đ���������A�j���[�V����
        sequence.Append(AnimateToValue(maxIntensity, increaseDuration, increaseType));

        // �A�j���[�V�����̊Ԃɑҋ@
        sequence.AppendInterval(waitDuration);

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

        return DOTween.To(() => vignette.intensity.value, x => vignette.intensity.value = x, targetValue, duration)
                      .SetEase(easeType);
    }
}
