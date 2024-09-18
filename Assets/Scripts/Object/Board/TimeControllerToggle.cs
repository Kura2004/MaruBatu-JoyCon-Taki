using UnityEngine;
using DG.Tweening;  // DOTween�̖��O��Ԃ��C���|�[�g
using Saito.SoundManager;

public class TimeControllerToggle : MonoBehaviour
{
    public static bool isTimeStopped = false;  // ���Ԃ��~�܂��Ă��邩�ǂ������Ǘ�����ÓI��bool�^�̕ϐ�

    [SerializeField, Tooltip("��������I�u�W�F�N�g�̃v���n�u")]
    private GameObject objectPrefab;  // ��������I�u�W�F�N�g�̃v���n�u

    [SerializeField, Tooltip("�I�u�W�F�N�g���傫���Ȃ�܂ł̎���")]
    private float growDuration = 2f;  // �I�u�W�F�N�g���傫���Ȃ�܂ł̎���

    [SerializeField, Tooltip("�I�u�W�F�N�g����������ŏI�I�ȃX�P�[��")]
    private Vector3 targetScale = Vector3.one * 2f;  // �I�u�W�F�N�g����������ŏI�I�ȃX�P�[��

    [SerializeField, Tooltip("�I�u�W�F�N�g�̈ʒu�I�t�Z�b�g")]
    private Vector3 positionOffset = Vector3.zero;  // �I�u�W�F�N�g�̈ʒu�I�t�Z�b�g

    [SerializeField, Tooltip("�I�u�W�F�N�g�̏����X�P�[��")]
    private Vector3 initialScale = Vector3.one;  // �I�u�W�F�N�g�̏����X�P�[��

    [SerializeField, Tooltip("�p�[�e�B�N���V�X�e��")]
    private new ParticleSystem particleSystem;  // �p�[�e�B�N���V�X�e��

    //[SerializeField]
    //ObjectShaker backShaker;

    private GameObject spawnedObject;  // �������ꂽ�I�u�W�F�N�g
    private Tweener currentTween;  // ���݂̃A�j���[�V�������Ǘ�����Tweener

    private void OnEnable()
    {
        isTimeStopped = false;
        // �I�u�W�F�N�g�𐶐����A����������
        if (spawnedObject == null && objectPrefab != null)
        {
            Vector3 createPos = transform.position + positionOffset;
            spawnedObject = Instantiate(objectPrefab, createPos, Quaternion.identity);
            spawnedObject.transform.localScale = initialScale;
        }
    }

    // �I�u�W�F�N�g��Ń}�E�X���N���b�N���ꂽ���ɌĂяo�����
    void OnMouseDown()
    {
        if (TimeLimitController.Instance.isEffectTriggered) { return; }
        if (GameStateManager.Instance.IsBoardSetupComplete &&
            !GameStateManager.Instance.IsRotating)
        {
            isTimeStopped = !isTimeStopped;
            // ���Ԓ�~��Ԃ�؂�ւ���
            if (!isTimeStopped)
            {
                EndTimeStop();
            }
            else
            {
                StartTimeStop();
            }
        }
    }

    // ���Ԓ�~���J�n���郁�\�b�h
    private void StartTimeStop()
    {
        TimeLimitController.Instance.StopTimer();
        ScenesAudio.ShootSe();
        //ScenesAudio.PauseBgm();
        //backShaker.StopShake();

        // ���݂̃A�j���[�V�������~
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        // �p�[�e�B�N���V�X�e�����~
        if (particleSystem != null)
        {
            particleSystem.Pause();
        }

        // �I�u�W�F�N�g��傫������
        if (spawnedObject != null)
        {
            currentTween = spawnedObject.transform.DOScale(targetScale, growDuration).SetEase(Ease.OutExpo);
        }
    }

    // ���Ԓ�~���I�����郁�\�b�h
    private void EndTimeStop()
    {
        TimeLimitController.Instance.StartTimer();
        ScenesAudio.RestartSe();
        //ScenesAudio.UnPauseBgm();
        // ���݂̃A�j���[�V�������~
        if (currentTween != null && currentTween.IsActive())
        {
            currentTween.Kill();
        }

        // �I�u�W�F�N�g������������
        if (spawnedObject != null)
        {
            currentTween = spawnedObject.transform.DOScale(initialScale, growDuration).SetEase(Ease.InExpo);
        }

        // �p�[�e�B�N���V�X�e�����ĊJ
        if (particleSystem != null)
        {
            particleSystem.Play();
        }
    }
}
