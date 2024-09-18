using DG.Tweening;
using UnityEngine;

public class MouseInteractionWithTurnManager : MonoBehaviour
{
    [SerializeField]
    private ObjectColorChanger colorChanger; // ObjectColorChanger �R���|�[�l���g���Q��

    private void Start()
    {
        if (colorChanger == null)
        {
            Debug.LogError("ObjectColorChanger �R���|�[�l���g���ݒ肳��Ă��܂���");
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
        // SwitchController��A�{�^���������ꂽ���ǂ��������m
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
        // A�{�^���������ꂽ�ۂɃN���b�N�������Ăяo��
        //colorChanger.HandleClick();
        GameTurnManager.Instance.SetTurnChange(true);
        GameTurnManager.Instance.AdvanceTurn(); // �^�[����i�߂�
        UpdateColorBasedOnTurn(); // �F���X�V
        ObjectStateManager.Instance.SetFirstObjectActive(false);
        ObjectStateManager.Instance.SetSecondObjectActive(true);
    }
}