using UnityEngine;
using UnityEngine.UI;
using Saito.SoundManager;
using TMPro;
using DG.Tweening;

public class VolumeSettings : MonoBehaviour
{
    [Header("音量調整スライダー")]
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    [Header("音量テキスト")]
    [SerializeField] private TextMeshProUGUI bgmVolumeText;
    [SerializeField] private TextMeshProUGUI seVolumeText;

    [Header("テキスト拡大設定")]
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float scaleDuration = 0.3f;

    private Vector3 initialBgmVolumeTextScale;
    private Vector3 initialSeVolumeTextScale;

    public bool onSeVolume = true;

    private void Start()
    {
        // SoundManagerから音量の初期値を取得し、スライダーに設定
        bgmVolumeSlider.value = SoundManager.Instance.bgmMasterVolume;
        seVolumeSlider.value = SoundManager.Instance.seMasterVolume;

        // スライダーの値変更イベントにリスナーを追加
        bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        seVolumeSlider.onValueChanged.AddListener(OnSeVolumeChanged);

        initialBgmVolumeTextScale = bgmVolumeText.transform.localScale;
        initialSeVolumeTextScale = seVolumeText.transform.localScale;

        EnableBgmVolumeControl();
    }

    /// <summary>
    /// BGM音量スライダーの値が変更された時の処理
    /// </summary>
    /// <param name="value">新しい音量値</param>
    public void OnBgmVolumeChanged(float value)
    {
        //value = Mathf.Clamp(value, 0, 1);
        SoundManager.Instance.SetBgmMasterVolume(value);
    }

    /// <summary>
    /// SE音量スライダーの値が変更された時の処理
    /// </summary>
    /// <param name="value">新しい音量値</param>
    public void OnSeVolumeChanged(float value)
    {
        //value = Mathf.Clamp(value, 0, 1);
        SoundManager.Instance.SetSeMasterVolume(value);
    }

    public void AddBgmVolume(float amount)
    {
        float newVolume = SoundManager.Instance.bgmMasterVolume + amount;
        newVolume = Mathf.Clamp(newVolume, 0f, 1f);
        SoundManager.Instance.SetBgmMasterVolume(newVolume);
        bgmVolumeSlider.value = newVolume;
    }

    public void AddSeVolume(float amount)
    {
        float newVolume = SoundManager.Instance.seMasterVolume + amount;
        newVolume = Mathf.Clamp(newVolume, 0f, 1f);
        SoundManager.Instance.SetSeMasterVolume(newVolume);
        seVolumeSlider.value = newVolume;
    }

    public void EnableSeVolumeControl()
    {
        onSeVolume = true;
        ScaleText(seVolumeText, scaleFactor, scaleDuration);
        ResetTextScale(bgmVolumeText, scaleDuration);
    }

    public void EnableBgmVolumeControl()
    {
        onSeVolume = false;
        ScaleText(bgmVolumeText, scaleFactor, scaleDuration);
        ResetTextScale(seVolumeText, scaleDuration);
    }

    public void ScaleText(TextMeshProUGUI text, float scaleFactor, float duration)
    {
        text.transform.DOScale(scaleFactor, duration).OnComplete(() =>
        {

        });

    }

    public void ResetTextScale(TextMeshProUGUI text, float duration)
    {
        // 初期スケールを使用して戻す
        if (text == bgmVolumeText)
        {
            text.transform.DOScale(initialBgmVolumeTextScale, duration);
        }
        else if (text == seVolumeText)
        {
            text.transform.DOScale(initialSeVolumeTextScale, duration);
        }
    }
}

