using UnityEngine;
using UnityEngine.UI;
using TMPro; // TextMeshPro���g�p���邽�߂̖��O���

public class TimeLimitAdjuster : MonoBehaviour
{
    [SerializeField] private Button button10SecPlus;  // 10�b�{�{�^��
    [SerializeField] private Button button10SecMinus; // 10�b�}�C�i�X�{�^��
    [SerializeField] private Button button1SecPlus;   // 1�b�{�{�^��
    [SerializeField] private Button button1SecMinus;  // 1�b�}�C�i�X�{�^��
    [SerializeField] private TMP_Text valueDisplay; // TextMeshPro���g�p����e�L�X�g

    public static float timeLimit = 30f; // ��������Q�[���̐�������

    [System.Obsolete]
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
        // �{�^���̃N���b�N�C�x���g�Ƀ��X�i�[��ǉ�
        button10SecPlus.onClick.AddListener(() => AdjustTimeLimit(10));   // 10�b�{�{�^���̃��X�i�[��ǉ�
        button10SecMinus.onClick.AddListener(() => AdjustTimeLimit(-10)); // 10�b�}�C�i�X�{�^���̃��X�i�[��ǉ�
        button1SecPlus.onClick.AddListener(() => AdjustTimeLimit(1));     // 1�b�{�{�^���̃��X�i�[��ǉ�
        button1SecMinus.onClick.AddListener(() => AdjustTimeLimit(-1));   // 1�b�}�C�i�X�{�^���̃��X�i�[��ǉ�

        // �����l��\��
        UpdateValueDisplay();
    }

    private void AdjustTimeLimit(int amount)
    {
        timeLimit += amount;
        timeLimit = Mathf.Clamp(timeLimit, 10f, 60f); // �ŏ��l10�A�ő�l60�ɐ���
        UpdateValueDisplay();
    }

    private void UpdateValueDisplay()
    {
        valueDisplay.text = timeLimit.ToString("F0") + "s"; // �����ŕ\��
    }
}



