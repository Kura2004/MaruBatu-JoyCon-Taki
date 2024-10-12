using UnityEngine;
using DG.Tweening;

public class KeyInputMover : MonoBehaviour
{
    [SerializeField]
    private float horizontalMoveDistance = 5f; // �������̈ړ�����

    [SerializeField]
    private float verticalMoveDistance = 5f; // �c�����̈ړ�����

    [SerializeField]
    private float moveDuration = 1f; // �ړ��ɂ����鎞��

    [SerializeField]
    private Transform boundsOrigin; // �͈͂̌��_�i�C���X�y�N�^�[�Őݒ�j
    [SerializeField]
    private Vector3 boundsSize = new Vector3(10f, 0f, 10f); // �͈͂̍L���i�C���X�y�N�^�[�Őݒ�\�j

    private bool isMoving = false; // �ړ������ǂ����̃t���O

    [SerializeField] bool onDebug = false;

    void Update()
    {
        if (isMoving || !GameStateManager.Instance.IsBoardSetupComplete ||
            GameStateManager.Instance.IsRotating) return;

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece) ||
            GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup))
        {
            Handle2PInput();
            if (onDebug)
            {
                debugInput();
            }
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) ||
            GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup))
        {
            Handle1PInput();
            if (onDebug)
            {
                debugInput();
            }
        }
    }
    // Input.GetAxis���g�p���Ĉړ�����
    private void Handle1PInput()
    {
        // Input.GetAxis�ŉ����Əc���̓��͂��擾
        float horizontalInput = Input.GetAxis("1P_Select_X") * 10;
        float verticalInput = Input.GetAxis("1P_Select_Y") * 10;

        if (Mathf.Abs(horizontalInput) < 0.5f)
        {
            horizontalInput = 0;
        }

        if (Mathf.Abs(verticalInput) < 0.5f)
        {
            verticalInput = 0;
        }

        if (horizontalInput == 0 && verticalInput == 0) return;

        // �������Əc�����̓��͂̐�Βl���r
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            // �������̈ړ����D�悳���
            Vector3 moveDirection = (horizontalInput > 0) ? Vector3.right * horizontalMoveDistance : Vector3.left * horizontalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
        else
        {
            // �c�����̈ړ����D�悳���
            Vector3 moveDirection = (verticalInput > 0) ? Vector3.forward * verticalMoveDistance : Vector3.back * verticalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
    }

    private void Handle2PInput()
    {
        // Input.GetAxis�ŉ����Əc���̓��͂��擾
        float horizontalInput = Input.GetAxis("2P_Select_X") * 10;
        float verticalInput = Input.GetAxis("2P_Select_Y") * 10;

        if (Mathf.Abs(horizontalInput) < 0.5f)
        {
            horizontalInput = 0;
        }

        if (Mathf.Abs(verticalInput) < 0.5f)
        {
            verticalInput = 0;
        }

        if (horizontalInput == 0 && verticalInput == 0) return;

        // �������Əc�����̓��͂̐�Βl���r
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            // �������̈ړ����D�悳���
            Vector3 moveDirection = (horizontalInput > 0) ? Vector3.right * horizontalMoveDistance : Vector3.left * horizontalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
        else
        {
            // �c�����̈ړ����D�悳���
            Vector3 moveDirection = (verticalInput > 0) ? Vector3.forward * verticalMoveDistance : Vector3.back * verticalMoveDistance;
            Debug.Log("�c����" +  verticalInput);
            TryMoveInDirection(moveDirection);
        }
    }

    private void debugInput()
    {
        Vector2 debugInput = new Vector2(Input.GetAxis("Horizontal"),
    Input.GetAxis("Vertical"));

        if (Mathf.Abs(debugInput.x) < 0.1f)
        {
            debugInput.x = 0;
        }

        if (Mathf.Abs(debugInput.y) < 0.1f)
        {
            debugInput.y = 0;
        }

        if (debugInput.x == 0 && debugInput.y == 0) return;

        // �������Əc�����̓��͂̐�Βl���r
        if (Mathf.Abs(debugInput.x) > Mathf.Abs(debugInput.y))
        {
            // �������̈ړ����D�悳���
            Vector3 moveDirection = (debugInput.x > 0) ? Vector3.right * horizontalMoveDistance : Vector3.left * horizontalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
        else
        {
            // �c�����̈ړ����D�悳���
            Vector3 moveDirection = (debugInput.y > 0) ? Vector3.forward * verticalMoveDistance : Vector3.back * verticalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
    }

    // �ړ��͈͂��m�F���āA�͈͓��Ȃ�ړ�����
    private void TryMoveInDirection(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction;

        // �ړ���̍��W���͈͊O�Ȃ�ړ����Ȃ�
        if (IsOutOfBounds(targetPosition))
        {
            Debug.Log("�ړ��悪�͈͊O�ł�: " + targetPosition);
            return;
        }

        // �ړ��������J�n
        isMoving = true; // �ړ����t���O�𗧂Ă�
        transform.DOMove(targetPosition, moveDuration).OnComplete(() => isMoving = false); // �ړ��������Ƀt���O������
    }



    // �ړ��悪�͈͊O���ǂ������`�F�b�N
    private bool IsOutOfBounds(Vector3 targetPosition)
    {
        if (boundsOrigin == null)
        {
            Debug.LogError("Bounds�̌��_���ݒ肳��Ă��܂���");
            return true;
        }

        // Bounds�̒��S�����_�Ƃ��A�͈͂��`�F�b�N����
        Vector3 boundsCenter = boundsOrigin.position;
        Vector3 halfSize = boundsSize / 2f;

        // �e���Ŕ͈͊O���ǂ����𔻒�
        bool isOutOfX = targetPosition.x < boundsCenter.x - halfSize.x || targetPosition.x > boundsCenter.x + halfSize.x;
        bool isOutOfY = targetPosition.y < boundsCenter.y - halfSize.y || targetPosition.y > boundsCenter.y + halfSize.y;
        bool isOutOfZ = targetPosition.z < boundsCenter.z - halfSize.z || targetPosition.z > boundsCenter.z + halfSize.z;

        return isOutOfX || isOutOfY || isOutOfZ;
    }
    /*
    [SerializeField]
    private float horizontalMoveDistance = 5f; // �������̈ړ�����

    [SerializeField]
    private float verticalMoveDistance = 5f; // �c�����̈ړ�����

    [SerializeField]
    private float moveDuration = 1f; // �ړ��ɂ����鎞��

    [SerializeField]
    private Transform boundsOrigin; // �͈͂̌��_�i�C���X�y�N�^�[�Őݒ�j
    [SerializeField]
    private Vector3 boundsSize = new Vector3(10f, 0f, 10f); // �͈͂̍L���i�C���X�y�N�^�[�Őݒ�\�j

    private bool isMoving = false; // �ړ������ǂ����̃t���O

    void Update()
    {
        if (isMoving) return; // �ړ����͓��͂𖳎�

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece) ||
            GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup))
        {
            HandleABXYInput();
        }

        if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) ||
            GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup))
        {
            HandleArrowInput();
        }
    }

    // ABXY�{�^���ł̈ړ�����
    private void HandleABXYInput()
    {
        if (Input.GetKeyDown((KeyCode)SwitchController.A))
        {
            // A�{�^���������ꂽ�牡�����Ɉړ�
            TryMoveInDirection(Vector3.right * horizontalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.B))
        {
            // B�{�^���������ꂽ��c�����Ɉړ�
            TryMoveInDirection(Vector3.back * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.X))
        {
            // X�{�^���������ꂽ��c�����Ɉړ�
            TryMoveInDirection(Vector3.forward * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.Y))
        {
            // Y�{�^���������ꂽ�牡�����Ɉړ�
            TryMoveInDirection(Vector3.left * horizontalMoveDistance);
        }
    }

    // Arrow�n�{�^���ł̈ړ�����
    private void HandleArrowInput()
    {
        if (Input.GetKeyDown((KeyCode)SwitchController.UpArrow))
        {
            // ����L�[�������ꂽ��c�����Ɉړ�
            TryMoveInDirection(Vector3.forward * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.DownArrow))
        {
            // �����L�[�������ꂽ��c�����Ɉړ�
            TryMoveInDirection(Vector3.back * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.LeftArrow))
        {
            // �����L�[�������ꂽ�牡�����Ɉړ�
            TryMoveInDirection(Vector3.left * horizontalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.RightArrow))
        {
            // �E���L�[�������ꂽ�牡�����Ɉړ�
            TryMoveInDirection(Vector3.right * horizontalMoveDistance);
        }
    }

    // �ړ��͈͂��m�F���āA�͈͓��Ȃ�ړ�����
    private void TryMoveInDirection(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction;

        // �ړ���̍��W���͈͊O�Ȃ�ړ����Ȃ�
        if (IsOutOfBounds(targetPosition))
        {
            Debug.Log("�ړ��悪�͈͊O�ł�: " + targetPosition);
            return;
        }

        // �ړ��������J�n
        isMoving = true; // �ړ����t���O�𗧂Ă�
        transform.DOMove(targetPosition, moveDuration).OnComplete(() => isMoving = false); // �ړ��������Ƀt���O������
    }

    // �ړ��悪�͈͊O���ǂ������`�F�b�N
    private bool IsOutOfBounds(Vector3 targetPosition)
    {
        if (boundsOrigin == null)
        {
            Debug.LogError("Bounds�̌��_���ݒ肳��Ă��܂���");
            return true;
        }

        // Bounds�̒��S�����_�Ƃ��A�͈͂��`�F�b�N����
        Vector3 boundsCenter = boundsOrigin.position;
        Vector3 halfSize = boundsSize / 2f;

        // �e���Ŕ͈͊O���ǂ����𔻒�
        bool isOutOfX = targetPosition.x < boundsCenter.x - halfSize.x || targetPosition.x > boundsCenter.x + halfSize.x;
        bool isOutOfY = targetPosition.y < boundsCenter.y - halfSize.y || targetPosition.y > boundsCenter.y + halfSize.y;
        bool isOutOfZ = targetPosition.z < boundsCenter.z - halfSize.z || targetPosition.z > boundsCenter.z + halfSize.z;

        return isOutOfX || isOutOfY || isOutOfZ;
    }
    */
}
