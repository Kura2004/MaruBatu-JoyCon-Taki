using Saito.SoundManager;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesAudio : MonoBehaviour
{
    private void OnEnable()
    {
        // �V�[�����ǂݍ��܂ꂽ�Ƃ��ɌĂяo�����C�x���g�ɓo�^
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        // �C�x���g�̓o�^����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �V�[�����ǂݍ��܂ꂽ�Ƃ��ɌĂяo����郁�\�b�h
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �V�[���̖��O�ɉ�����BGM���Đ�
        switch (scene.name)
        {
            case "StartMenu":
                PlayStartMenuBgm();
                break;
            case "GameOver":
                //PlayGameOverBgm();
                break;
            case "4�~4":
                PlayGameBgm();
                PauseBgm();
                break;
            default:
                Debug.LogWarning("Unknown scene name: " + scene.name);
                break;
        }
    }

    public static void PlayGameBgm()
    {
        SoundManager.Instance.PlayBgm(SoundManager.BgmSoundData.BGM.Play);
        Debug.Log("4�~4��BGM���Đ����܂�");
    }

    public static void PauseBgm()
    {
        SoundManager.Instance.PauseBgm();
    }

    public static void UnPauseBgm()
    {
        SoundManager.Instance.UnPauseBgm();
        Debug.Log("BGM���Đ����܂�");
    }

    private void PlayGameOverBgm()
    {
        SoundManager.Instance.PlayBgm(SoundManager.BgmSoundData.BGM.GameOver);
    }

    private void PlayStartMenuBgm()
    {
        SoundManager.Instance.PlayBgm(SoundManager.BgmSoundData.BGM.Title);
    }

    public static void ClickSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Clicked);
    }

    public static void WinSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Win);
    }

    public static void SetSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Set);
    }

    public static void ShootSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Shoot);
    }

    public static void RestartSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Restart);
    }

    public static void FallSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Fall);
    }


    public static void HeartSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Heart);
    }

    public static void BlockedSe()
    {
        SoundManager.Instance.PlaySe(SoundManager.SeSoundData.SE.Blocked);
    }

    // �����SE���~���[�g����
    public static void MuteSpecificSe(SoundManager.SeSoundData.SE se)
    {
        SoundManager.Instance.MuteSpecificSe(se);
    }

    // �����SE�̃~���[�g����������
    public static void UnMuteSpecificSe(SoundManager.SeSoundData.SE se)
    {
        SoundManager.Instance.UnMuteSpecificSe(se);
    }

    // �����SE�̃��[�v�Đ����I���ɂ���
    public static void SetSeLoop(SoundManager.SeSoundData.SE se)
    {
        SoundManager.Instance.SetSeLoop(se);
    }

    // �����SE�̃��[�v�Đ����I�t�ɂ���
    public static void UnsetSeLoop(SoundManager.SeSoundData.SE se)
    {
        SoundManager.Instance.UnsetSeLoop(se);
    }
}
