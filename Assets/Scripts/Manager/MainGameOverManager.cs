using DG.Tweening;
using UnityEngine;

public class MainGameOverManager : MonoBehaviour
{
    public static bool loadGameOver = false;

    private float resetDelay = 0.03f; // ResetBoardSetup���Ăяo���܂ł̒x������

    // �{�[�h���Z�b�g�̏��������\�b�h��
    private void ExecuteResetBoardSetup()
    {
        GameStateManager.Instance.ResetBoardSetup();
        TimeLimitController.Instance.ResetEffect();
    }

    // �Q�[���I�[�o�[�����s���郁�\�b�h
    public void ExecuteGameOver()
    {
        loadGameOver = true;
        TimeLimitController.Instance.StopTimer();
        //ScenesAudio.WinSe();

        // DOTween���g���Ďw�肵���x�����Ԍ�Ƀt���O�𗧂Ă�
        DOTween.Sequence().AppendInterval(resetDelay).AppendCallback(() =>
        {
            ExecuteResetBoardSetup();
        });

        ScenesLoader.Instance.LoadGameOver(1.0f); // GameOver�V�[�������[�h
    }

    // �v���C���[�Ƒ���̏�Ԃ��m�F���ăQ�[���I�[�o�[���Ǘ�
    private void Update()
    {
        if (loadGameOver) return; // �Q�[���I�[�o�[�����Ɏ��s����Ă����珈�����Ȃ�
        var gameState = GameStateManager.Instance;

        // �v���C���[�����������ꍇ
        if (gameState.IsPlayerWin)
        {
            MoveHorizontally.Instance.MoveRight();
            ExecuteGameOver();
            return;
        }

        // ���肪���������ꍇ
        if (gameState.IsOpponentWin)
        {
            MoveHorizontally.Instance.MoveLeft();
            ExecuteGameOver();
            return;
        }
    }

    // ������������Ԃ̏ꍇ�A�Q�[���I�[�o�[�����s
    private void LateUpdate()
    {
        if (loadGameOver) return; // �Q�[���I�[�o�[�����Ɏ��s����Ă����珈�����Ȃ�
        var gameState = GameStateManager.Instance;

        if (gameState.AreBothPlayersWinning() || GameTurnManager.Instance.IsGameEnd())
        {
            ExecuteGameOver();
            return;
        }
    }

    // ��A�N�e�B�u�ɂȂ�Ƃ���loadGameOver��false�Ƀ��Z�b�g
    private void OnDisable()
    {
        loadGameOver = false;
    }
}
