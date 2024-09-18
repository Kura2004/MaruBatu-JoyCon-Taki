using UnityEngine;
using UnityEngine.UI;
using Saito.SoundManager;

public class VolumeSettings : MonoBehaviour
{
    [Header("���ʒ����X���C�_�[")]
    [SerializeField] private Slider bgmVolumeSlider;
    [SerializeField] private Slider seVolumeSlider;

    private void Start()
    {
        // SoundManager���特�ʂ̏����l���擾���A�X���C�_�[�ɐݒ�
        bgmVolumeSlider.value = SoundManager.Instance.bgmMasterVolume;
        seVolumeSlider.value = SoundManager.Instance.seMasterVolume;

        // �X���C�_�[�̒l�ύX�C�x���g�Ƀ��X�i�[��ǉ�
        bgmVolumeSlider.onValueChanged.AddListener(OnBgmVolumeChanged);
        seVolumeSlider.onValueChanged.AddListener(OnSeVolumeChanged);
    }

    /// <summary>
    /// BGM���ʃX���C�_�[�̒l���ύX���ꂽ���̏���
    /// </summary>
    /// <param name="value">�V�������ʒl</param>
    private void OnBgmVolumeChanged(float value)
    {
        SoundManager.Instance.SetBgmMasterVolume(value);
    }

    /// <summary>
    /// SE���ʃX���C�_�[�̒l���ύX���ꂽ���̏���
    /// </summary>
    /// <param name="value">�V�������ʒl</param>
    private void OnSeVolumeChanged(float value)
    {
        SoundManager.Instance.SetSeMasterVolume(value);
    }
}

