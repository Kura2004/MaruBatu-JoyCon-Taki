using UnityEngine;
using DG.Tweening; // DOTween�̖��O���

public class SpriteFader : MonoBehaviour
{
    [Header("Fade Settings")]
    [SerializeField] private float fadeDuration = 1f; // �t�F�[�h�̎�������
    [SerializeField] private float fadeInAlpha = 1f; // �t�F�[�h�C�����̖ڕW�����x
    [SerializeField] private float fadeOutAlpha = 0f; // �t�F�[�h�A�E�g���̖ڕW�����x

    private SpriteRenderer spriteRenderer; // SpriteRenderer�R���|�[�l���g
    private bool isFadedIn = true; // ���݂̓����x��Ԃ�ǐՂ���t���O

    private void OnEnable()
    {
        // SpriteRenderer�R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();
        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component is missing on this GameObject.");
        }
    }

    /// <summary>
    /// SpriteRenderer�̓����x���w�肵���l�Ƀt�F�[�h���郁�\�b�h
    /// </summary>
    public void FadeTo(float targetAlpha, float duration)
    {
        if (spriteRenderer != null)
        {
            Color currentColor = spriteRenderer.color; // ���݂̐F���擾
            Color targetColor = new Color(currentColor.r, currentColor.g, currentColor.b, targetAlpha); // �ڕW�F��ݒ�

            spriteRenderer.DOColor(targetColor, duration).OnStart(() =>
            {
                Debug.Log($"Fading to alpha {targetAlpha} over {duration} seconds.");
            }).OnComplete(() =>
            {
                Debug.Log("Fade complete.");
            });
        }
    }

    /// <summary>
    /// �f�t�H���g�ݒ�œ����x��⊮�I�ɕω������郁�\�b�h
    /// </summary>
    public void FadeToDefault()
    {
        FadeTo(fadeInAlpha, fadeDuration);
    }

    /// <summary>
    /// ���݂̓����x��Ԃɉ����āA�t�F�[�h�C���܂��̓t�F�[�h�A�E�g��؂�ւ��郁�\�b�h
    /// </summary>
    public void ToggleFade()
    {
        if (spriteRenderer != null)
        {
            if (isFadedIn)
            {
                FadeTo(fadeOutAlpha, fadeDuration); // �t�F�[�h�A�E�g
                var cameraHandle = CameraSwitcher.CameraType.Main;
                CameraSwitcher.Instance.SwitchCamera(cameraHandle);
            }
            else
            {
                FadeTo(fadeInAlpha, fadeDuration); // �t�F�[�h�C��
                var cameraHandle = CameraSwitcher.CameraType.Secondary;
                CameraSwitcher.Instance.SwitchCamera(cameraHandle);
            }

            isFadedIn = !isFadedIn; // ��Ԃ𔽓]
        }
    }
}

