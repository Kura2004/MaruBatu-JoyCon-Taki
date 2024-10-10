using UnityEngine;
using DG.Tweening;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;  // 1��̗h��̌p������
    [SerializeField] private float shakeIntensity = 0.5f;  // �h��̋���
    [SerializeField] private int vibrato = 10;  // �h��̉�
    [SerializeField] private float interval = 5f;  // �h��̊Ԋu�i�b�j
    [SerializeField] private Camera mainCamera;  // ���C���J����

    private Coroutine shakeCoroutine;  // �R���[�`���̎Q��

    public void StartAnimation()
    {
        // ���Ԋu�ŃJ������h�炷�R���[�`�����J�n
        shakeCoroutine = StartCoroutine(ShakeCameraRoutine());
    }

    private IEnumerator ShakeCameraRoutine()
    {
        while (true)
        {
            // DOTween���g���ăJ�����̗h��G�t�F�N�g��K�p
            mainCamera.transform.DOShakePosition(shakeDuration, shakeIntensity, vibrato).SetEase(Ease.OutQuad);

            // �G�t�F�N�g�̊J�n�A�j���[�V�������g���K�[
            InvertColorsEffect.Instance.StartAnimation(0.01f, 0.5f);
            // ���̗h��܂Ŏw�肵���Ԋu��҂�
            yield return new WaitForSeconds(interval);
        }
    }

    /// <summary>
    /// �A�j���[�V�������~���郁�\�b�h
    /// </summary>
    public void StopAnimation()
    {
        if (shakeCoroutine != null)
        {
            StopCoroutine(shakeCoroutine);  // �R���[�`�����~
            shakeCoroutine = null;  // �Q�Ƃ����Z�b�g
        }
    }
}