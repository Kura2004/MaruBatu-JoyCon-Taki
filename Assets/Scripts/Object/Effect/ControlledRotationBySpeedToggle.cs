using UnityEngine;

public class ControlledRotationBySpeedToggle : MonoBehaviour
{
    public enum RotationAxis
    {
        X,
        Y,
        Z
    }

    [SerializeField, Tooltip("�ʏ�̉�]���x (�x/�b)")]
    private float normalRotationSpeed = 10f;  // �ʏ펞�̉�]���x

    [SerializeField, Tooltip("������]����Ƃ��̑��x (�x/�b)")]
    private float fastRotationSpeed = 50f;    // �t���O���I���̂Ƃ��̉�]���x

    [SerializeField, Tooltip("��]���x�𑬂����邩�ǂ��������肷��t���O")]
    public bool isSpeedIncreased;    // ��]���x��ύX����t���O

    [SerializeField, Tooltip("�ǂ̎��ŉ�]���邩")]
    private RotationAxis rotationAxis = RotationAxis.Y;  // ��]���鎲�A�f�t�H���g��Y

    private float currentRotationSpeed;
    private bool isFastRotating = false; // ������]��Ԃ��Ǘ�����ϐ�

    void OnEnable()
    {
        currentRotationSpeed = normalRotationSpeed;  // �����̉�]���x��ݒ�
        isSpeedIncreased = false;
    }

    private void OnMouseEnter()
    {
        isSpeedIncreased = true;
    }

    private void OnMouseExit()
    {
        isSpeedIncreased = false;
    }

    void Update()
    {
        // �t���O�ɉ����ĉ�]���x��ݒ�
        currentRotationSpeed = isSpeedIncreased ? fastRotationSpeed : normalRotationSpeed;

        // �w�肳�ꂽ���ŉ�]
        Vector3 rotationVector = GetRotationVector();

        if (!TimeControllerToggle.isTimeStopped)
        {
            if (isFastRotating)
            {
                // Z���ō�����]
                rotationVector = Vector3.forward;
                currentRotationSpeed = fastRotationSpeed;
            }

            transform.Rotate(rotationVector * currentRotationSpeed * Time.deltaTime);
        }
    }

    private Vector3 GetRotationVector()
    {
        switch (rotationAxis)
        {
            case RotationAxis.X:
                return Vector3.right;
            case RotationAxis.Y:
                return Vector3.up;
            case RotationAxis.Z:
                return Vector3.forward;
            default:
                return Vector3.up;
        }
    }

    // ��]����ݒ肷�郁�\�b�h
    public void SetRotationAxis(RotationAxis newAxis)
    {
        rotationAxis = newAxis;
    }

    // ������Z����]���郁�\�b�h
    public void SetFastRotation(bool isFast)
    {
        isFastRotating = isFast;
    }
}
