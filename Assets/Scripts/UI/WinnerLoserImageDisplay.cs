using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerLoserImageDisplay : MonoBehaviour
{
    // 1P�̏��ҁA�s�҂ɑΉ�����Image�̃��X�g
    [SerializeField] private List<Image> player1WinnerImages; // 1P���҂�Image���X�g
    [SerializeField] private List<Image> player1LoserImages;  // 1P�s�҂�Image���X�g

    // 2P�̏��ҁA�s�҂ɑΉ�����Image�̃��X�g
    [SerializeField] private List<Image> player2WinnerImages; // 2P���҂�Image���X�g
    [SerializeField] private List<Image> player2LoserImages;  // 2P�s�҂�Image���X�g

    // ������������Image�̃��X�g
    [SerializeField] private List<Image> player1DrawImages; // 1P������������Image���X�g
    [SerializeField] private List<Image> player2DrawImages; // 2P������������Image���X�g

    private void Start()
    {

        DisableAllImages(); // �ŏ��ɑS�Ẳ摜���\���ɂ���

        // ���݂̏��҂ɉ����ĕ\������摜��؂�ւ���
        switch (GameWinnerManager.Instance.CurrentWinner)
        {
            case GameWinnerManager.Winner.Player1:
                SetPlayerImages(player1WinnerImages, true);  // 1P���҂̉摜��\��
                SetPlayerImages(player2LoserImages, true);   // 2P�s�҂̉摜��\��
                break;

            case GameWinnerManager.Winner.Player2:
                SetPlayerImages(player2WinnerImages, true);  // 2P���҂̉摜��\��
                SetPlayerImages(player1LoserImages, true);   // 1P�s�҂̉摜��\��
                break;

            case GameWinnerManager.Winner.Draw:
                SetDrawImages(); // �����������̉摜��\��
                break;

            case GameWinnerManager.Winner.None:
            default:
                // ���ׂẲ摜���\���ɂ����܂�
                break;
        }
    }

    /// <summary>
    /// �w�肵���v���C���[��Image���X�g��\������
    /// </summary>
    private void SetPlayerImages(List<Image> images, bool show)
    {
        foreach (var image in images)
        {
            image.gameObject.SetActive(show);
        }
    }

    /// <summary>
    /// ������������Image��\������
    /// </summary>
    private void SetDrawImages()
    {
        SetPlayerImages(player1DrawImages, true);
        SetPlayerImages(player2DrawImages, true);
    }

    /// <summary>
    /// ���ׂĂ�Image���\���ɂ���
    /// </summary>
    private void DisableAllImages()
    {
        SetPlayerImages(player1WinnerImages, false);
        SetPlayerImages(player1LoserImages, false);
        SetPlayerImages(player1DrawImages, false);

        SetPlayerImages(player2WinnerImages, false);
        SetPlayerImages(player2LoserImages, false);
        SetPlayerImages(player2DrawImages, false);
    }
}