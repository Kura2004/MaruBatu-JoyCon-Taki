using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro���g�p���邽�߂̖��O���
using System.Collections;

public class TimeLimitAdjuster : MonoBehaviour
{
    [SerializeField] private TMP_Text valueDisplay;   // TextMeshPro���g�p����e�L�X�g
    [SerializeField] private float cooldownDuration = 1f; // �N�[���_�E������

    public static int timeLimit = 30; // ��������Q�[���̐�������
    private bool canAdjustTime = true;   // �^�C���������󂯕t���邩�ǂ���
    private bool inputCooldownActive = false; // ���̓N�[���_�E���̃t���O

    private void Awake()
    {
        // ���̃C���X�^���X�����ɑ��݂���ꍇ�́A���݂̃C���X�^���X��j������
        if (FindObjectsOfType<TimeLimitAdjuster>().Length > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); // �V�[���؂�ւ����ɃI�u�W�F�N�g���ێ�
        }
    }

    private void Start()
    {
        // �����l��\��
        UpdateValueDisplay();
    }

    private void Update()
    {
        // ������󂯕t���Ȃ��ꍇ�͏������Ȃ�
        if (inputCooldownActive) return;

        // Input.GetAxis("Vertical") �̒l�ɉ����ď\�̈ʂ�ύX
        //������ύX
        float verticalInput = Input.GetAxis("Vertical");

        // �c���̓��͂�0�ȊO�̂Ƃ��̂ݏ���
        if (verticalInput != 0 && canAdjustTime)
        {
            if (verticalInput > 0)
            {
                AdjustTimeLimit(10); // �\�̈ʂ�1���₷
            }
            else if (verticalInput < 0)
            {
                AdjustTimeLimit(-10); // �\�̈ʂ�1���炷
            }

            StartInputCooldown(); // �N�[���_�E�����J�n
        }
    }

    int prevTimeLimit = 0;
    private void AdjustTimeLimit(int amount)
    {
        if (!canAdjustTime) return;

        timeLimit += amount;
        timeLimit = Mathf.Clamp(timeLimit, 20, 40); // �ŏ��l20�A�ő�l40�ɐ���

        if (timeLimit != prevTimeLimit)
        {
            ScenesAudio.ClickSe();
        }

        UpdateValueDisplay();

        // �^�C����������莞�Ԗ����ɂ���
        StartCoroutine(DisableTimeAdjustmentTemporarily());
        prevTimeLimit = timeLimit;
    }

    private void UpdateValueDisplay()
    {
        valueDisplay.text = timeLimit.ToString("F0") + "s"; // �����ŕ\��
    }

    // �N�[���_�E�����J�n����
    private IEnumerator DisableTimeAdjustmentTemporarily()
    {
        canAdjustTime = false;
        yield return new WaitForSeconds(cooldownDuration); // ��莞�ԑ҂�
        canAdjustTime = true;
    }

    // ���͂̃N�[���_�E�����J�n
    private void StartInputCooldown()
    {
        inputCooldownActive = true;
        StartCoroutine(ResetInputCooldown());
    }

    private IEnumerator ResetInputCooldown()
    {
        yield return new WaitForSeconds(cooldownDuration);
        inputCooldownActive = false;
    }
}