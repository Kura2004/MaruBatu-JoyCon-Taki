using UnityEngine;
using DG.Tweening;

public class ObjectScaler : MonoBehaviour
{
    [SerializeField]
    private Transform targetObject; // 操作対象のオブジェクト

    [SerializeField]
    private float scaleRate = 1.3f; // 拡大率

    [SerializeField]
    private float scaleDuration = 0.3f; // 拡大・縮小にかかる時間

    [SerializeField]
    private string targetTag = "Player"; // 触れる対象のタグ

    private Vector3 originalScale; // 元のスケール
    private Vector3 enlargedScale; // 拡大後のスケール

    // タグを持つオブジェクトが触れているかのフラグ
    private bool isTouchingTarget = false;

    //// マウス入力を無視するかどうかを管理する
    //public bool ignoreMouseInput = false;

    private void OnEnable()
    {
        if (targetObject == null)
        {
            targetObject = transform;
        }

        // 元のスケールを保存
        originalScale = targetObject.localScale;
        enlargedScale = originalScale * scaleRate; // 拡大後のスケール
    }

    // タグを持つオブジェクトが触れたときに呼ばれるメソッド
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && CanProcessInput())
        {
            isTouchingTarget = true;
            EnlargeObject();
        }
    }

    // タグを持つオブジェクトが離れたときに呼ばれるメソッド
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && CanProcessInput())
        {
            isTouchingTarget = false;
            ResetObjectSize();
        }
    }

    // タグを持つオブジェクトが触れている場合にのみ処理を実行するメソッド
    private void Update()
    {
        if (TimeControllerToggle.isTimeStopped || GameStateManager.Instance.IsRotating)
        {
            return;
        }

        if (isTouchingTarget)
        {
            EnlargeObject();
        }
        else
        {
            ResetObjectSize();
        }
    }

    // オブジェクトのサイズを大きくするメソッド
    private void EnlargeObject()
    {
        if (!targetObject.DOScale(enlargedScale, scaleDuration).IsPlaying())
        {
            targetObject.DOScale(enlargedScale, scaleDuration).SetEase(Ease.OutBack);
        }
    }

    // オブジェクトのサイズを徐々に元に戻すメソッド
    private void ResetObjectSize()
    {
        if (!targetObject.DOScale(originalScale, scaleDuration).IsPlaying())
        {
            targetObject.DOScale(originalScale, scaleDuration).SetEase(Ease.InOutQuad);
        }
    }

    // 入力を処理できるかどうかを判定するメソッド
    private bool CanProcessInput()
    {
        return !TimeControllerToggle.isTimeStopped && !GameStateManager.Instance.IsRotating;
    }
}
