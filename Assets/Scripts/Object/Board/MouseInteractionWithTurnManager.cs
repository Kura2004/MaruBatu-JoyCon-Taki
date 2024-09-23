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
        if (colorChanger.isClicked) return;
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
        return colorChanger.isClicked ||
               TimeControllerToggle.isTimeStopped ||
               !stateManager.IsBoardSetupComplete || stateManager.IsRotating;
    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("MassSelecter")) return;

        // SwitchController��A�{�^���������ꂽ���ǂ��������m
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

        //�e�X�g�p
        if (Input.GetKeyDown(KeyCode.Return))
        {
            HandleInteraction();
        }
    }

    private void HandleInteraction()
    {
        if (IsInteractionBlocked()) return;

        ScenesAudio.ClickSe();
        // A�{�^���������ꂽ�ۂɃN���b�N�������Ăяo��
        colorChanger.HandleClick();
        GameTurnManager.Instance.SetTurnChange(true);
        GameTurnManager.Instance.AdvanceTurn(); // �^�[����i�߂�
        UpdateColorBasedOnTurn(); // �F���X�V
        ObjectStateManager.Instance.MoveFirstObjectUpDown(false);
        ObjectStateManager.Instance.MoveSecondObjectUpDown(true);
    }
}