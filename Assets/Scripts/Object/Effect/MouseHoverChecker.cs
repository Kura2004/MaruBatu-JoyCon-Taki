using UnityEngine;

public class MouseHoverChecker : MonoBehaviour
{
    // �}�E�X���I�u�W�F�N�g�ɐG��Ă��邩�ǂ�����Ԃ����\�b�h
    public bool IsMouseOver()
    {
        // �}�E�X�̈ʒu���烌�C�L���X�g���s�����߂�Ray���擾
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        // RaycastHit�ϐ���錾���āA�q�b�g�����擾
        RaycastHit hit;

        // ���C�L���X�g���I�u�W�F�N�g�Ƀq�b�g�������ǂ����𔻒�
        if (Physics.Raycast(ray, out hit))
        {
            // �q�b�g�����I�u�W�F�N�g�����̃X�N���v�g���A�^�b�`����Ă���I�u�W�F�N�g���ǂ����𔻒�
            if (hit.collider.gameObject == gameObject)
            {
                return true; // �}�E�X���I�u�W�F�N�g�ɐG��Ă���
            }
        }
        return false; // �}�E�X���I�u�W�F�N�g�ɐG��Ă��Ȃ�
    }
}

