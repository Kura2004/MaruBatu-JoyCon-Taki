using UnityEngine;
using DG.Tweening;

public class PixelationEffect : CameraPostProcessingEffect
{
    [SerializeField]
    private float initialPixelNumberX;
    [SerializeField]
    private float initialPixelNumberY;

    private float animationStartTime;
    private float animationDuration;
    private float targetPixelNumberX;
    private float targetPixelNumberY;
    private bool isAnimating = false;
    private float easingFactor = 9; // ���萔�A�����l�� 9 �ɐݒ�

    // �⊮���@�̗񋓌^
    public enum TransitionType
    {
        Logarithmic,
        Exponential
    }

    private TransitionType currentTransitionType;

    // �V���O���g���̃C���X�^���X
    public static PixelationEffect Instance => SingletonMonoBehaviour<PixelationEffect>.Instance;

    private void OnEnable()
    {
        if (effectMaterials.ContainsKey(EffectType.Pixel))
        {
            Material pixelationMaterial = effectMaterials[EffectType.Pixel];
            initialPixelNumberX = pixelationMaterial.GetFloat("_PixelNumberX");
            initialPixelNumberY = pixelationMaterial.GetFloat("_PixelNumberY");
        }
    }

    private void OnDisable()
    {
        if (effectMaterials.ContainsKey(EffectType.Pixel))
        {
            Material pixelationMaterial = effectMaterials[EffectType.Pixel];
            pixelationMaterial.SetFloat("_PixelNumberX", initialPixelNumberX);
            pixelationMaterial.SetFloat("_PixelNumberY", initialPixelNumberY);
        }
    }

    public void SetPixelation(float pixelNumberX, float pixelNumberY)
    {
        if (effectMaterials.ContainsKey(EffectType.Pixel))
        {
            Material pixelationMaterial = effectMaterials[EffectType.Pixel];
            pixelationMaterial.SetFloat("_PixelNumberX", pixelNumberX);
            pixelationMaterial.SetFloat("_PixelNumberY", pixelNumberY);
        }
    }

    // �⊮���@��I�����ăA�j���[�V�������J�n���郁�\�b�h
    public void StartTransition(float targetX, float targetY, float duration, TransitionType transitionType = TransitionType.Logarithmic, float easing = 9)
    {
        targetPixelNumberX = targetX;
        targetPixelNumberY = targetY;
        animationStartTime = Time.time;
        animationDuration = duration;
        easingFactor = easing;
        currentTransitionType = transitionType;
        isAnimating = true;
    }

    private void Update()
    {
        if (isAnimating)
        {
            float elapsedTime = Time.time - animationStartTime;
            float t = Mathf.Clamp01(elapsedTime / animationDuration);

            if (effectMaterials.ContainsKey(EffectType.Pixel))
            {
                Material pixelationMaterial = effectMaterials[EffectType.Pixel];

                switch (currentTransitionType)
                {
                    case TransitionType.Logarithmic:
                        ApplyLogarithmicTransition(pixelationMaterial, t);
                        break;

                    case TransitionType.Exponential:
                        ApplyExponentialTransition(pixelationMaterial, t);
                        break;
                }
            }

            if (Mathf.Approximately(t, 1f))
            {
                isAnimating = false;
                ToggleEffect(false);
            }
        }
    }

    private void ApplyLogarithmicTransition(Material pixelationMaterial, float t)
    {
        // �ΐ��⊮
        float logT = Mathf.Log10(1 + easingFactor * t) / Mathf.Log10(1 + easingFactor);
        float currentPixelNumberX = Mathf.Lerp(initialPixelNumberX, targetPixelNumberX, logT);
        float currentPixelNumberY = Mathf.Lerp(initialPixelNumberY, targetPixelNumberY, logT);

        pixelationMaterial.SetFloat("_PixelNumberX", currentPixelNumberX);
        pixelationMaterial.SetFloat("_PixelNumberY", currentPixelNumberY);
    }

    private void ApplyExponentialTransition(Material pixelationMaterial, float t)
    {
        // �w���⊮
        float exponentialT = Mathf.Pow(2, easingFactor * (t - 1)) - 0.001f;
        float currentPixelNumberX = Mathf.Lerp(initialPixelNumberX, targetPixelNumberX, exponentialT);
        float currentPixelNumberY = Mathf.Lerp(initialPixelNumberY, targetPixelNumberY, exponentialT);

        pixelationMaterial.SetFloat("_PixelNumberX", currentPixelNumberX);
        pixelationMaterial.SetFloat("_PixelNumberY", currentPixelNumberY);
    }
}
