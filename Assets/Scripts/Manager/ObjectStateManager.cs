using UnityEngine;

public class ObjectStateManager : SingletonMonoBehaviour<ObjectStateManager>
{
    [SerializeField]
    private string firstObjectTag = "FirstObjectTag"; // 1つ目のオブジェクトのタグ

    [SerializeField]
    private string secondObjectTag = "SecondObjectTag"; // 2つ目のオブジェクトのタグ

    [SerializeField]
    private float delayBeforeFinding = 1f; // タグでオブジェクトを探す前の遅延時間（秒）

    private GameObject firstObject; // 1つ目のオブジェクト
    private GameObject secondObject; // 2つ目のオブジェクト

    private void Start()
    {
        // 遅延してタグのゲームオブジェクトを探す処理を開始
        Invoke(nameof(FindTaggedObjects), delayBeforeFinding);
    }

    private void FindTaggedObjects()
    {
        // タグでゲームオブジェクトを検索
        firstObject = GameObject.FindGameObjectWithTag(firstObjectTag);
        secondObject = GameObject.FindGameObjectWithTag(secondObjectTag);

        // オブジェクトが見つからなかった場合のログ
        if (firstObject == null)
        {
            Debug.LogWarning($"タグ '{firstObjectTag}' のオブジェクトが見つかりませんでした。");
        }

        if (secondObject == null)
        {
            Debug.LogWarning($"タグ '{secondObjectTag}' のオブジェクトが見つかりませんでした。");
        }
        secondObject.SetActive(false);
    }

    /// <summary>
    /// 1つ目のオブジェクトをアクティブにする
    /// </summary>
    public void SetFirstObjectActive(bool isActive)
    {
        if (firstObject != null)
        {
            firstObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("FirstObjectが設定されていません。");
        }
    }

    /// <summary>
    /// 2つ目のオブジェクトをアクティブにする
    /// </summary>
    public void SetSecondObjectActive(bool isActive)
    {
        if (secondObject != null)
        {
            secondObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("SecondObjectが設定されていません。");
        }
    }

    /// <summary>
    /// 1つ目と2つ目のオブジェクトのアクティブ状態をトグルする
    /// </summary>
    public void ToggleObjects(bool isFirstActive, bool isSecondActive)
    {
        SetFirstObjectActive(isFirstActive);
        SetSecondObjectActive(isSecondActive);
    }
}
