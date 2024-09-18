using UnityEngine;
using TMPro; // TextMeshPro���g�p���邽�߂̖��O���
using DG.Tweening; // DOTween���g�p���邽�߂̖��O���

public class TimeLimitController : SingletonMonoBehaviour<TimeLimitController>
{
    public TMP_Text timerDisplay; // �������Ԃ�\������TextMeshPro
    public float currentTime;    // ���݂̐�������
    public bool isTimerRunning;  // �^�C�}�[�������Ă��邩�ǂ������Ǘ�

    [Header("Effect Settings")]
    [SerializeField] private float effectTriggerTime = 5f; // �G�t�F�N�g���Đ����鎞�ԁi�b�j
    [SerializeField] private float effectDelay = 0.1f; // �G�t�F�N�g�̍Đ���x�点�鎞�ԁi�b�j

    public bool isEffectTriggered = false; // �G�t�F�N�g�����Ƀg���K�[���ꂽ���ǂ���

    private void Start()
    {
        ResetTimer(); // �X�^�[�g���ɐ������Ԃ����Z�b�g
        isTimerRunning = false;
    }

    private void Update()
    {
        if (!GameStateManager.Instance.IsBoardSetupComplete) { return; }

        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime; // ���t���[�����Ԃ�����������
            if (currentTime <= 0f)
            {
                currentTime = 0f;
                ResetEffect();
                TimeUp(); // ���Ԃ��؂ꂽ���̏������Ăяo��
                StopTimer(); // �^�C�}�[���~
                return;
            }
            else if (currentTime <= effectTriggerTime && !isEffectTriggered) 
            {
                TriggerEffects(); // �G�t�F�N�g���Đ�
            }

            UpdateTimerDisplay(); // �\�����X�V
        }
    }

    // �������Ԃ����Z�b�g���郁�\�b�h
    public void ResetTimer()
    {
        currentTime = TimeLimitAdjuster.timeLimit; // TimeLimitAdjuster��timeLimit���g�p���ă��Z�b�g
        isTimerRunning = true; // �^�C�}�[���ĊJ
        isEffectTriggered = false; // �G�t�F�N�g�̃g���K�[��Ԃ����Z�b�g
        ResetEffect();
        Debug.Log("���Ԃ����Z�b�g���܂�");
    }

    public void ResetEffect()
    {
        var chromatic = ChromaticAberrationEffectController.Instance;
        var vignette = VignetteEffectController.Instance;
        if (chromatic != null && vignette != null)
        {
            ScenesAudio.UnsetSeLoop(Saito.SoundManager.SoundManager.SeSoundData.SE.Heart);
            chromatic.StopChromaticAberrationEffect();
            vignette.StopVignetteEffect();
        }
    }

    // �G�t�F�N�g���Đ����郁�\�b�h
    private void TriggerEffects()
    {
        var chromatic = ChromaticAberrationEffectController.Instance;
        var vignette = VignetteEffectController.Instance;
        if (chromatic != null && vignette != null)
        {
            chromatic.StartChromaticAberrationEffect();
            vignette.StartVignetteEffect();
            // DoTween���g���Ēx������������
            DOVirtual.DelayedCall(effectDelay, () =>
            {
                ScenesAudio.SetSeLoop(Saito.SoundManager.SoundManager.SeSoundData.SE.Heart);
            });

            isEffectTriggered = true; // �G�t�F�N�g����x�����Đ����邽�߂̃t���O��ݒ�
        }
    }

    // �������Ԃ�\�����郁�\�b�h
    private void UpdateTimerDisplay()
    {
        timerDisplay.text = currentTime.ToString("F1") + "s"; // �����_�ȉ�1���ŕ\��
    }

    // ���Ԑ؂ꎞ�ɌĂяo����郁�\�b�h
    private void TimeUp()
    {
        Debug.Log("Time is up!");
        ScenesAudio.WinSe();
        FadeManager.Instance.LoadScene("GameOver", 1.0f);
    }

    // �^�C�}�[�̌������~���郁�\�b�h
    public void StopTimer()
    {
        isTimerRunning = false;
    }

    // �^�C�}�[���ĊJ���郁�\�b�h
    public void StartTimer()
    {
        isTimerRunning = true;
    }
}
