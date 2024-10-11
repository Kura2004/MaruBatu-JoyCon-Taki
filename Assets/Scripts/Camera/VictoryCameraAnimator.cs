using UnityEngine;
using DG.Tweening;

/// <summary>
/// �������o���̃J�����ړ��𐧌䂷��N���X
/// </summary>
public class VictoryCameraAnimator : SingletonMonoBehaviour<VictoryCameraAnimator>
{
    [SerializeField]
    private Camera mainCamera;  // �C���X�y�N�^�[�Őݒ�ł���J����

    [SerializeField]
    private float moveDuration = 1.0f;  // �J�������ړ����鎞��
    [SerializeField]
    private Ease moveEase = Ease.OutBounce;  // �������o�p�̃C�[�W���O
    [SerializeField]
    private float xCameraOffset = 5.0f;  // �J������X���ɉ������ړ��ʁi�C���X�y�N�^�[�Őݒ�j

    private bool hasAnimated = false;  // �A�j���[�V���������s���ꂽ���ǂ������Ǘ�����t���O

    [SerializeField]
    private RectTransform playerImage1P;  // �v���C���[�摜1��RectTransform
    [SerializeField]
    private RectTransform playerImage2P;  // �v���C���[�摜2��RectTransform

    [SerializeField] float xImageOffset = 5.0f;
    [SerializeField] float adjustOffset = 5.0f;

    [SerializeField] LightningAnimator lightningAnimator;

    [SerializeField] UIObjectShaker shaker1P;
    [SerializeField] UIObjectShaker shaker2P;

    [SerializeField] PlayerImageAnimator animator1P;
    [SerializeField] PlayerImageAnimator animator2P;

    [SerializeField] float delayDuration = 0;
    private void Start()
    {
        hasAnimated = false;
    }

    /// <summary>
    /// �J�������E�ɕ⊮�I�Ɉړ������āA���������o����.
    /// </summary>
    public void MoveCameraRightForVictory()
    {
        if (!hasAnimated)  // �A�j���[�V�����������s�̏ꍇ�̂�
        {
            if (mainCamera != null)
            {
                // ���݂̃J�����ʒu���擾
                Vector3 currentPosition = mainCamera.transform.position;
                Vector3 targetPosition = currentPosition + new Vector3(xCameraOffset, 0, 0);

                // DOTween���g���ĕ⊮�I�Ɉړ�
                mainCamera.transform.DOMove(targetPosition, moveDuration).SetEase(moveEase)
                                        .OnComplete(() =>
                                        {
                                            lightningAnimator.StartLightningAnimationBlue();
                                            shaker2P.ShakeUIElement();

                                            DOVirtual.DelayedCall(delayDuration,
                                                () => ScenesLoader.Instance.LoadGameOver(Color.white));
                                        });

                // playerImage1P�����[�J�����W�ō��ɕ⊮�I�Ɉړ�
                if (playerImage1P != null)
                {
                    playerImage1P.DOLocalMoveX(playerImage1P.localPosition.x - xImageOffset - adjustOffset,
                        moveDuration).SetEase(moveEase);
                }

                // playerImage2P�����[�J�����W�ŉE�ɕ⊮�I�Ɉړ�
                if (playerImage2P != null)
                {
                    playerImage2P.DOLocalMoveX(playerImage2P.localPosition.x - xImageOffset ,
                        moveDuration).SetEase(moveEase);
                }

                animator1P.ChangeSpritesColor(Color.gray, 0.3f);
                animator2P.ChangeSpritesColor(Color.clear, 0.3f);
                animator2P.OnWinImages();
                animator2P.ChangeWinSpritesColor(Color.white, 0.3f);

                hasAnimated = true;  // �A�j���[�V�������s�ς݂ɐݒ�
            }
            else
            {
                Debug.LogWarning("�J�������ݒ肳��Ă��܂���B");
            }
        }
        else
        {
            Debug.LogWarning("�A�j���[�V�����͂��łɎ��s����܂����B");
        }
    }

    /// <summary>
    /// �J���������ɕ⊮�I�ɖ߂��āA�������o�����Z�b�g.
    /// </summary>
    public void MoveCameraLeftToResetVictory()
    {
        if (!hasAnimated)  // �A�j���[�V�����������s�̏ꍇ�̂�
        {
            if (mainCamera != null)
            {
                // ���݂̃J�����ʒu���擾
                Vector3 currentPosition = mainCamera.transform.position;
                Vector3 targetPosition = currentPosition - new Vector3(xCameraOffset, 0, 0);

                // DOTween���g���ĕ⊮�I�ɖ߂�
                mainCamera.transform.DOMove(targetPosition, moveDuration).SetEase(moveEase)
                    .OnComplete(() =>
                    {
                        lightningAnimator.StartLightningAnimationRed();
                        shaker1P.ShakeUIElement();

                        DOVirtual.DelayedCall(delayDuration,
                            () => ScenesLoader.Instance.LoadGameOver(Color.white));
                    });

                // playerImage1P�����[�J�����W�ō��ɕ⊮�I�Ɉړ�
                if (playerImage1P != null)
                {
                    playerImage1P.DOLocalMoveX(playerImage1P.localPosition.x + xImageOffset ,
                        moveDuration).SetEase(moveEase);
                }

                // playerImage2P�����[�J�����W�ŉE�ɕ⊮�I�Ɉړ�
                if (playerImage2P != null)
                {
                    playerImage2P.DOLocalMoveX(playerImage2P.localPosition.x + xImageOffset + adjustOffset,
                        moveDuration).SetEase(moveEase);
                }

                animator2P.ChangeSpritesColor(Color.gray, 0.3f);
                animator1P.ChangeSpritesColor(Color.clear, 0.3f);
                animator1P.OnWinImages();
                animator1P.ChangeWinSpritesColor(Color.yellow, 0.3f);

                hasAnimated = true;  // �A�j���[�V�������s�ς݂ɐݒ�
            }
            else
            {
                Debug.LogWarning("�J�������ݒ肳��Ă��܂���B");
            }
        }
        else
        {
            Debug.LogWarning("�A�j���[�V�����͂��łɎ��s����܂����B");
        }
    }

    /// <summary>
    /// �蓮�ŃJ������ݒ肷�郁�\�b�h.
    /// </summary>
    /// <param name="camera">�ݒ肷��J����</param>
    public void SetVictoryCamera(Camera camera)
    {
        mainCamera = camera;
        Debug.Log("�������o�p�̃J�������ݒ肳��܂���: " + camera.name);
    }

    /// <summary>
    /// �A�j���[�V�����t���O�����Z�b�g���郁�\�b�h�i�K�v�ɉ����ČĂяo���j.
    /// </summary>
    public void ResetAnimationFlag()
    {
        hasAnimated = false;
    }
}
