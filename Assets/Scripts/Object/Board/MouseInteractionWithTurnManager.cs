using DG.Tweening;
using UnityEngine;

public class MouseInteractionWithTurnManager : MonoBehaviour
{
    [SerializeField]
    private ObjectColorChanger colorChanger; // ObjectColorChanger コンポーネントを参照

    public bool isClicked { get; private set; } = false;

    private void Start()
    {
        if (colorChanger == null)
        {
            Debug.LogError("ObjectColorChanger コンポーネントが設定されていません");
            return;
        }
        colorChanger.ChangeHoverColor(GlobalColorManager.Instance.currentColor);
        isClicked = false;
    }

    private void LateUpdate()
    {
        if (isClicked) return;
        if (colorChanger.hoverAndClickColor != GlobalColorManager.Instance.currentColor)
        {
            colorChanger.ChangeHoverColor(GlobalColorManager.Instance.currentColor);
        }
    }

    private void UpdateColorBasedOnTurn()
    {
        GlobalColorManager.Instance.UpdateColorBasedOnTurn();
    }

    public bool IsInteractionBlocked()
    {
        var stateManager = GameStateManager.Instance;
        return //TimeControllerToggle.isTimeStopped ||
               !stateManager.IsBoardSetupComplete ||
               stateManager.IsRotating ||
               GameTurnManager.Instance.IsTurnChanging;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MassSelecter") || IsInteractionBlocked()) return;

        // SwitchControllerのAボタンが押されたかどうかを検知
        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece) &&
            Input.GetButtonDown("2P_Decision"))
        {
            HandleInteraction();
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) &&
    Input.GetButtonDown("1P_Decision"))
        {
            HandleInteraction();
        }

        //テスト用
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        if (isClicked)
        {
            ScenesAudio.BlockedSe();
            return;
        }

        isClicked = true;
        ScenesAudio.ClickSe();
        // Aボタンが押された際にクリック処理を呼び出す
        colorChanger.HandleClick();
        GameTurnManager.Instance.SetTurnChange(true);
        GameTurnManager.Instance.AdvanceTurn(); // ターンを進める
        UpdateColorBasedOnTurn(); // 色を更新
        ObjectStateManager.Instance.MoveFirstObjectUpDown(false);
        ObjectStateManager.Instance.MoveSecondObjectUpDown(true);
    }
}