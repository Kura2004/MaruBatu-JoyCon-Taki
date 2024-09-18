using UnityEngine;
using DG.Tweening;

public class ObjectColorChanger : MonoBehaviour
{
    [SerializeField]
    private Color originalColor = Color.white; // ���̐F���C���X�y�N�^�[�Őݒ�ł���悤��

    [SerializeField]
    private Color hoverAndClickColor = Color.red; // �}�E�X�����������Ƃ��ƃN���b�N���̐F

    [SerializeField]
    private float colorChangeDuration = 1f; // �F�̕⊮�ɂ����鎞��

    private Renderer objectRenderer; // �I�u�W�F�N�g��Renderer
    private Tween colorTween; // �F�̕⊮�p��Tween
    public bool isClicked = false; // �N���b�N��Ԃ�ێ�����t���O

    protected virtual void Start()
    {
        objectRenderer = GetComponent<Renderer>();
        if (objectRenderer != null)
        {
            objectRenderer.material.color = originalColor; // �I�u�W�F�N�g�̌��̐F��ݒ�
        }
    }

    protected virtual void OnMouseEnter()
    {
        if (objectRenderer != null)
        {
            // �}�E�X���I�u�W�F�N�g�ɓ������Ƃ��ɐF��⊮�I�ɕς���
            colorTween = objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration);
        }
    }

    protected virtual void OnMouseExit()
    {
        if (objectRenderer != null)
        {
            // �}�E�X���I�u�W�F�N�g����o���Ƃ��ɐF�����ɖ߂�
            colorTween = objectRenderer.material.DOColor(originalColor, colorChangeDuration);
        }
    }

    protected virtual void OnMouseOver()
    {
        if (ShouldChangeColorOnMouseOver())
        {
            // �}�E�X���I�u�W�F�N�g�ɓ������Ƃ��ɐF��⊮�I�ɕς���
            colorTween = objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration);
        }
    }

    public virtual void OnMouseDown()
    {
        if (objectRenderer != null)
        {
            isClicked = true; // �N���b�N��Ԃ��L�^
            objectRenderer.material.color = hoverAndClickColor;
        }
    }

    public void ChangeHoverColor(Color newColor)
    {
        hoverAndClickColor = newColor;
    }

    private bool ShouldChangeColorOnMouseOver()
    {
        return !GameStateManager.Instance.IsRotating && !isClicked
            && !objectRenderer.material.DOColor(hoverAndClickColor, colorChangeDuration).IsPlaying();
    }
}
