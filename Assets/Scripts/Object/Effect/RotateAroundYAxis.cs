using UnityEngine;

public class RotateAroundYAxis : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 30f; // ��]���x�i�x/�b�j
    private Vector3 lastPosition; // �O�t���[���̈ʒu���L�^����ϐ�

    private void Update()
    {
        if (!TimeControllerToggle.isTimeStopped)
        {
            // Y���𒆐S�ɉ�]
            transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

            // ���݂̈ʒu�����t���[���̂��߂ɋL�^
            lastPosition = transform.position;
        }
        else
        {
            // ���Ԓ�~���͈ʒu��O�t���[���̈ʒu�Ƀ��Z�b�g
            transform.position = lastPosition;
        }
    }
}



