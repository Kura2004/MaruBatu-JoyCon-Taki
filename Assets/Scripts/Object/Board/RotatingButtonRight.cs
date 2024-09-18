using DG.Tweening;
using UnityEngine;

public class RotatingButtonRight : MonoBehaviour
{
    [SerializeField]
    private RotatingMassObjectManager rotatingManager; // 回転処理を管理するマネージャー

    [SerializeField]
    private ObjectColorChanger colorChanger; // 色の変更を管理するコンポーネント

    [SerializeField]
    string selecterTag = "Def";

    private bool IsInteractionBlocked()
    {
        var turnManager = GameTurnManager.Instance;
        return CanvasBounce.isBlocked ||
               turnManager.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) ||
               turnManager.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece) ||
               TimeControllerToggle.isTimeStopped || !GameStateManager.Instance.IsBoardSetupComplete;
    }

    private void Start()
    {
        if (colorChanger == null)
        {
            Debug.LogError("ObjectColorChanger コンポーネントが設定されていません");
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (IsInteractionBlocked() || !rotatingManager.AnyMassClicked())
        {
            return;
        }

        if (other.CompareTag(selecterTag) &&
            GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup) &&
            Input.GetKeyDown((KeyCode)SwitchController.R))
        {
            HandleClickInteraction();
        }

        if (other.CompareTag(selecterTag) &&
    GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup) &&
    Input.GetKeyDown((KeyCode)SwitchController.L))
        {
            HandleClickInteraction();
        }
    }

    private void HandleClickInteraction()
    {
        TimeLimitController.Instance.StopTimer();
        rotatingManager.StartRotationRight(); // 右回転を開始
    }
}





