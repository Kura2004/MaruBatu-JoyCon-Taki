using UnityEngine;
using DG.Tweening; // DOTween���g�����߂̖��O���

public class ObjectStateManager : SingletonMonoBehaviour<ObjectStateManager>
{
    [SerializeField]
    private string firstObjectTag = "FirstObjectTag"; // 1�ڂ̃I�u�W�F�N�g�̃^�O

    [SerializeField]
    private string secondObjectTag = "SecondObjectTag"; // 2�ڂ̃I�u�W�F�N�g�̃^�O

    [SerializeField]
    private float delayBeforeFinding = 1f; // �^�O�ŃI�u�W�F�N�g��T���O�̒x�����ԁi�b�j

    [Header("�㉺�^���̐ݒ�")]
    [SerializeField] private float moveDuration = 2f;  // �ړ��ɂ����鎞��
    [SerializeField] private float moveDistance = 2f;  // �ړ�����͈͂̋���

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

        if (secondObject != null)
        {
            secondObject.SetActive(false); // �ŏ���2�ڂ̃I�u�W�F�N�g���A�N�e�B�u�ɐݒ�
        }
    }

    /// <summary>
    /// 1�ڂ̃I�u�W�F�N�g�����݂̈ʒu����w��͈͂ŏ㉺�^��������
    /// </summary>
    public void MoveFirstObjectUpDown(bool isActive)
    {
        if (firstObject != null)
        {
            // ���݂̈ʒu����ɏ㉺�^��������
            firstObject.transform.DOMoveY(firstObject.transform.position.y + moveDistance, moveDuration)
                .SetLoops(2, LoopType.Yoyo) // 1��オ���Ė߂铮����ݒ�
                .OnComplete(() => SetFirstObjectActive(isActive)); // ���̈ʒu�ɖ߂�����A�N�e�B�u��Ԃ�؂�ւ���
        }
        else
        {
            Debug.LogWarning("FirstObject���ݒ肳��Ă��܂���B");
        }
    }

    /// <summary>
    /// 2�ڂ̃I�u�W�F�N�g�����݂̈ʒu����w��͈͂ŏ㉺�^��������
    /// </summary>
    public void MoveSecondObjectUpDown(bool isActive)
    {
        if (secondObject != null)
        {
            // ���݂̈ʒu����ɏ㉺�^��������
            secondObject.transform.DOMoveY(secondObject.transform.position.y + moveDistance, moveDuration)
                .SetLoops(2, LoopType.Yoyo) // 1��オ���Ė߂铮����ݒ�
                .OnComplete(() => SetSecondObjectActive(isActive)); // ���̈ʒu�ɖ߂�����A�N�e�B�u��Ԃ�؂�ւ���
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

    private void SetFirstObjectActive(bool isActive)
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

    private void SetSecondObjectActive(bool isActive)
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
}
