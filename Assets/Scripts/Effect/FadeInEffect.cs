using UnityEngine;
using DG.Tweening;

public class FadeInEffect : MonoBehaviour
{
    [SerializeField]
    private float fadeDuration = 2f; // �A���t�@�l��1�ɂȂ�܂ł̎��ԁi�b�j

    private SpriteRenderer spriteRenderer; // SpriteRenderer�̎Q��

    [SerializeField]
    bool OnStart = true;

    private void Start()
    {
        // SpriteRenderer�R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component is missing on the GameObject.");
        }
        else
        {
            // �A���t�@�l��0�ɐݒ肵�Ă���
            Color color = spriteRenderer.color;
            color.a = 0f;
            spriteRenderer.color = color;
        }

        if (OnStart)
            StartFadeIn();
    }

    /// <summary>
    /// �t�F�[�h�C�����J�n���郁�\�b�h
    /// </summary>
    public void StartFadeIn()
    {
        if (spriteRenderer != null)
        {
            // ���݂̐F���擾
            Color currentColor = spriteRenderer.color;

            // DoTween���g�p���ăA���t�@�l��⊮�I��1�܂ő���������
            spriteRenderer.DOColor(new Color(currentColor.r, currentColor.g, currentColor.b, 1f), fadeDuration)
                          .SetEase(Ease.Linear);
        }
    }
}