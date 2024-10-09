using UnityEngine;
using DG.Tweening;
using System.Collections;

public class ArmAnimator : MonoBehaviour
{
    private RectTransform rectTransform;

    // �C���X�y�N�^�[�Őݒ�\�ȃp�����[�^
    [Header("Rotation Settings")]
    [SerializeField] private Vector3 rotationAxis = Vector3.up;  // ��]��
    [SerializeField] private float rotationAngle = 90f;          // ��]�p�x
    [SerializeField] private float duration = 1f;                // �⊮����
    [SerializeField] private Ease easing = Ease.InOutSine;       // �C�[�W���O
    [SerializeField] private float delayBetweenLoops = 0.5f;     // �������؂�ւ�鎞�̑ҋ@����

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        StartCoroutine(RotateArm());
    }

    private IEnumerator RotateArm()
    {
        while (true)
        {
            Vector3 targetRotation = rectTransform.localEulerAngles + rotationAxis * rotationAngle;
            rectTransform.DOLocalRotate(targetRotation, duration, RotateMode.FastBeyond360)
                         .SetEase(easing);

            // ��]���I���܂őҋ@
            yield return new WaitForSeconds(duration + delayBetweenLoops);
            
            // ���΂̕����ɉ�]
            targetRotation = rectTransform.localEulerAngles - rotationAxis * rotationAngle;
            rectTransform.DOLocalRotate(targetRotation, duration, RotateMode.FastBeyond360)
                         .SetEase(easing);
            
            // �ēx�ҋ@
            yield return new WaitForSeconds(duration + delayBetweenLoops);
        }
    }
}
