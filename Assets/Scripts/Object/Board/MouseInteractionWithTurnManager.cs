using DG.Tweening;
using UnityEngine;

public class MouseInteractionWithTurnManager : MonoBehaviour
{
    [SerializeField]
    private ObjectColorChanger colorChanger; // ObjectColorChanger コンポーネントを参照

    private void Start()
    {
        if (colorChanger == null)
        {
            Debug.LogError("ObjectColorChanger コンポーネントが設定されていません");
            return;
        }
        colorChanger.ChangeHoverColor(GlobalColorManager.Instance.currentColor);
    }

    private void LateUpdate()
    {
        if (colorChanger.hoverAndClickColor != GlobalColorManager.Instance.currentColor)
            colorChanger.ChangeHoverColor(GlobalColorManager.Instance.currentColor);
    }

    private void UpdateColorBasedOnTurn()
    {
        GlobalColorManager.Instance.UpdateColorBasedOnTurn();
    }

    public static bool IsInteractionBlocked()
    {
        var turnManager = GameTurnManager.Instance;
        var stateManager = GameStateManager.Instance;
        return CanvasBounce.isBlocked ||
               turnManager.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup) ||
               turnManager.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup) ||
               TimeControllerToggle.isTimeStopped ||
               (!stateManager.IsBoardSetupComplete && !stateManager.IsRotating);
    }

    private void Update()
    {
        // SwitchControllerのAボタンが押されたかどうかを検知
        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece) &&
            Input.GetKeyDown((KeyCode)SwitchController.R))
        {
            HandleInteraction();
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) &&
    Input.GetKeyDown((KeyCode)SwitchController.L))
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        if (IsInteractionBlocked() || colorChanger.isClicked)
        {
            //ScenesAudio.BlockSe();
            return;
        }

        ScenesAudio.ClickSe();
        // Aボタンが押された際にクリック処理を呼び出す
        //colorChanger.HandleClick();
        GameTurnManager.Instance.SetTurnChange(true);
        GameTurnManager.Instance.AdvanceTurn(); // ターンを進める
        UpdateColorBasedOnTurn(); // 色を更新
        ObjectStateManager.Instance.SetFirstObjectActive(false);
        ObjectStateManager.Instance.SetSecondObjectActive(true);
    }
}