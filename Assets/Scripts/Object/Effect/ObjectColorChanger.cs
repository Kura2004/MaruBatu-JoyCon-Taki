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
    public bool isClicked { get; private set; } = false;

    protected bool isChanging = false;
    protected virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor; // �I�u�W�F�N�g�̌��̐F��ݒ�
        }
    }

    protected virtual void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(targetTag) && objectRenderer != null)
        {
            // �^�O�����I�u�W�F�N�g���G�ꂽ�Ƃ��ɐF��⊮�I�ɕς���
            isChanging = true;
            colorTween = objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration)
                .OnComplete(() =>
                {
                    isChanging = false;
                });
        }
    }

    protected virtual void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag) && objectRenderer != null)
        {
            // �^�O�����I�u�W�F�N�g�����ꂽ�Ƃ��ɐF�����ɖ߂�
            colorTween = objectRenderer.material.DOColor(originalColor, colorChangeDuration);
        }
    }

    public void HandleClick()
    {
        isClicked = true; // �N���b�N��Ԃ��L�^
        if (objectRenderer != null)
        {
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
            && !isChanging;
    }
}
