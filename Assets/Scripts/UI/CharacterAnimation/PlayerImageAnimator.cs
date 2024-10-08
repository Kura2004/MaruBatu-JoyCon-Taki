using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;
using UnityEngine.UI;

public class PlayerImageAnimator : MonoBehaviour
{
    [SerializeField] private float moveDuration = 1f; // �⊮�ɂ����鎞��
    [SerializeField] private float colorDuration = 1f; // �F�̕ύX�ɂ����鎞��

    [Header("X���ݒ�")]
    [SerializeField] private float xMovementDistance = 5f; // X���W�̈ړ�����
    [SerializeField] private Ease xEaseType = Ease.Linear; // X���W�̃C�[�W���O�̎��

    [Header("Y���ݒ�")]
    [SerializeField] private float yMovementDistance = 5f; // Y���W�̈ړ�����
    [SerializeField] private Ease yEaseType = Ease.Linear; // Y���W�̃C�[�W���O�̎��
    [SerializeField] private int loopMax = 0; // �ő僋�[�v��

    private int loopCounter = 0; // ���݂̃��[�v�J�E���g
    private Tween xTween; // X���W��Tween
    private Tween yTween; // Y���W��Tween

    [Header("�X�v���C�g�ݒ�")]
    [SerializeField] private List<Image> images; // ������Image��ێ����郊�X�g

    private void Start()
    {
        // �����F�����ɐݒ�
        foreach (var image in images)
        {
            if (image != null)
            {
                image.color = Color.black;
            }
        }
        loopCounter = 0;
    }

    // �A�j���[�V�������J�n
    public void StartMovement()
    {
        xTween = MoveXPositive(); // X���W�̐������Ɉړ����ATween��ۑ�
        yTween = MoveYPositive(); // Y���W�̐������Ɉړ����ATween��ۑ�
    }

    // �A�j���[�V�������~
    public void StopMovement()
    {
        StopXMovement(); // X����Tween���~
        StopYMovement(); // Y����Tween���~
    }

    // X���W��Tween���~
    private void StopXMovement()
    {
        if (xTween != null && xTween.IsActive())
        {
            xTween.Kill(); // X����Tween���~
            xTween = null; // Tween��null�ɐݒ�
        }
    }

    // Y���W��Tween���~
    private void StopYMovement()
    {
        if (yTween != null && yTween.IsActive())
        {
            yTween.Kill(); // Y����Tween���~
            yTween = null; // Tween��null�ɐݒ�
        }
    }

    // X���W�̐������Ɉړ�
    private Tween MoveXPositive()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetX = rectTransform.localPosition.x + xMovementDistance; // ���݂�X���W�Ɉړ������𑫂�

        return rectTransform.DOLocalMoveX(targetX, moveDuration)
            .SetEase(xEaseType)
            .OnComplete(() =>
            {

            });
    }

    // X���W�̕������Ɉړ�
    private Tween MoveXNegative()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetX = rectTransform.localPosition.x - xMovementDistance; // ���݂�X���W����ړ�����������

        return rectTransform.DOLocalMoveX(targetX, moveDuration)
            .SetEase(xEaseType)
            .OnComplete(() =>
            {

            });
    }

    // Y���W�̐������Ɉړ�
    private Tween MoveYPositive()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetY = rectTransform.localPosition.y + yMovementDistance; // ���݂�Y���W�Ɉړ������𑫂�

        return rectTransform.DOLocalMoveY(targetY, moveDuration / (2.0f * (float)loopMax))
            .SetEase(yEaseType)
            .OnComplete(() =>
            {
                if (loopCounter < loopMax) // �ő僋�[�v���ɒB���Ă��Ȃ��ꍇ
                {
                    MoveYNegative(); // Y���W�̕������Ɉړ�
                }
                else
                {
                    StopYMovement(); // ���[�v���ɒB�������~
                }
            });
    }

    // Y���W�̕������Ɉړ�
    private Tween MoveYNegative()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        float targetY = rectTransform.localPosition.y - yMovementDistance; // ���݂�Y���W����ړ�����������

        return rectTransform.DOLocalMoveY(targetY, moveDuration / (2.0f * (float)loopMax))
            .SetEase(yEaseType)
            .OnComplete(() =>
            {
                loopCounter++; // ���[�v�J�E���g�𑝉�
                if (loopCounter < loopMax) // �ő僋�[�v���ɒB���Ă��Ȃ��ꍇ
                {
                    MoveYPositive(); // Y���W�̐������Ɉړ�
                }
                else
                {
                    StopYMovement(); // ���[�v���ɒB�������~
                }
            });
    }

    // �X�v���C�g�̐F�𔒂ɕω�������
    public void ChangeSpritesColorToWhite()
    {
        foreach (var image in images)
        {
            if (image != null)
            {
                image.DOColor(Color.white, colorDuration)
                    .SetEase(Ease.InExpo); // �w�肵�����ԂŔ��ɕ⊮
            }
        }
    }

    // �X�v���C�g�̐F�������Ŏ󂯎�����F�ɕ⊮�I�ɕύX����
    public void ChangeSpritesColor(Color targetColor, float duration)
    {
        foreach (var image in images)
        {
            if (image != null)
            {
                image.DOColor(targetColor, duration)
                    .SetEase(Ease.InExpo); // �w�肵�����ԂŐF��⊮
            }
        }
    }

}
