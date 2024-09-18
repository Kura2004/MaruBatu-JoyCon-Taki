using UnityEngine;

public class UIMoveMediator : MonoBehaviour
{
    [SerializeField] private UIMoveRight moveRight; // �E�ɓ������N���X
    [SerializeField] private UIMoveLeft moveLeft;   // ���ɓ������N���X

    [SerializeField] bool moveRightNext; // ���ɂǂ���ɓ����������Ǘ�����t���O
    private void LateUpdate()
    {
        if (!GameStateManager.Instance.IsBoardSetupComplete) return;

        var turnMana = GameTurnManager.Instance;
        if (turnMana.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) && GameTurnManager.Instance.IsTurnChanging)
        {
            MoveToggle();
        }

        if (turnMana.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece) && GameTurnManager.Instance.IsTurnChanging)
        {
            MoveToggle();
        }
    }

    // �E�ɓ�����
    public void MoveRight()
    {
        moveRight.StartMove();
    }

    // ���ɓ�����
    public void MoveLeft()
    {
        moveLeft.StartMove();
    }

    // �E���̐؂�ւ�
    public void MoveToggle()
    {
        if (moveRightNext)
        {
            MoveRight();
        }
        else
        {
            MoveLeft();
        }

        moveRightNext = !moveRightNext; // ����͔��΂̕����ɓ�����
        
    }
}
