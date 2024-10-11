using DG.Tweening;
using UnityEngine;

public class MainGameOverManager : MonoBehaviour
{
    public static bool loadGameOver = false;
    int GameEndCounter = 0;
    [SerializeField] CanvasFader[] fadeUI;

    private void OnEnable()
    {
        GameEndCounter = 0;
        loadGameOver = false;
        //Invoke(nameof(ExecutePlayerWin), 7.0f);
    }

    private void ExecutePlayerWin()
    {
        MoveHorizontally.Instance.MoveRight();
        VictoryCameraAnimator.Instance.MoveCameraLeftToResetVictory();
        GameWinnerManager.Instance.SetWinner(GameWinnerManager.Winner.Player1);
        ExecuteGameOver();
    }

    private void ExecuteOpponentWin()
    {
        MoveHorizontally.Instance.MoveLeft();
        VictoryCameraAnimator.Instance.MoveCameraRightForVictory();
        GameWinnerManager.Instance.SetWinner(GameWinnerManager.Winner.Player2);
        ExecuteGameOver();
    }

    private void ExecuteDraw()
    {
        GameWinnerManager.Instance.SetWinner(GameWinnerManager.Winner.Draw);
        ExecuteGameOver();
    }

    // �Q�[���I�[�o�[�����s���郁�\�b�h
    private void ExecuteGameOver()
    {
        loadGameOver = true;
        for (int i = 0; i < fadeUI.Length; i++)
            fadeUI[i].HideCanvas();
        GameStateManager.Instance.ResetBoardSetup();
        TimeLimitController.Instance.ResetEffect();
        TimeLimitController.Instance.StopTimer();
        //ScenesAudio.WinSe();
    }

    // �v���C���[�Ƒ���̏�Ԃ��m�F���ăQ�[���I�[�o�[���Ǘ�
    private void Update()
    {
        if (loadGameOver) return; // �Q�[���I�[�o�[�����Ɏ��s����Ă����珈�����Ȃ�
        var gameState = GameStateManager.Instance;

        // �v���C���[�����������ꍇ
        if (gameState.IsPlayerWin)
        {
            ExecutePlayerWin();
            return;
        }

        // ���肪���������ꍇ
        if (gameState.IsOpponentWin)
        {
            ExecuteOpponentWin();
            return;
        }

        if (GameTurnManager.Instance.IsGameEnd())
        {
            GameEndCounter++;
            if (GameEndCounter == 2)
            {
                ExecuteDraw();
                return;
            }
        }
    }

    // ������������Ԃ̏ꍇ�A�Q�[���I�[�o�[�����s
    private void LateUpdate()
    {
        if (loadGameOver) return; // �Q�[���I�[�o�[�����Ɏ��s����Ă����珈�����Ȃ�
        var gameState = GameStateManager.Instance;

        if (gameState.AreBothPlayersWinning())
        {
            ExecuteDraw();
            return;
        }
    }

    // ��A�N�e�B�u�ɂȂ�Ƃ���loadGameOver��false�Ƀ��Z�b�g
    private void OnDisable()
    {
        loadGameOver = false;
    }
}
