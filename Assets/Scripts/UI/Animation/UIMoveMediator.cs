using UnityEngine;

public class UIMoveMediator : MonoBehaviour
{
    [SerializeField] private UIMoveRight moveRight; // �E�ɓ������N���X
    [SerializeField] private UIMoveLeft moveLeft;   // ���ɓ������N���X

    [SerializeField] Color onColor;
    [SerializeField] Color offColor;

    [SerializeField] PlayerImageAnimator animator;

    [SerializeField] bool moveRightNext; // ���ɂǂ���ɓ����������Ǘ�����t���O

    private int toggleCounter = 0;

    private void Start()
    {
        toggleCounter = 0;
    }
    private void LateUpdate()
    {
        if (!GameStateManager.Instance.IsBoardSetupComplete) return;

        var turnMana = GameTurnManager.Instance;
        if (turnMana.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) && GameTurnManager.Instance.IsTurnChanging)
        {
            toggleCounter++;

            if (toggleCounter == 3)
                MoveToggle();
        }

        if (turnMana.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece) && GameTurnManager.Instance.IsTurnChanging)
        {
            toggleCounter++;

            if (toggleCounter == 3)
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
        animator.ChangeSpritesColor(moveRightNext ? offColor : onColor, 0.3f);
        toggleCounter = 0;

    }
}
