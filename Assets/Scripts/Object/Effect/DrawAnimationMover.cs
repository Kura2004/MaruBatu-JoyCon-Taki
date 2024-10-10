using DG.Tweening;
using UnityEngine;

public class DrawAnimationMover : MonoBehaviour
{
    // 2�̃I�u�W�F�N�g��o�^���邽�߂̃t�B�[���h
    [SerializeField] private Transform player1Object; // �v���C���[1�̃I�u�W�F�N�g
    [SerializeField] private Transform player2Object; // �v���C���[2�̃I�u�W�F�N�g
    [SerializeField] private Transform leftPanel;     // ���p�l��
    [SerializeField] private Transform rightPanel;    // �E�p�l��

    // �A�j���[�V�����̐ݒ�
    [SerializeField] private Vector3 player1OutwardVector = new Vector3(5f, 0f, 0f); // Player1�̍s���̃x�N�g��
    [SerializeField] private Vector3 player1ReturnVector = new Vector3(-3f, 0f, 0f); // Player1�̋A��̃x�N�g��
    [SerializeField] private Vector3 player2OutwardVector = new Vector3(5f, 0f, 0f); // Player2�̍s���̃x�N�g��
    [SerializeField] private Vector3 player2ReturnVector = new Vector3(-3f, 0f, 0f); // Player2�̋A��̃x�N�g��
    [SerializeField] private Vector3 panelCloseVector = new Vector3(-5f, 0f, 0f);   // �p�l������������̃x�N�g��
    [SerializeField] private float outwardDuration = 2f; // �s���̃A�j���[�V�����̎���
    [SerializeField] private float returnDuration = 1.5f; // �A��̃A�j���[�V�����̎���
    [SerializeField] private float panelCloseDuration = 1f; // �p�l��������A�j���[�V�����̎���
    [SerializeField] private Ease startEase = Ease.Linear; // �C�[�W���O�̃^�C�v
    [SerializeField] private Ease returnEase = Ease.Linear; // �A��̃C�[�W���O�̃^�C�v
    [SerializeField] private Ease panelCloseEase = Ease.InOutQuad; // �p�l���̕���A�j���[�V�����̃C�[�W���O

    private bool hasMovedOutward = false;

    /// <summary>
    /// �s���̃A�j���[�V���������s
    /// </summary>
    public void MoveOutward()
    {
        // Player1�̍s���̃A�j���[�V����
        player1Object.DOMove(player1Object.position + player1OutwardVector, outwardDuration)
            .SetEase(startEase);

        // Player2�̍s���̃A�j���[�V����
        player2Object.DOMove(player2Object.position - player2OutwardVector, outwardDuration)
            .SetEase(startEase)
            .OnComplete(() => MoveReturn());

        hasMovedOutward = true;
    }

    /// <summary>
    /// �A��̃A�j���[�V���������s
    /// </summary>
    private void MoveReturn()
    {
        if (!hasMovedOutward) return;

        // Player1�̋A��̃A�j���[�V����
        player1Object.DOMove(player1Object.position + player1ReturnVector, returnDuration)
            .SetEase(returnEase);

        // Player2�̋A��̃A�j���[�V����
        player2Object.DOMove(player2Object.position - player2ReturnVector, returnDuration)
            .SetEase(returnEase)
            .OnComplete(() => ClosePanels());
    }

    /// <summary>
    /// ���E�p�l��������A�j���[�V�������Đ����郁�\�b�h
    /// </summary>
    private void ClosePanels()
    {
        // ���p�l��������A�j���[�V����
        leftPanel.DOLocalMove(leftPanel.localPosition + panelCloseVector, panelCloseDuration)
            .SetEase(panelCloseEase);

        // �E�p�l��������A�j���[�V�����i�t�����j
        rightPanel.DOLocalMove(rightPanel.localPosition - panelCloseVector, panelCloseDuration)
            .SetEase(panelCloseEase)
            .OnComplete(() => ScenesLoader.Instance.LoadGameOver(Color.black));
    }
}
