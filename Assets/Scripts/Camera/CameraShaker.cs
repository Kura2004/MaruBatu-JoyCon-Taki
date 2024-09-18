using UnityEngine;
using Cinemachine;
using DG.Tweening;
using System.Collections;

public class CameraShaker : MonoBehaviour
{
    [SerializeField] private float shakeDuration = 0.5f;  // 1��̗h��̌p������
    [SerializeField] private float shakeIntensity = 0.5f;  // �h��̋���
    [SerializeField] private int vibrato = 10;  // �h��̉�
    [SerializeField] private float interval = 5f;  // �h��̊Ԋu�i�b�j
    [SerializeField] private CinemachineVirtualCamera virtualCamera; // CinemachineVirtualCamera�ւ̎Q��

    private void OnEnable()
    {
        // ���Ԋu�ŃJ������h�炷�R���[�`�����J�n
        StartCoroutine(ShakeCameraRoutine());
    }

    private IEnumerator ShakeCameraRoutine()
    {
        while (true)
        {
            // ���̗h��܂Ŏw�肵���Ԋu��҂�
            yield return new WaitForSeconds(interval);

            if (!TimeControllerToggle.isTimeStopped)
            {
                // DOTween���g���ăJ�����̗h��G�t�F�N�g��K�p
                virtualCamera.transform.DOShakePosition(shakeDuration, shakeIntensity, vibrato).SetEase(Ease.OutQuad);

                // �G�t�F�N�g�̊J�n�A�j���[�V�������g���K�[
                InvertColorsEffect.Instance.StartAnimation(0.01f, 0.5f);
            }
        }
    }
}


