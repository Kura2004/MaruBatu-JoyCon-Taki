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

    [SerializeField, Tooltip("�ǂ̎��ŉ�]���邩")]
    private RotationAxis rotationAxis = RotationAxis.Y;  // ��]���鎲�A�f�t�H���g��Y

    private Vector3 rotationVector;

    void OnEnable()
    {
        rotationVector = GetRotationVector();
    }

    void Update()
    {
        //if (!TimeControllerToggle.isTimeStopped)

        transform.localEulerAngles += rotationVector * normalRotationSpeed * Time.deltaTime;
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
}
