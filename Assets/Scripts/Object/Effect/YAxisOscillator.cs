using UnityEngine;

public class YAxisOscillator : MonoBehaviour
{
    [SerializeField]
    private float frequency = 1f; // ���g���i�h��̑����j

    [SerializeField]
    private float amplitude = 1f; // �U���i�h��̑傫���j

    [SerializeField]
    private float initialYRange = 1f; // ����Y���W�͈̔́i�����_������ǉ��j

    private float initialY; // ����Y���W
    private float time;

    private void Start()
    {
        // �����_���ȏ���Y���W��ݒ�
        initialY = transform.position.y + Random.Range(-initialYRange, initialYRange);
    }

    private void Update()
    {
        // ���ԂɊ�Â���Y���W���v�Z
        time += Time.deltaTime;
        float newY = initialY + Mathf.Sin(time * frequency) * amplitude;

        // �v�Z����Y���W���I�u�W�F�N�g�ɓK�p
        transform.position = new Vector3(transform.position.x, newY, transform.position.z);
    }

    /// <summary>
    /// ���g����ݒ肷��
    /// </summary>
    public void SetFrequency(float newFrequency)
    {
        frequency = newFrequency;
    }

    /// <summary>
    /// �U����ݒ肷��
    /// </summary>
    public void SetAmplitude(float newAmplitude)
    {
        amplitude = newAmplitude;
    }

    /// <summary>
    /// ����Y���W�͈̔͂�ݒ肷��
    /// </summary>
    public void SetInitialYRange(float range)
    {
        initialYRange = range;
    }
}

