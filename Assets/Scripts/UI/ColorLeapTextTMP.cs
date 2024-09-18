using UnityEngine;
using TMPro; // TextMeshPro ���O���
using DG.Tweening; // DOTween ���O���

public class ColorLerpTextTMP : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMeshPro; // TextMeshProUGUI �R���|�[�l���g
    [SerializeField] private Color startColor = Color.white; // �J�n�F
    [SerializeField] private Color endColor = Color.red; // �I���F
    [SerializeField] private float duration = 1f; // �F���ς��܂ł̎���

    private void Start()
    {
        // DOTween�ŐF�̕�ԃA�j���[�V������ݒ�
        textMeshPro.color = startColor; // �����F��ݒ�
        LoopColor();
    }

    private void LoopColor()
    {
        // �F���I���F�܂ŕ�Ԃ��A������ɋt�����ɕ��
        textMeshPro.DOColor(endColor, duration)
            .SetEase(Ease.Linear) // ���`�⊮
            .SetLoops(-1, LoopType.Yoyo); // �������[�v�ōs�����藈����
    }
}
