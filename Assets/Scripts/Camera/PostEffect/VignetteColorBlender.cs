using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

public class VignetteColorBlender : MonoBehaviour
{
    [SerializeField]
    private PostProcessVolume postProcessVolume; // PostProcessing��Volume�̎Q��

    private Vignette vignette;

    [SerializeField]
    private Color colorA = Color.white; // 1�ڂ̐F
    [SerializeField]
    private Color colorB = Color.black; // 2�ڂ̐F
    [SerializeField, Range(0f, 1f)]
    private float blendFactor = 0.5f;  // �⊮�̊����i0��colorA�A1��colorB�j

    [SerializeField]
    private Color blendedColor; // �⊮���ꂽ�F

    [SerializeField]
    private Material savedMaterial; // �ۑ��p�̃}�e���A��

    private void Start()
    {
        // PostProcessing Volume ���� Vignette �G�t�F�N�g���擾
        if (postProcessVolume.profile.TryGetSettings(out Vignette vignetteEffect))
        {
            vignette = vignetteEffect;
        }
        else
        {
            Debug.LogWarning("Vignette effect is not set in the PostProcessVolume.");
        }

        // �����l�Ƃ��Ē��ԐF��ݒ�
        UpdateVignetteColor();
    }

    private void Update()
    {
        // ���s���ɕ⊮�������ύX���ꂽ�ꍇ�A���ԐF���X�V����
        UpdateVignetteColor();
    }

    private void OnEnable()
    {
        // �ۑ������}�e���A������ colorA, colorB, blendFactor ��������
        if (savedMaterial != null)
        {
            // �}�e���A���̃v���p�e�B����F�ƕ⊮�������擾
            if (savedMaterial.HasProperty("_ColorA"))
            {
                colorA = savedMaterial.GetColor("_ColorA");
            }
            if (savedMaterial.HasProperty("_ColorB"))
            {
                colorB = savedMaterial.GetColor("_ColorB");
            }
            if (savedMaterial.HasProperty("_BlendFactor"))
            {
                blendFactor = savedMaterial.GetFloat("_BlendFactor");
            }

            // �擾�����l�Ɋ�Â��ĐF���X�V
            blendedColor = Color.Lerp(colorA, colorB, blendFactor);

            // Vignette��Color�v���p�e�B�ɓK�p
            if (vignette != null)
            {
                vignette.color.value = blendedColor;
            }
        }
        else
        {
            Debug.LogWarning("Saved Material is not set.");
        }
    }

    private void OnDisable()
    {
        // colorA, colorB, blendFactor, blendedColor ��ۑ��p�̃}�e���A���ɓK�p
        if (savedMaterial != null)
        {
            savedMaterial.SetColor("_ColorA", colorA);
            savedMaterial.SetColor("_ColorB", colorB);
            savedMaterial.SetFloat("_BlendFactor", blendFactor);
            savedMaterial.color = blendedColor; // �ۑ��p�̐F���X�V
        }
        else
        {
            Debug.LogWarning("Saved Material is not set.");
        }
    }

    /// <summary>
    /// 2�F�̒��ԐF��Vignette�ɓK�p���A�C���X�y�N�^�[�ɕ\��
    /// </summary>
    public void UpdateVignetteColor()
    {
        if (vignette != null)
        {
            // Color.Lerp���g�p����2�̐F�̒��ԐF���擾
            blendedColor = Color.Lerp(colorA, colorB, blendFactor);

            // Vignette��Color�v���p�e�B�ɓK�p
            vignette.color.value = blendedColor;
        }
    }
}
