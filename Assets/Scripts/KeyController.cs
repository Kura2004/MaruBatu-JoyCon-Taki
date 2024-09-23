using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class KeyController : MonoBehaviour
{
    private void Update()
    {
        // ESC�L�[�������ꂽ��A�v���P�[�V�������I��
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("�A�v�����I�����܂�");
            Quit();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Saito.SoundManager.SoundManager.Instance.MuteBgm();
            Saito.SoundManager.SoundManager.Instance.MuteSe();
            Debug.Log("BGM��SE���~���[�g���܂���");
        }

        // E�L�[�������ꂽ��BGM��SE�̃~���[�g������
        if (Input.GetKeyDown(KeyCode.E))
        {
            Saito.SoundManager.SoundManager.Instance.UnMuteBgm();
            Saito.SoundManager.SoundManager.Instance.UnMuteSe();
            Debug.Log("BGM��SE�̃~���[�g���������܂���");
        }

        // �f�o�b�O�p
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ScenesLoader.Instance.LoadGameOver(0);
            Debug.Log("�Q�[���I�[�o�[��ʂɍs���܂�");
        }
    }

    public void Quit()
    {
#if UNITY_EDITOR
        EditorApplication.isPlaying = false;
#else
                    UnityEngine.Application.Quit();
#endif
    }
}
