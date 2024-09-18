using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(VignetteColorBlender))]
public class VignetteColorBlenderEditor : Editor
{
    private void OnEnable()
    {
        // �ҏW���Ƀv���p�e�B���ύX����邽�тɂ��̃��\�b�h���Ă΂��
        serializedObject.Update();
    }

    public override void OnInspectorGUI()
    {
        // �v���p�e�B��`��
        serializedObject.Update();

        // �C���X�y�N�^�[�ŕ\������v���p�e�B��`��
        DrawDefaultInspector();

        VignetteColorBlender blender = (VignetteColorBlender)target;

        // �v���p�e�B���ύX���ꂽ�ꍇ��UpdateVignetteColor���Ăяo��
        if (GUI.changed)
        {
            blender.UpdateVignetteColor();
        }

        serializedObject.ApplyModifiedProperties();
    }
}
