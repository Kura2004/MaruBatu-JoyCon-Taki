
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
        colorChanger.ChangeHoverColor(GlobalColorManager.Instance.currentColor);
    }

    private void UpdateColorBasedOnTurn()
    {
        GlobalColorManager.Instance.UpdateColorBasedOnTurn();
        colorChanger.ChangeHoverColor(GlobalColorManager.Instance.currentColor);
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

    private void OnMouseDown()
    {
        if (IsInteractionBlocked() || colorChanger.isClicked)
        {
            //ScenesAudio.BlockSe();
            return;
        }

        ScenesAudio.ClickSe();
        colorChanger.OnMouseDown();
        GameTurnManager.Instance.SetTurnChange(true);
        GameTurnManager.Instance.AdvanceTurn(); // �^�[����i�߂�
        UpdateColorBasedOnTurn(); // �F���X�V
        //Debug.Log("Current color: " + colorChanger.GetComponent<Renderer>().material.color);
    }
}










