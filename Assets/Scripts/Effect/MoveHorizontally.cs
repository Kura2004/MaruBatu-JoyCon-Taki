using UnityEngine;
using DG.Tweening;

public class MoveHorizontally : SingletonMonoBehaviour<MoveHorizontally>
{
    [SerializeField]
    private float moveDistance = 5f; // �ړ����鋗��
    [SerializeField]
    private float moveDuration = 2f; // �ړ��ɂ����鎞��
    [SerializeField]
    private Ease moveEase = Ease.Linear; // �C�[�W���O�I�v�V����
    private Transform objectTransform; // �ʏ��Transform

    protected override void OnEnable()
    {
        base.OnEnable();
        objectTransform = GetComponent<Transform>();
    }

    /// <summary>
    /// �w�肵�����������E�Ɉړ����郁�\�b�h
    /// </summary>
    public void MoveRight()
    {
        objectTransform.DOMoveX(objectTransform.position.x + moveDistance, moveDuration).SetEase(moveEase);
    }

    /// <summary>
    /// �w�肵�������������Ɉړ����郁�\�b�h
    /// </summary>
    public void MoveLeft()
    {
        objectTransform.DOMoveX(objectTransform.position.x - moveDistance, moveDuration).SetEase(moveEase);
    }
}