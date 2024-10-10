using UnityEngine;
using System.Collections;

public class GameStateManager : SingletonMonoBehaviour<GameStateManager>
{
    // ��]��Ԃ������v���p�e�B
    public bool IsRotating { get; private set; } = false;

    public bool IsBoardSetupComplete { get; private set; } = false;

    // �v���C���[�̏������
    public bool IsPlayerWin { get; private set; } = false;

    // ����̏������
    public bool IsOpponentWin { get; private set; } = false;

    protected override void OnEnable()
    {
        base.OnEnable();
        IsRotating = false;
        IsBoardSetupComplete = false;
        IsPlayerWin = false;
        IsOpponentWin = false;
    }

    // ��]���J�n���郁�\�b�h
    public void StartRotation()
    {
        Debug.Log("��]���n�߂܂�");
        IsRotating = true;
    }

    // ��]���I�����郁�\�b�h
    public void EndRotation()
    {
        IsRotating = false;
    }

    // �ՖʃZ�b�g�����̃t���O���X�V���郁�\�b�h
    private void SetBoardSetupComplete(bool isComplete)
    {
        IsBoardSetupComplete = isComplete;

        Debug.Log("Board setup complete status: " + IsBoardSetupComplete);
    }

    // �ՖʃZ�b�g���J�n���郁�\�b�h�i�����Ƃ��ăZ�b�g�A�b�v�����܂ł̕b�����󂯎��j
    public void StartBoardSetup(float setupDuration)
    {
        ScenesAudio.UnPauseBgm();
        StartCoroutine(BoardSetupCoroutine(setupDuration));
    }

    // �R���[�`���Ŕ񓯊��ɔՖʃZ�b�g�A�b�v�������s��
    private IEnumerator BoardSetupCoroutine(float setupDuration)
    {
        Debug.Log("Starting board setup...");

        yield return new WaitForSeconds(setupDuration);

        TimeLimitController.Instance.StartTimer();
        SetBoardSetupComplete(true);
    }

    // �ՖʃZ�b�g�����t���O�����Z�b�g���郁�\�b�h
    public void ResetBoardSetup()
    {
        IsBoardSetupComplete = false;
        Debug.Log("Board setup has been reset.");
    }

    // �v���C���[�̏�����Ԃ�ݒ肷�郁�\�b�h
    public void SetPlayerWin(bool isWin)
    {
        IsPlayerWin = isWin;
        Debug.Log("Player win status: " + IsPlayerWin);
    }

    // ����̏�����Ԃ�ݒ肷�郁�\�b�h
    public void SetOpponentWin(bool isWin)
    {
        IsOpponentWin = isWin;
        Debug.Log("Opponent win status: " + IsOpponentWin);
    }

    // ���҂����������ꍇ�� true ��Ԃ����\�b�h
    public bool AreBothPlayersWinning()
    {
        return IsPlayerWin && IsOpponentWin;
    }
}
