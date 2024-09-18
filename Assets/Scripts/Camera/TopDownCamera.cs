using UnityEngine;

public class TopDownCamera : MonoBehaviour
{
    [SerializeField] Transform target; // �^�[�Q�b�g�̃Q�[���I�u�W�F�N�g
    [SerializeField] float YOffset = 10f; // �J�����̍����iY���W�̃I�t�Z�b�g�j
    [SerializeField] float ZOffset = -10f; // �J������Z���W�̃I�t�Z�b�g

    void Start()
    {
        if (target != null)
        {
            // �^�[�Q�b�g�̐^��ɃJ������z�u
            Vector3 newPosition = target.position;
            newPosition.y += YOffset;
            transform.position = newPosition;

            // �J�����̌������^�[�Q�b�g�Ɍ�����
            transform.LookAt(target);

            newPosition.z += ZOffset;
            transform.position = newPosition;
        }

        // �z�u���I�������폜
        Destroy(this);
    }
}

