using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class RotatingButtonLeft : MonoBehaviour
{
    [SerializeField]
    private RotatingMassObjectManager rotatingManager; // ��]�������Ǘ�����}�l�[�W���[

    [SerializeField]
    private ObjectColorChanger colorChanger; // �F�̕ύX���Ǘ�����R���|�[�l���g

    [SerializeField]
    string selecterTag = "Def";

    private bool IsInteractionBlocked()
    {
        return CanvasBounce.isBlocked ||
               TimeControllerToggle.isTimeStopped 
               || !GameStateManager.Instance.IsBoardSetupComplete;

    }

    private void Start()
    {
        if (colorChanger == null)
        {
            Debug.LogError("ObjectColorChanger �R���|�[�l���g���ݒ肳��Ă��܂���");
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
        rotatingManager.StartRotationLeft(); // ����]���J�n
    }
}



