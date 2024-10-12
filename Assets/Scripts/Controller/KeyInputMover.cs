using UnityEngine;
using DG.Tweening;

public class KeyInputMover : MonoBehaviour
{
    [SerializeField]
    private float horizontalMoveDistance = 5f; // 横方向の移動距離

    [SerializeField]
    private float verticalMoveDistance = 5f; // 縦方向の移動距離

    [SerializeField]
    private float moveDuration = 1f; // 移動にかかる時間

    [SerializeField]
    private Transform boundsOrigin; // 範囲の原点（インスペクターで設定）
    [SerializeField]
    private Vector3 boundsSize = new Vector3(10f, 0f, 10f); // 範囲の広さ（インスペクターで設定可能）

    private bool isMoving = false; // 移動中かどうかのフラグ

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
    // Input.GetAxisを使用して移動処理
    private void Handle1PInput()
    {
        // Input.GetAxisで横軸と縦軸の入力を取得
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

        // 横方向と縦方向の入力の絶対値を比較
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            // 横方向の移動が優先される
            Vector3 moveDirection = (horizontalInput > 0) ? Vector3.right * horizontalMoveDistance : Vector3.left * horizontalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
        else
        {
            // 縦方向の移動が優先される
            Vector3 moveDirection = (verticalInput > 0) ? Vector3.forward * verticalMoveDistance : Vector3.back * verticalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
    }

    private void Handle2PInput()
    {
        // Input.GetAxisで横軸と縦軸の入力を取得
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

        // 横方向と縦方向の入力の絶対値を比較
        if (Mathf.Abs(horizontalInput) > Mathf.Abs(verticalInput))
        {
            // 横方向の移動が優先される
            Vector3 moveDirection = (horizontalInput > 0) ? Vector3.right * horizontalMoveDistance : Vector3.left * horizontalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
        else
        {
            // 縦方向の移動が優先される
            Vector3 moveDirection = (verticalInput > 0) ? Vector3.forward * verticalMoveDistance : Vector3.back * verticalMoveDistance;
            Debug.Log("縦方向" +  verticalInput);
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

        // 横方向と縦方向の入力の絶対値を比較
        if (Mathf.Abs(debugInput.x) > Mathf.Abs(debugInput.y))
        {
            // 横方向の移動が優先される
            Vector3 moveDirection = (debugInput.x > 0) ? Vector3.right * horizontalMoveDistance : Vector3.left * horizontalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
        else
        {
            // 縦方向の移動が優先される
            Vector3 moveDirection = (debugInput.y > 0) ? Vector3.forward * verticalMoveDistance : Vector3.back * verticalMoveDistance;
            TryMoveInDirection(moveDirection);
        }
    }

    // 移動範囲を確認して、範囲内なら移動する
    private void TryMoveInDirection(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction;

        // 移動後の座標が範囲外なら移動しない
        if (IsOutOfBounds(targetPosition))
        {
            Debug.Log("移動先が範囲外です: " + targetPosition);
            return;
        }

        // 移動処理を開始
        isMoving = true; // 移動中フラグを立てる
        transform.DOMove(targetPosition, moveDuration).OnComplete(() => isMoving = false); // 移動完了時にフラグを解除
    }



    // 移動先が範囲外かどうかをチェック
    private bool IsOutOfBounds(Vector3 targetPosition)
    {
        if (boundsOrigin == null)
        {
            Debug.LogError("Boundsの原点が設定されていません");
            return true;
        }

        // Boundsの中心を原点とし、範囲をチェックする
        Vector3 boundsCenter = boundsOrigin.position;
        Vector3 halfSize = boundsSize / 2f;

        // 各軸で範囲外かどうかを判定
        bool isOutOfX = targetPosition.x < boundsCenter.x - halfSize.x || targetPosition.x > boundsCenter.x + halfSize.x;
        bool isOutOfY = targetPosition.y < boundsCenter.y - halfSize.y || targetPosition.y > boundsCenter.y + halfSize.y;
        bool isOutOfZ = targetPosition.z < boundsCenter.z - halfSize.z || targetPosition.z > boundsCenter.z + halfSize.z;

        return isOutOfX || isOutOfY || isOutOfZ;
    }
    /*
    [SerializeField]
    private float horizontalMoveDistance = 5f; // 横方向の移動距離

    [SerializeField]
    private float verticalMoveDistance = 5f; // 縦方向の移動距離

    [SerializeField]
    private float moveDuration = 1f; // 移動にかかる時間

    [SerializeField]
    private Transform boundsOrigin; // 範囲の原点（インスペクターで設定）
    [SerializeField]
    private Vector3 boundsSize = new Vector3(10f, 0f, 10f); // 範囲の広さ（インスペクターで設定可能）

    private bool isMoving = false; // 移動中かどうかのフラグ

    void Update()
    {
        if (isMoving) return; // 移動中は入力を無視

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

    // ABXYボタンでの移動処理
    private void HandleABXYInput()
    {
        if (Input.GetKeyDown((KeyCode)SwitchController.A))
        {
            // Aボタンが押されたら横方向に移動
            TryMoveInDirection(Vector3.right * horizontalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.B))
        {
            // Bボタンが押されたら縦方向に移動
            TryMoveInDirection(Vector3.back * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.X))
        {
            // Xボタンが押されたら縦方向に移動
            TryMoveInDirection(Vector3.forward * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.Y))
        {
            // Yボタンが押されたら横方向に移動
            TryMoveInDirection(Vector3.left * horizontalMoveDistance);
        }
    }

    // Arrow系ボタンでの移動処理
    private void HandleArrowInput()
    {
        if (Input.GetKeyDown((KeyCode)SwitchController.UpArrow))
        {
            // 上矢印キーが押されたら縦方向に移動
            TryMoveInDirection(Vector3.forward * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.DownArrow))
        {
            // 下矢印キーが押されたら縦方向に移動
            TryMoveInDirection(Vector3.back * verticalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.LeftArrow))
        {
            // 左矢印キーが押されたら横方向に移動
            TryMoveInDirection(Vector3.left * horizontalMoveDistance);
        }
        else if (Input.GetKeyDown((KeyCode)SwitchController.RightArrow))
        {
            // 右矢印キーが押されたら横方向に移動
            TryMoveInDirection(Vector3.right * horizontalMoveDistance);
        }
    }

    // 移動範囲を確認して、範囲内なら移動する
    private void TryMoveInDirection(Vector3 direction)
    {
        Vector3 targetPosition = transform.position + direction;

        // 移動後の座標が範囲外なら移動しない
        if (IsOutOfBounds(targetPosition))
        {
            Debug.Log("移動先が範囲外です: " + targetPosition);
            return;
        }

        // 移動処理を開始
        isMoving = true; // 移動中フラグを立てる
        transform.DOMove(targetPosition, moveDuration).OnComplete(() => isMoving = false); // 移動完了時にフラグを解除
    }

    // 移動先が範囲外かどうかをチェック
    private bool IsOutOfBounds(Vector3 targetPosition)
    {
        if (boundsOrigin == null)
        {
            Debug.LogError("Boundsの原点が設定されていません");
            return true;
        }

        // Boundsの中心を原点とし、範囲をチェックする
        Vector3 boundsCenter = boundsOrigin.position;
        Vector3 halfSize = boundsSize / 2f;

        // 各軸で範囲外かどうかを判定
        bool isOutOfX = targetPosition.x < boundsCenter.x - halfSize.x || targetPosition.x > boundsCenter.x + halfSize.x;
        bool isOutOfY = targetPosition.y < boundsCenter.y - halfSize.y || targetPosition.y > boundsCenter.y + halfSize.y;
        bool isOutOfZ = targetPosition.z < boundsCenter.z - halfSize.z || targetPosition.z > boundsCenter.z + halfSize.z;

        return isOutOfX || isOutOfY || isOutOfZ;
    }
    */
}
