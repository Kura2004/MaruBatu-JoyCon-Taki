using UnityEngine;

public class CustomObjectScaler : ObjectScaler
{
    private void Update()
    {
        // ��{�N���X�̋@�\���Ăяo���A�I�u�W�F�N�g�̃T�C�Y���`�F�b�N����
        if (IsObjectSizeReset())
        {
            ResetObjectSize();
        }
    }
}

