using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ObjectShaker : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 2f;  // �k����b��
    [SerializeField] private float shakeIntensity = 1f;  // �k���̋���
    [SerializeField] private int vibrato = 10;  // �k���̉�
    [SerializeField] private bool repeatShake = false;  // �������Ők���邩�ǂ���
    [SerializeField] private float repeatInterval = 5f;  // �k���̎����i�b�j

    private Vector3 originalPosition;  // �I�u�W�F�N�g�̌��̈ʒu
    private Tween currentShakeTween;  // ���݂̐k���A�j���[�V�������Ǘ�����Tween

    private void Start()
    {
        originalPosition = transform.position;  // �I�u�W�F�N�g�̌��̈ʒu���L�^
        if (repeatShake)
        {
            // �������Ők����R���[�`�����J�n
            StartCoroutine(ShakeObjectRoutine());
        }
    }

    private void ShakeObject()
    {
        // �����̃A�j���[�V����������Β��f
        StopShake();

        // �V�����k���A�j���[�V�������J�n
        currentShakeTween = transform.DOShakePosition(shakeDuration, shakeIntensity, vibrato)
            .SetEase(Ease.OutQuad)
            .OnKill(() =>
            {
                // �A�j���[�V�������I�������猳�̈ʒu�ɖ߂�
                transform.position = originalPosition;
            });
    }

    public void StopShake()
    {
        if (currentShakeTween != null && currentShakeTween.IsPlaying())
        {
            // �A�j���[�V�������Đ����ł���ꍇ�A��~����
            currentShakeTween.Kill();
            // ���̈ʒu�ɖ߂�
            transform.position = originalPosition;
        }
    }

    private IEnumerator ShakeObjectRoutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(repeatInterval);

            if (!TimeControllerToggle.isTimeStopped)
            {
                // �Ăѐk���G�t�F�N�g��K�p
                ShakeObject();
            }
            else
            {
                // TimeControllerToggle.isTimeStopped��true�̏ꍇ�ɐk���𒆒f
                StopShake();
            }
        }
    }
}
