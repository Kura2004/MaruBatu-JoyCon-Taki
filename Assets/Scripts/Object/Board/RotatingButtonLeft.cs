using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingButtonLeft : MonoBehaviour
{
    [SerializeField]
    private RotatingMassObjectManager rotatingManager; // ��]�������Ǘ�����}�l�[�W���[

    [SerializeField]
    string selecterTag = "Def";

    private bool IsInteractionBlocked()
    {
        return CanvasBounce.isBlocked ||
               TimeControllerToggle.isTimeStopped ||
               !GameStateManager.Instance.IsBoardSetupComplete;

    }

    private void Start()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        if (IsInteractionBlocked() || !other.CompareTag(selecterTag))
        {
            return;
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup) &&
            Input.GetButtonDown("2P_L1"))
        {
            HandleClickInteraction();
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup) &&
            Input.GetButtonDown("1P_L1"))
        {
            HandleClickInteraction();
        }

        //�e�X�g�p
        if (Input.GetKeyDown(KeyCode.X))
        {
            HandleClickInteraction();
        }
    }

    private void HandleClickInteraction()
    {
        if (!rotatingManager.AnyMassClicked() ||
!rotatingManager.isSelected)
        {
            ScenesAudio.BlockedSe();
            return;
        }

        TimeLimitController.Instance.StopTimer();
        rotatingManager.StartRotationLeft(); // ����]���J�n
    }
}



