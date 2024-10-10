using UnityEngine;
using TMPro;  // TextMeshPro�p�̖��O��Ԃ��C���|�[�g

public class WinnerTextDisplay : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI winnerText; // TextMeshPro�̃e�L�X�g�t�B�[���h

    private void Start()
    {
        // GameWinnerManager�̌��݂̏��҂ɉ����ăe�L�X�g��ύX
        switch (GameWinnerManager.Instance.CurrentWinner)
        {
            case GameWinnerManager.Winner.Player1:
                winnerText.text = "1P Win!";
                break;

            case GameWinnerManager.Winner.Player2:
                winnerText.text = "2P Win!";
                break;

            case GameWinnerManager.Winner.Draw:
                winnerText.text = "Draw";
                break;

            case GameWinnerManager.Winner.None:
            default:
                winnerText.text = "No Winner Yet";
                break;
        }
    }
}
