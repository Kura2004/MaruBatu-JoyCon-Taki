using UnityEngine;
using DG.Tweening;

public class FadeOutEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 2f; // �A���t�@�l��0�ɂȂ�܂ł̎��ԁi�b�j

    private SpriteRenderer spriteRenderer; // SpriteRenderer�̎Q��

    private void Awake()
    {
        // SpriteRenderer�R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component is missing on the GameObject.");
        }
    }

    /// <summary>
    /// �t�F�[�h�A�E�g���J�n���郁�\�b�h
    /// </summary>
    public void StartFadeOut()
    {
        if (spriteRenderer != null)
        {
            // ���݂̐F���擾
            Color currentColor = spriteRenderer.color;

            // DoTween���g�p���ăA���t�@�l��⊮�I��0�܂Ō���������
            spriteRenderer.DOColor(new Color(currentColor.r, currentColor.g, currentColor.b, 0f), fadeDuration)
                          .SetEase(Ease.Linear);
        }
    }
}