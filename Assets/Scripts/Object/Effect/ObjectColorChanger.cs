using UnityEngine;
using DG.Tweening;

public class ObjectColorChanger : MonoBehaviour
{
    [SerializeField]
    private Color originalColor = Color.white; // 元の色をインスペクターで設定できるように

    [SerializeField]
    public Color hoverAndClickColor = Color.red; // オブジェクトが触れたときとクリック時の色

    [SerializeField]
    private float colorChangeDuration = 1f; // 色の補完にかかる時間

    [SerializeField]
    private string targetTag = "Player"; // 触れる対象のタグ

    private Renderer objectRenderer; // オブジェクトのRenderer
    private Tween colorTween; // 色の補完用のTween
    public bool isClicked = false; // クリック状態を保持するフラグ
    private bool isTouchingTarget = false; // タグを持つオブジェクトが触れているかのフラグ

    protected virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor; // オブジェクトの元の色を設定
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && objectRenderer != null)
        {
            isTouchingTarget = true;
            // タグを持つオブジェクトが触れたときに色を補完的に変える
            colorTween = objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && objectRenderer != null)
        {
            isTouchingTarget = false;
            // タグを持つオブジェクトが離れたときに色を元に戻す
            colorTween = objectRenderer.material.DOColor(originalColor, colorChangeDuration);
        }
    }

    private void Update()
    {
        if (isClicked) return;
        // タグを持つオブジェクトが触れているかつSwitchControllerのAボタンが押されたときに処理を行う
        if (isTouchingTarget)
        {
            if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup)
                && Input.GetKeyDown((KeyCode)SwitchController.L))
            {
                HandleClick();
            }

            if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece)
    && Input.GetKeyDown((KeyCode)SwitchController.R))
            {
                HandleClick();
            }
        }
    }

    public void HandleClick()
    {
        if (objectRenderer != null)
        {
            isClicked = true; // クリック状態を記録
            objectRenderer.material.color = hoverAndClickColor;
            Debug.Log("マスがクリックされました");
        }
    }

    public void ChangeHoverColor(Color newColor)
    {
        hoverAndClickColor = newColor;
    }

    private bool ShouldChangeColorOnTrigger()
    {
        return !GameStateManager.Instance.IsRotating && !isClicked
            && !objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration).IsPlaying();
    }
}
