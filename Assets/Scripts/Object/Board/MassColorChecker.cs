using UnityEngine;
using System.Collections;
using DG.Tweening; // DOTween�̃l�[���X�y�[�X���C���|�[�g

public class MassColorChecker : MonoBehaviour
{
    [SerializeField]
    private string massTag = "Mass"; // �^�O��

    [SerializeField]
    private Material targetMaterial; // �F��ύX����}�e���A��

    [SerializeField]
    private Color targetColor = Color.red; // �ύX��̐F

    private GameObject[] mass = new GameObject[4]; // �I�u�W�F�N�g��ۑ�����z��
    private int massIndex = 0; // �z��̃C���f�b�N�X

    private void OnTriggerStay(Collider other)
    {
        // Tag��Mass�̃I�u�W�F�N�g�ɓ������Ă���ꍇ
        if (other.CompareTag(massTag))
        {
            // �I�u�W�F�N�g��o�^
            mass[massIndex] = other.gameObject;
            massIndex = (massIndex + 1) % mass.Length;
        }
    }

    private void Update()
    {
        if (MainGameOverManager.loadGameOver) return;
        var gameState = GameStateManager.Instance;
        if (!gameState.IsRotating && gameState.IsBoardSetupComplete)
        {
            if (HasFourOrMorePlayerObjects())
            {
                
                gameState.SetPlayerWin(true);
                StartCoroutine(HandleGameOverCoroutine());
                return;
            }

            if (HasFourOrMoreOpponentObjects())
            {
                gameState.SetOpponentWin(true);
                StartCoroutine(HandleGameOverCoroutine());
                return;
            }
        }
    }

    private bool HasFourOrMorePlayerObjects()
    {
        return HasFourOrMoreObjectsOfColor(GlobalColorManager.Instance.playerColor);
    }

    private bool HasFourOrMoreOpponentObjects()
    {
        return HasFourOrMoreObjectsOfColor(GlobalColorManager.Instance.opponentColor);
    }

    private bool HasFourOrMoreObjectsOfColor(Color colorToCheck)
    {
        int count = 0;

        // �z����̃I�u�W�F�N�g���`�F�b�N
        foreach (var obj in mass)
        {
            if (obj != null)
            {
                Renderer renderer = obj.GetComponent<Renderer>();

                if (renderer != null && renderer.material.color == colorToCheck
                    && obj.GetComponent<MouseInteractionWithTurnManager>().isClicked)
                {
                    count++;
                }
            }
        }

        return count >= 4; // 4�ȏ�̃I�u�W�F�N�g���w�肵���F�ł����true��Ԃ�
    }
    public IEnumerator HandleGameOverCoroutine()
    {
        //ToggleMassState();


        // mass�̃}�e���A���J���[���w�肵���F�ɑ����ύX����
        foreach (var obj in mass)
        {
            if (obj != null)
            {
                Renderer renderer = obj.GetComponent<Renderer>();
                if (renderer != null && targetMaterial != null)
                {
                    // �J�X�^���V�F�[�_�[�ŐF�ύX
                    targetMaterial.SetColor("_Color", targetColor); // �V�F�[�_�[�̃v���p�e�B���ɍ��킹�ĕύX
                    renderer.material = targetMaterial;
                }
            }
        }
        yield return null; // Coroutine���I�����邽�߂ɑҋ@�i�K�v�ɉ����đ��̏�����ǉ��j
    }
}
