using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 5f; // ��{�̈ړ����x
    [SerializeField] private float acceleration = 2f; // �����x�̌W��
    [SerializeField] private float deceleration = 2f; // �����x�̌W��
    [SerializeField] private float maxVelocity = 10f; // �ő呬�x�̏��

    private Vector3 startPosition; // �J�����̏������W���L�^����ϐ�
    private bool isLocked = false; // ���W�ړ��̃��b�N���
    private Vector3 currentVelocity = Vector3.zero; // ���݂̑��x

    private void Start()
    {
        // �������W���L�^
        startPosition = transform.position;
    }

    private void Update()
    {
        HandleInput();
        ApplyMovement();
    }

    private void HandleInput()
    {
        HandleLockToggle();
        HandleMovement();
        HandlePositionReset();
    }

    private void HandleLockToggle()
    {
        // �}�E�X�̃N���b�N�Ń��b�N�̐؂�ւ�
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            isLocked = !isLocked;
            ScenesAudio.ShootSe();
        }
    }

    private void HandleMovement()
    {
        if (isLocked)
            return;

        // Input.GetAxis���g�p���Ĉړ����͂��擾
        float moveHorizontal = Input.GetAxis("Horizontal"); // A/D�L�[�܂��͖��L�[
        float moveVertical = Input.GetAxis("Vertical");   // W/S�L�[�܂��͖��L�[

        Vector3 move = new Vector3(moveHorizontal, 0, moveVertical);

        // �L�[�������ꂽ�ԁA����
        if (move.magnitude > 0)
        {
            currentVelocity += move.normalized * moveSpeed * acceleration * Time.deltaTime;
            // ���x�̏����K�p
            currentVelocity = Vector3.ClampMagnitude(currentVelocity, maxVelocity);
        }
        else
        {
            // �L�[�������ꂽ�u�Ԃ��猸��
            currentVelocity = Vector3.Lerp(currentVelocity, Vector3.zero, deceleration * Time.deltaTime);
        }
    }

    private void ApplyMovement()
    {
        // �J�����̈ʒu���ړ�������
        transform.position += currentVelocity * Time.deltaTime;
    }

    private void HandlePositionReset()
    {
        // 1�L�[�������ꂽ���A�J�����̈ʒu���������W�Ƀ��Z�b�g
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            currentVelocity = Vector3.zero;
            transform.position = startPosition;
            isLocked = false;
            ScenesAudio.WinSe(); // �K�v�ɉ����ĉ����Đ����郁�\�b�h��ǉ�
        }
    }
}
