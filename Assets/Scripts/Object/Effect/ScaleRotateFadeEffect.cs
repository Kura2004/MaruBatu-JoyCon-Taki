using UnityEngine;
using DG.Tweening;  // DoTween�̖��O���
using UnityEngine.SceneManagement;  // �V�[���Ǘ��̂��߂̖��O���

public class ScaleRotateFadeEffect : MonoBehaviour
{
    // ��]���̑I����
    public enum Axis
    {
        X,
        Y,
        Z
    }

    [SerializeField] private float initialScale = 0.5f;      // �摜�̏����X�P�[��
    [SerializeField] private Vector3 initialRotation = Vector3.zero;  // �摜�̏�����]�p�x
    [SerializeField] private float startMenuScale = 1.5f;     // StartMenu�V�[���ł̊g���̖ڕW�X�P�[��
    [SerializeField] private float fourByFourScale = 2f;      // 4x4�V�[���ł̊g���̖ڕW�X�P�[��
    [SerializeField] private float gameOverScale = 3f;        // GameOver�V�[���ł̊g���̖ڕW�X�P�[��
    [SerializeField] private float animationDuration = 1f;    // �A�j���[�V�����̎���
    [SerializeField] private float fadeDuration = 1f;         // �t�F�[�h�A�E�g�̎���
    [SerializeField] private Axis rotationAxis = Axis.Y;      // ��]��
    private float targetScale;

    private void Start()
    {
        SetTargetScale();
        InitializeEffect();
        ApplyEffect();
    }

    private void SetTargetScale()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "StartMenu":
                targetScale = startMenuScale;
                break;
            case "4x4":
                targetScale = fourByFourScale;
                break;
            case "GameOver":
                targetScale = gameOverScale;
                break;
            default:
                targetScale = 2f; // �f�t�H���g�l
                break;
        }
    }

    private void InitializeEffect()
    {
        // �����̃X�P�[���Ɖ�]��ݒ�
        transform.localScale = Vector3.one * initialScale;
        transform.rotation = Quaternion.Euler(initialRotation);
    }

    private CanvasGroup GetOrAddCanvasGroup()
    {
        // CanvasGroup�R���|�[�l���g���擾�܂��͒ǉ��i�t�F�[�h�A�E�g�p�j
        CanvasGroup canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null)
        {
            canvasGroup = gameObject.AddComponent<CanvasGroup>();
        }
        return canvasGroup;
    }

    private Vector3 GetRotationVector()
    {
        // ��]���ɉ�������]�x�N�g�����擾
        switch (rotationAxis)
        {
            case Axis.X:
                return new Vector3(360, 0, 0);
            case Axis.Y:
                return new Vector3(0, 360, 0);
            case Axis.Z:
                return new Vector3(0, 0, 360);
            default:
                return Vector3.zero;
        }
    }

    private void ApplyEffect()
    {
        CanvasGroup canvasGroup = GetOrAddCanvasGroup();

        Vector3 rotationVector = GetRotationVector();
        Vector3 finalRotation = transform.eulerAngles + rotationVector;

        // �A�j���[�V�����ݒ�
        Sequence animationSequence = DOTween.Sequence();
        animationSequence
            .Append(transform.DOScale(targetScale, animationDuration).SetEase(Ease.InOutQuad))
            .Join(transform.DORotate(finalRotation, animationDuration, RotateMode.FastBeyond360).SetEase(Ease.InOutQuad))
            .Join(canvasGroup.DOFade(0, fadeDuration).SetDelay(animationDuration - fadeDuration))
            .OnComplete(() => Destroy(gameObject));  // �A�j���[�V����������������I�u�W�F�N�g���폜
    }
}
