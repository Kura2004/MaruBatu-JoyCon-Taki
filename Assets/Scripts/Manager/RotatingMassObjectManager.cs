using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;

public class RotatingMassObjectManager : MonoBehaviour
{
    [SerializeField]
    private string massTag = "Mass"; // Tag�� (Inspector�ŕύX�\)

    [SerializeField]
    public float rotationDuration = 1f; // ��]�ɂ����鎞�ԁi�b�j

    [SerializeField]
    private float rotationDegrees = 90f; // ��]����p�x�i�x�j

    private GameObject[] mass = new GameObject[4];
    private int massIndex = 0;

    private Dictionary<MassPoint, GameObject> massPlaceholders = new Dictionary<MassPoint, GameObject>();

    private enum MassPoint
    {
        Point1,
        Point2,
        Point3,
        Point4
    }

    public bool AnyMassClicked()
    {
        foreach (var obj in mass)
        {
            if (obj != null && obj.GetComponent<ObjectColorChanger>()?.isClicked == true)
            {
                return true; // �����ꂩ�̃I�u�W�F�N�g���N���b�N����Ă����ꍇ
            }
        }
        return false; // �S�ẴI�u�W�F�N�g���N���b�N����Ă��Ȃ��ꍇ
    }

    public void OnTriggerStay(Collider other)
    {
        // Tag��Mass�̃I�u�W�F�N�g�ɓ������Ă���ꍇ
        if (other.CompareTag(massTag))
        {
            // �I�u�W�F�N�g��o�^
            mass[massIndex] = other.gameObject;
            massIndex = (massIndex + 1) % mass.Length;
        }
    }

    public void StartRotationLeft()
    {
        StartCoroutine(RotateObject(-rotationDegrees));
    }

    public void StartRotationRight()
    {
        StartCoroutine(RotateObject(rotationDegrees));
    }

    private IEnumerator RotateObject(float degrees)
    {
        if (GameStateManager.Instance.IsRotating) yield break;

        MakeObjectsChildren();

        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, degrees, 0);
        transform.rotation = endRotation; // �ŏI�I�ȉ�]��ݒ�

        // �I�u�W�F�N�g�̈ʒu��ΐ��֐��I�ɕ⊮���Ĉړ�
        foreach (var entry in massPlaceholders)
        {
            MassPoint point = entry.Key;
            GameObject placeholder = entry.Value;
            int index = (int)point;

            if (mass[index] != null)
            {
                // �ΐ��֐��I�ȃC�[�W���O���g���Ĉړ�
                mass[index].transform.DOMove(placeholder.transform.position, rotationDuration)
                    .SetEase(Ease.InOutQuad); // ������Ease�𒲐�
            }
        }

        // ��]���I�������e�q�֌W�����Z�b�g���A��]�������������s
        yield return new WaitForSeconds(rotationDuration);

        ResetParentRelationships();
        OnRotationComplete();
    }

    // �I�u�W�F�N�g�̈ʒu���X�V���郁�\�b�h�͕s�v�ɂȂ�̂ō폜

    // MakeObjectsChildren, ResetParentRelationships, OnRotationComplete ���\�b�h�͂��̂܂܎g�p

    private void MakeObjectsChildren()
    {
        if (GameStateManager.Instance.IsRotating) return;
        GameStateManager.Instance.StartRotation(); // ��]�J�n�t���O�𗧂Ă�

        for (int i = 0; i < mass.Length; i++)
        {
            if (mass[i] != null)
            {
                GameObject placeholder = new GameObject("MassPlaceholder" + i);
                placeholder.transform.position = mass[i].transform.position;
                placeholder.transform.rotation = mass[i].transform.rotation;

                placeholder.transform.SetParent(transform);
                massPlaceholders[(MassPoint)i] = placeholder;

                //mass[i].GetComponent<ObjectScaler>().IgnoreMouseInput();
            }
        }
    }

    private void ResetParentRelationships()
    {
        foreach (var obj in mass)
        {
            if (obj != null)
            {
                obj.transform.SetParent(null);
                //obj.GetComponent<ObjectScaler>().ResumeMouseInput();
            }
        }

        foreach (var placeholder in massPlaceholders.Values)
        {
            Destroy(placeholder);
        }

        massPlaceholders.Clear();
    }

    private void OnRotationComplete()
    {
        GameStateManager.Instance.EndRotation(); // ��]�I���t���O��߂�
        TimeLimitController.Instance.ResetTimer();
        TimeLimitController.Instance.StartTimer();
        GameTurnManager.Instance.SetTurnChange(true);
        GameTurnManager.Instance.AdvanceTurn(); // �^�[����i�߂�
        ScenesAudio.SetSe();
        ObjectStateManager.Instance.SetFirstObjectActive(true);
        ObjectStateManager.Instance.SetSecondObjectActive(false);
    }
}

