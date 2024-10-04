using UnityEngine;
using UnityEngine.UI;
using Saito.SoundManager;
using TMPro;
using DG.Tweening;

public class VolumeSettings : MonoBehaviour
{
    [Header("���ʒ����X���C�_�[")]
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    [Header("���ʃe�L�X�g")]
    [SerializeField] private TextMeshProUGUI bgmVolumeText;
    [SerializeField] private TextMeshProUGUI seVolumeText;

    [Header("�e�L�X�g�g��ݒ�")]
    [SerializeField] private float scaleFactor = 1.2f;
    [SerializeField] private float scaleDuration = 0.3f;

    private Vector3 initialBgmVolumeTextScale;
    private Vector3 initialSeVolumeTextScale;

    public bool onSeVolume = true;

    private void Start()
    {
        // SoundManager���特�ʂ̏����l���擾���A�X���C�_�[�ɐݒ�
        bgmVolumeSlider.value = SoundManager.Instance.bgmMasterVolume;
        seVolumeSlider.value = SoundManager.Instance.seMasterVolume;

        // �X���C�_�[�̒l�ύX�C�x���g�Ƀ��X�i�[��ǉ�
        bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        seVolumeSlider.onValueChanged.AddListener(OnSeVolumeChanged);

        initialBgmVolumeTextScale = bgmVolumeText.transform.localScale;
        initialSeVolumeTextScale = seVolumeText.transform.localScale;

        EnableBgmVolumeControl();
    }

    /// <summary>
    /// BGM���ʃX���C�_�[�̒l���ύX���ꂽ���̏���
    /// </summary>
    /// <param name="value">�V�������ʒl</param>
    public void OnBgmVolumeChanged(float value)
    {
        //value = Mathf.Clamp(value, 0, 1);
        SoundManager.Instance.SetBgmMasterVolume(value);
    }

    /// <summary>
    /// SE���ʃX���C�_�[�̒l���ύX���ꂽ���̏���
    /// </summary>
    /// <param name="value">�V�������ʒl</param>
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
        // �����X�P�[�����g�p���Ė߂�
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

