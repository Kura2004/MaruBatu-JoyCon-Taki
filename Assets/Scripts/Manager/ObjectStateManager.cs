using UnityEngine;

public class ObjectStateManager : SingletonMonoBehaviour<ObjectStateManager>
{
    [SerializeField]
    private string firstObjectTag = "FirstObjectTag"; // 1�ڂ̃I�u�W�F�N�g�̃^�O

    [SerializeField]
    private string secondObjectTag = "SecondObjectTag"; // 2�ڂ̃I�u�W�F�N�g�̃^�O

    [SerializeField]
    private float delayBeforeFinding = 1f; // �^�O�ŃI�u�W�F�N�g��T���O�̒x�����ԁi�b�j

    private GameObject firstObject; // 1�ڂ̃I�u�W�F�N�g
    private GameObject secondObject; // 2�ڂ̃I�u�W�F�N�g

    private void Start()
    {
        // �x�����ă^�O�̃Q�[���I�u�W�F�N�g��T���������J�n
        Invoke(nameof(FindTaggedObjects), delayBeforeFinding);
    }

    private void FindTaggedObjects()
    {
        // �^�O�ŃQ�[���I�u�W�F�N�g������
        firstObject = GameObject.FindGameObjectWithTag(firstObjectTag);
        secondObject = GameObject.FindGameObjectWithTag(secondObjectTag);

        // �I�u�W�F�N�g��������Ȃ������ꍇ�̃��O
        if (firstObject == null)
        {
            Debug.LogWarning($"�^�O '{firstObjectTag}' �̃I�u�W�F�N�g��������܂���ł����B");
        }

        if (secondObject == null)
        {
            Debug.LogWarning($"�^�O '{secondObjectTag}' �̃I�u�W�F�N�g��������܂���ł����B");
        }
        secondObject.SetActive(false);
    }

    /// <summary>
    /// 1�ڂ̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
    /// </summary>
    public void SetFirstObjectActive(bool isActive)
    {
        if (firstObject != null)
        {
            firstObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("FirstObject���ݒ肳��Ă��܂���B");
        }
    }

    /// <summary>
    /// 2�ڂ̃I�u�W�F�N�g���A�N�e�B�u�ɂ���
    /// </summary>
    public void SetSecondObjectActive(bool isActive)
    {
        if (secondObject != null)
        {
            secondObject.SetActive(isActive);
        }
        else
        {
            Debug.LogWarning("SecondObject���ݒ肳��Ă��܂���B");
        }
    }

    /// <summary>
    /// 1�ڂ�2�ڂ̃I�u�W�F�N�g�̃A�N�e�B�u��Ԃ��g�O������
    /// </summary>
    public void ToggleObjects(bool isFirstActive, bool isSecondActive)
    {
        SetFirstObjectActive(isFirstActive);
        SetSecondObjectActive(isSecondActive);
    }
}
