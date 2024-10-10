using DG.Tweening;
using UnityEngine;

public class RotatingButtonRight : MonoBehaviour
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
    private void OnTriggerStay(Collider other)
    {
        if (IsInteractionBlocked() || !other.CompareTag(selecterTag))
        {
            return;
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup) &&
            Input.GetButtonDown("2P_R1"))
        {
            HandleClickInteraction();
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup) &&
            Input.GetButtonDown("1P_R1"))
        {
            HandleClickInteraction();
        }

        //�e�X�g�p
        if (Input.GetKeyDown(KeyCode.Z))
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
        rotatingManager.StartRotationRight(); // �E��]���J�n
    }
}





