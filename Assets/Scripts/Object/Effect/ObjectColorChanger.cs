using UnityEngine;
using DG.Tweening;

public class ObjectColorChanger : MonoBehaviour
{
    [SerializeField]
    private Color originalColor = Color.white; // ���̐F���C���X�y�N�^�[�Őݒ�ł���悤��

    [SerializeField]
    public Color hoverAndClickColor = Color.red; // �I�u�W�F�N�g���G�ꂽ�Ƃ��ƃN���b�N���̐F

    [SerializeField]
    private float colorChangeDuration = 1f; // �F�̕⊮�ɂ����鎞��

    [SerializeField]
    private string targetTag = "Player"; // �G���Ώۂ̃^�O

    private Renderer objectRenderer; // �I�u�W�F�N�g��Renderer
    private Tween colorTween; // �F�̕⊮�p��Tween
    public bool isClicked = false; // �N���b�N��Ԃ�ێ�����t���O
    private bool isTouchingTarget = false; // �^�O�����I�u�W�F�N�g���G��Ă��邩�̃t���O

    protected virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor; // �I�u�W�F�N�g�̌��̐F��ݒ�
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && objectRenderer != null)
        {
            isTouchingTarget = true;
            // �^�O�����I�u�W�F�N�g���G�ꂽ�Ƃ��ɐF��⊮�I�ɕς���
            colorTween = objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration);
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && objectRenderer != null)
        {
            isTouchingTarget = false;
            // �^�O�����I�u�W�F�N�g�����ꂽ�Ƃ��ɐF�����ɖ߂�
            colorTween = objectRenderer.material.DOColor(originalColor, colorChangeDuration);
        }
    }

    private void Update()
    {
        if (isClicked) return;
        // �^�O�����I�u�W�F�N�g���G��Ă��邩��SwitchController��A�{�^���������ꂽ�Ƃ��ɏ������s��
        if (isTouchingTarget)
        {
            if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup)
                && Input.GetKeyDown((KeyCode)SwitchController.L))
            {
                HandleClick();
            }

            if (GameTurnManager.Instance.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece)
    && Input.GetKeyDown((KeyCode)SwitchController.R))
            {
                HandleClick();
            }
        }
    }

    public void HandleClick()
    {
        if (objectRenderer != null)
        {
            isClicked = true; // �N���b�N��Ԃ��L�^
            objectRenderer.material.color = hoverAndClickColor;
            Debug.Log("�}�X���N���b�N����܂���");
        }
    }

    public void ChangeHoverColor(Color newColor)
    {
        hoverAndClickColor = newColor;
    }

    private bool ShouldChangeColorOnTrigger()
    {
        return !GameStateManager.Instance.IsRotating && !isClicked
            && !objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration).IsPlaying();
    }
}
