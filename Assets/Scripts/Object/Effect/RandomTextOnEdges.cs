using UnityEngine;
using TMPro;

public class RandomTextOnEdges : MonoBehaviour
{
    [Header("Text Settings")]
    [SerializeField] private TMP_FontAsset font; // �g�p����t�H���g
    [SerializeField] private int textLength = 5; // �����_��������̒���
    [SerializeField] private Color textColor = Color.black; // �e�L�X�g�̐F
    [SerializeField] private float textChangeInterval = 1f; // �������ς��Ԋu (�b)
    [SerializeField] private float fontSize = 40f; // �t�H���g�T�C�Y

    private TextMeshPro textMeshPro;
    private float timeSinceLastChange;
    private const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";

    void Start()
    {
        // �e�L�X�g�I�u�W�F�N�g�𐶐�
        GameObject textObject = new GameObject("EdgeText");
        textObject.transform.SetParent(transform);
        textObject.transform.localPosition = Vector3.zero;
        textObject.transform.Rotate(90, 0, 0);

        textMeshPro = textObject.AddComponent<TextMeshPro>();
        textMeshPro.font = font;
        textMeshPro.fontSize = fontSize; // �t�H���g�T�C�Y��ݒ�
        textMeshPro.alignment = TextAlignmentOptions.Center;
        textMeshPro.color = textColor;

        // ����������ݒ�
        UpdateRandomText();
    }

    void Update()
    {
        timeSinceLastChange += Time.deltaTime;

        if (ShouldUpdateText())
        {
            UpdateRandomText();
            timeSinceLastChange = 0;
        }
    }

    private bool ShouldUpdateText()
    {
        // �����������\�b�h��
        return timeSinceLastChange >= textChangeInterval && !TimeControllerToggle.isTimeStopped;
    }

    private void UpdateRandomText()
    {
        // �����_���ȕ�����𐶐�
        string randomText = GenerateRandomString(textLength);

        // �e�L�X�g���X�V
        textMeshPro.text = randomText;
    }

    private string GenerateRandomString(int length)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        System.Random random = new System.Random();
        for (int i = 0; i < length; i++)
        {
            sb.Append(chars[random.Next(chars.Length)]);
        }
        return sb.ToString();
    }
}
