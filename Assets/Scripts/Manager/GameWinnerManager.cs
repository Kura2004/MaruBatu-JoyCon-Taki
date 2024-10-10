using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// ���҂��Ǘ�����N���X
/// </summary>
public class GameWinnerManager : SingletonMonoBehaviour<GameWinnerManager>
{
    public enum Winner
    {
        None,     // ���҂Ȃ�
        Player1,  // 1P
        Player2,  // 2P
        Draw      // ��������
    }

    private Winner _currentWinner = Winner.None;

    public Winner CurrentWinner
    {
        get { return _currentWinner; }
    }

    /// <summary>
    /// ���҂�ݒ肷�郁�\�b�h
    /// </summary>
    public void SetWinner(Winner winner)
    {
        _currentWinner = winner;
        Debug.Log("���҂��ݒ肳��܂���: " + winner.ToString());
    }

    /// <summary>
    /// �w�肵��Winner�����݂̏��҂ƈ�v���邩�m�F���郁�\�b�h
    /// </summary>
    public bool IsCurrentWinner(Winner winner)
    {
        return _currentWinner == winner;
    }

    /// <summary>
    /// ���ҏ������Z�b�g���郁�\�b�h
    /// </summary>
    public void ResetWinner()
    {
        _currentWinner = Winner.None;
        Debug.Log("���ҏ�񂪃��Z�b�g����܂���");
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded; // �V�[�����ǂݍ��܂ꂽ���̃C�x���g��o�^
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded; // �C�x���g�̉���
    }

    /// <summary>
    /// �V�[�����ǂݍ��܂ꂽ���ɌĂ΂�郁�\�b�h
    /// </summary>
    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // �V�[������ "4�~4" �������ꍇ�A���ҏ������Z�b�g����
        if (scene.name == "4�~4")
        {
            ResetWinner();
        }
    }
}
