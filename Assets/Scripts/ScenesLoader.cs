using DG.Tweening;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

/// <summary>
/// MonoBehaviour���p�������V���O���g���ȃN���X�iScenesLoader�p�j
/// </summary>
public class ScenesLoader : SingletonMonoBehaviour<ScenesLoader>
{
    // �V�[���̃��[�h���Ԃ�ݒ肷�邽�߂�enum
    public enum SceneLoadTime
    {
        Short = 1,    // �Z�����[�h���ԁi�b�j
        Medium = 2,   // �����x�̃��[�h���ԁi�b�j
        Long = 3      // �������[�h���ԁi�b�j
    }

    [System.Serializable]
    public class SceneSettings
    {
        public string sceneName;
        public float loadDuration;  // ���[�h���Ԃ��������ʂ܂Őݒ�
    }

    [Header("�V�[���̃��[�h���Ԑݒ�")]
    [SerializeField] private SceneSettings[] sceneSettings;

    private bool isLocked = false; // ���b�N��Ԃ��Ǘ�����ϐ�

    protected override void OnEnable()
    {
        // �V�[�������[�h���ꂽ�Ƃ��ɌĂ΂��C�x���g��o�^
        SceneManager.sceneLoaded += OnSceneLoaded;
        base.OnEnable();
    }

    private void OnDisable()
    {
        // �C�x���g�̓o�^����
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    // �V�[�������[�h���ꂽ�Ƃ��ɌĂ΂�郁�\�b�h
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log($"�V�[���u{scene.name}�v�����[�h����܂����B");

        // ���[�h��ɑ���𖳌������鏈�����J�n
        float loadDuration = GetSceneLoadDuration(scene.name);
        StartCoroutine(DisableInputForDuration(loadDuration));

        // �V�[�����ƂɈقȂ鏉�����������K�v�ȏꍇ�͂����ɒǉ�
        if (scene.name == "StartMenu")
        {
            // StartMenu�̏���������
        }
        else if (scene.name == "4�~4")
        {
            // 4�~4�X�e�[�W�̏���������
        }
        else if (scene.name == "GameOver")
        {
            // GameOver�̏���������
        }
    }

    // �w�肳�ꂽ�V�[���̃��[�h���Ԃ��擾
    private float GetSceneLoadDuration(string sceneName)
    {
        foreach (var setting in sceneSettings)
        {
            if (setting.sceneName == sceneName)
            {
                return setting.loadDuration;
            }
        }
        Debug.LogWarning($"�w�肳�ꂽ�V�[�����u{sceneName}�v�̐ݒ肪������܂���B�f�t�H���g�̃��[�h���Ԃ��g�p���܂��B");
        return 1f; // �f�t�H���g�̃��[�h����
    }

    /// <summary>
    /// �w�肳�ꂽ���Ԃ̊ԁA����𖳌��ɂ��邽�߂̃R���[�`��.
    /// </summary>
    /// <param name="duration">���������鎞�ԁi�b�j</param>
    /// <returns></returns>
    private IEnumerator DisableInputForDuration(float duration)
    {
        // ����𖳌���
        isLocked = true;

        // �w�肳�ꂽ���Ԃ����ҋ@
        yield return new WaitForSeconds(duration + 0.1f);

        // �����L����
        isLocked = false;
    }

    public void LoadStage44()
    {
        if (isLocked)
        {
            Debug.LogWarning("�V�[���̃��[�h���������b�N����Ă��܂��B");
            ScenesAudio.BlockedSe();
            return;
        }
        isLocked = true;
        float loadDuration = GetSceneLoadDuration("4�~4");
        FadeManager.Instance.LoadScene("4�~4", loadDuration);
        Debug.Log("Stage44��ǂݍ��݂܂�");
    }

    public void LoadStartMenu()
    {
        if (isLocked)
        {
            Debug.LogWarning("�V�[���̃��[�h���������b�N����Ă��܂��B");
            ScenesAudio.BlockedSe();
            return;
        }
        isLocked = true;
        float loadDuration = GetSceneLoadDuration("StartMenu");
        FadeManager.Instance.LoadScene("StartMenu", loadDuration);

        Debug.Log("StartMenu��ǂݍ��݂܂�");
    }

    public void LoadGameOver(float duration)
    {
        if (isLocked)
        {
            Debug.LogWarning("�V�[���̃��[�h���������b�N����Ă��܂��B");
            ScenesAudio.BlockedSe();
            return;
        }
        isLocked = true;
        float loadDuration = GetSceneLoadDuration("GameOver");
        FadeManager.Instance.LoadScene("GameOver", duration);
        Debug.Log("GameOver��ǂݍ��݂܂�");
    }
}
