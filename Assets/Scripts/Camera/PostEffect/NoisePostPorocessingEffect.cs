using UnityEngine;
using DG.Tweening; // DOTween���g�p���邽�߂̖��O���

public class NoisePostProcessingEffect : CameraPostProcessingEffect
{
    private Material noiseMaterial;
    // �V���O���g���̃C���X�^���X
    public static NoisePostProcessingEffect Instance => SingletonMonoBehaviour<NoisePostProcessingEffect>.Instance;
    protected override void Start()
    {
        base.Start();

        // �m�C�Y�G�t�F�N�g�p�̃}�e���A�����擾
        if (effectMaterials.ContainsKey(EffectType.Noise))
        {
            noiseMaterial = effectMaterials[EffectType.Noise];
        }
    }

    protected override void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (applyEffect && currentEffect == EffectType.Noise && noiseMaterial != null)
        {
            ApplyNoiseEffect(source, destination);
        }
        else
        {
            base.OnRenderImage(source, destination);
        }
    }

    private void ApplyNoiseEffect(RenderTexture source, RenderTexture destination)
    {
        noiseMaterial.SetFloat("_OffsetX", Random.Range(0f, 1.2f));
        noiseMaterial.SetFloat("_OffsetY", Random.Range(0f, 1.2f));
        Graphics.Blit(source, destination, noiseMaterial);
    }

    // _Intensity�̒l��DOTween�ŕ⊮�I�Ɍ��������郁�\�b�h
    public void AnimateIntensity(float targetIntensity, float duration)
    {
        if (noiseMaterial != null)
        {
            // DOTween���g����_Intensity�̒l���Ԃ���
            DOTween.To(() => noiseMaterial.GetFloat("_Intensity"),
                       x => noiseMaterial.SetFloat("_Intensity", x),
                       targetIntensity,
                       duration);
        }
    }

    private void OnDisable()
    {
        if (noiseMaterial != null)
        {
            noiseMaterial.SetFloat("_Intensity", 0.98f);
        }
    }
}
