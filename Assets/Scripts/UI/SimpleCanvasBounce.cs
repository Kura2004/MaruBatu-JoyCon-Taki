using UnityEngine;
using DG.Tweening;

public class SimpleCanvasBounce : CanvasBounce
{

    protected override void Update()
    {
        // �L�����o�X�������������
        if (ShouldDropCanvas())
        {
            DropCanvas();
            Debug.Log("�L�����o�X���������܂�");
        }

        // �o�E���h���������Ă���ꍇ�A���� Q �L�[�������ꂽ�Ƃ��ɃL�����o�X���㏸������
        if (Input.GetKeyDown(KeyCode.Q) && !isFalling && isBouncingComplete)
        {
            RiseCanvas();

            if (dropOnStart)
            {
                // �K�v�ȏ���������΂����ɒǉ��ł��܂�
                dropOnStart = false;
            }

            Debug.Log("�L�����o�X���㏸���܂�");
        }
    }

    protected override void DropCanvas()
    {
        InitializeDrop();
        // �L�����o�X�I�u�W�F�N�g���A�N�e�B�u�ɂ���
        canvasObject.SetActive(true);

        // �L�����o�X�������̍����ɐݒ�
        canvasRectTransform.anchoredPosition = new Vector2(canvasRectTransform.anchoredPosition.x, initialDropHeight);
        // �����A�j���[�V����
        canvasRectTransform.DOAnchorPosY(groundY, initialBounceDuration).SetEase(Ease.InQuad).OnComplete(Bounce);
    }

    protected override void Bounce()
    {
        float currentBounceHeight = bounceHeight;
        float currentBounceDuration = initialBounceDuration;
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < bounceCount; i++)
        {
            // �o�E���h�A�j���[�V�������I�������ɓ���̃��\�b�h���Ă�
            sequence.AppendCallback(() => ScenesAudio.FallSe());

            sequence.Append(canvasRectTransform.DOAnchorPosY(groundY + currentBounceHeight, currentBounceDuration).SetEase(Ease.OutQuad));
            sequence.Append(canvasRectTransform.DOAnchorPosY(groundY, currentBounceDuration).SetEase(Ease.InQuad));

            // �e�ލ����Ǝ��Ԃ�����������
            currentBounceHeight *= heightDampingFactor;
            currentBounceDuration *= durationDampingFactor;
        }

        sequence.OnComplete(() =>
        {
            isFalling = false; // �����t���O�����Z�b�g
            isBouncingComplete = true; // �o�E���h�A�j���[�V���������������t���O��ݒ�
            ScenesAudio.FallSe();
        });
        sequence.Play();
    }

    protected override void RiseCanvas()
    {
        if (!isFalling)
        {
            // �L�����o�X��n�ʂ̈ʒu�ɐݒ�
            canvasRectTransform.anchoredPosition = new Vector2(canvasRectTransform.anchoredPosition.x, groundY);

            // �㏸�A�j���[�V����
            canvasRectTransform.DOAnchorPosY(initialDropHeight, riseDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                // �A�j���[�V����������A�L�����o�X���A�N�e�B�u�ɐݒ�
                canvasObject.SetActive(false);
            });
        }
        isBlocked = false;
    }
}



