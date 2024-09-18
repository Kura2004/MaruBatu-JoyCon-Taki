using UnityEngine;

public class FullscreenParticleSystem : MonoBehaviour
{
    public ParticleSystem particleSystem_;
    [SerializeField] private Camera targetCamera; // �C���X�y�N�^�[�Őݒ�ł���J����
    [SerializeField] private float distanceFromCamera = 5f;
    [SerializeField] private Vector3 cameraPositionOffset = new Vector3(0, 0, 0); // �J�����ʒu�̃I�t�Z�b�g

    void Start()
    {
        if (targetCamera == null)
        {
            targetCamera = Camera.main; // targetCamera���ݒ肳��Ă��Ȃ��ꍇ�A���C���J�������g�p
        }

        // Particle System�̈ʒu���J�����̑O�ɐݒ�
        UpdateParticleSystemPositionAndSize();
        Destroy(this);
    }

    void UpdateParticleSystemPositionAndSize()
    {
        // �J�����̈ʒu�ɃI�t�Z�b�g��K�p����Particle System�̈ʒu���X�V
        Vector3 adjustedCameraPosition = targetCamera.transform.position + cameraPositionOffset;
        particleSystem_.transform.position = adjustedCameraPosition + targetCamera.transform.forward * distanceFromCamera;

        // �J�����̎���p�Ɋ�Â���Box�̃T�C�Y���X�V
        var shape = particleSystem_.shape;
        float height = 2f * distanceFromCamera * Mathf.Tan(targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);
        float width = height * targetCamera.aspect;
        shape.scale = new Vector3(width, height, 1f);
    }
}





