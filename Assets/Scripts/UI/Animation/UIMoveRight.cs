using UnityEngine;
using DG.Tweening;

public class UIMoveRight : MonoBehaviour
{
    [SerializeField] private RectTransform targetRectTransform; // �������Ώۂ�RectTransform
    [SerializeField] private float moveDistance = 100f;         // ����������
    [SerializeField] private float moveDuration = 2f;           // ����������
    private Tween moveTween;  // �A�j���[�V������Tween��ۑ�����ϐ�

    public void StartMove()
    {
        if (moveTween != null && moveTween.IsPlaying())
        {
            moveTween.Kill();
        }

        // ���݂̈ʒu���擾
        Vector2 currentPos = targetRectTransform.anchoredPosition;

        // �E�����Ɉړ�
        moveTween = targetRectTransform.DOAnchorPosX(currentPos.x + moveDistance, moveDuration)
            .SetEase(Ease.Linear);

        Debug.Log("�E�ɓ������܂���");
    }
}
