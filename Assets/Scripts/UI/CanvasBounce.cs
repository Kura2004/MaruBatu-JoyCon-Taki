using UnityEngine;
using DG.Tweening;

public class CanvasBounce : MonoBehaviour
{
    [SerializeField] protected RectTransform canvasRectTransform;
    [SerializeField] protected GameObject canvasObject; // �A�j���[�V������������L�����o�X��GameObject��ǉ�
    [SerializeField] protected float initialDropHeight = 1000f; // �L�����o�X�������n�߂鍂��
    [SerializeField] protected float groundY = -500f; // �n�ʂ�Y���W
    [SerializeField] protected float bounceHeight = 200f; // �����̒e�ލ���
    [SerializeField] protected int bounceCount = 3; // �e�މ�
    [SerializeField] protected float initialBounceDuration = 0.5f; // �����̒e�ރA�j���[�V�����̎���
    [SerializeField] protected float heightDampingFactor = 0.5f; // �����̌�����
    [SerializeField] protected float durationDampingFactor = 0.7f; // ���Ԃ̌�����
    [SerializeField] protected float riseDuration = 0.3f; // �㏸�A�j���[�V�����̎���
    [SerializeField] protected bool dropOnStart = false; // �ŏ���DropCanvas���Ăяo�����ǂ����̃t���O

    protected bool isFalling = false;
    protected bool isBouncingComplete = true; // �o�E���h�A�j���[�V�����̊����t���O
    public static bool isBlocked = false;

    protected virtual void Start()
    {
        if (dropOnStart)
        {
            InitializeCanvasPosition();
            isFalling = false; // �����t���O�����Z�b�g
            isBouncingComplete = true; // �o�E���h�A�j���[�V���������������t���O��ݒ�
        }

        else
        {
            // �L�����o�X��GameObject���A�N�e�B�u�ɐݒ�
            canvasObject.SetActive(false);
        }
    }

    void InitializeCanvasPosition()
    {
        InitializeDrop();
        Vector3 setPos = canvasRectTransform.localPosition;
        setPos.y = groundY;
        canvasRectTransform.localPosition = setPos;
    }

    protected virtual void Update()
    {
        // �L�����o�X�������������
        if (ShouldDropCanvas())
        {
            //DropCanvas();
            Debug.Log("�L�����o�X���������܂�");
        }

        // �o�E���h���������Ă���ꍇ�A���� Q �L�[�������ꂽ�Ƃ��ɃL�����o�X���㏸������
        if ((Input.GetButtonDown("1P_Decision") || Input.GetKeyDown(KeyCode.Q)) && !isFalling && isBouncingComplete)
        {
            RiseCanvas();

            if (dropOnStart)
            {
                GameTurnManager.Instance.IsGameStarted = true;
                GameStateManager.Instance.StartBoardSetup(1.6f);
                TimeLimitController.Instance.ResetTimer();
                TimeLimitController.Instance.StopTimer();
                Destroy(this);
            }

            Debug.Log("�L�����o�X���㏸���܂�");
        }

        //������ς��ė~����
        if (Input.GetButtonDown("1P_Back"))
        {
            ScenesLoader.Instance.LoadStartMenu();
            Debug.Log("�X�^�[�g��ʂɖ߂�܂�");
        }
    }

    protected virtual bool ShouldDropCanvas()
    {
        return Input.GetKeyDown(KeyCode.Q) && !isFalling && canvasRectTransform.anchoredPosition.y != groundY;
    }

    protected void InitializeDrop()
    {
        isFalling = true;
        isBouncingComplete = false; // �o�E���h�A�j���[�V�����̃t���O�����Z�b�g
        isBlocked = true;
    }

    protected virtual void DropCanvas()
    {
        InitializeDrop();

        // �L�����o�X���A�N�e�B�u�ɂ���
        canvasObject.SetActive(true);

        TimeLimitController.Instance.StopTimer();

        // �L�����o�X�������̍����ɐݒ�
        canvasRectTransform.anchoredPosition = new Vector2(canvasRectTransform.anchoredPosition.x, initialDropHeight);

        // �����A�j���[�V����
        canvasRectTransform.DOAnchorPosY(groundY, initialBounceDuration).SetEase(Ease.InQuad).OnComplete(Bounce);
    }

    protected virtual void Bounce()
    {
        float currentBounceHeight = bounceHeight;
        float currentBounceDuration = initialBounceDuration;
        Sequence sequence = DOTween.Sequence();

        for (int i = 0; i < bounceCount; i++)
        {
            // �o�E���h�A�j���[�V�������I�������ɓ���̃��\�b�h���Ă�
            sequence.AppendCallback(() => ScenesAudio.FallSe());

            sequence.Append(canvasRectTransform.DOAnchorPosY(groundY + currentBounceHeight, currentBounceDuration).SetEase(Ease.OutQuad));
            sequence.Append(canvasRectTransform.DOAnchorPosY(groundY, currentBounceDuration).SetEase(Ease.InQuad));

            // �e�ލ����Ǝ��Ԃ�����������
            currentBounceHeight *= heightDampingFactor;
            currentBounceDuration *= durationDampingFactor;
        }

        sequence.OnComplete(() =>
        {
            isFalling = false; // �����t���O�����Z�b�g
            isBouncingComplete = true; // �o�E���h�A�j���[�V���������������t���O��ݒ�
            ScenesAudio.FallSe();
        });

        sequence.Play();
    }

    protected virtual void RiseCanvas()
    {
        if (!isFalling)
        {
            // �L�����o�X��n�ʂ̈ʒu�ɐݒ�
            canvasRectTransform.anchoredPosition = new Vector2(canvasRectTransform.anchoredPosition.x, groundY);

            // �㏸�A�j���[�V����
            canvasRectTransform.DOAnchorPosY(initialDropHeight, riseDuration).SetEase(Ease.OutQuad).OnComplete(() =>
            {
                if (dropOnStart)
                    Destroy(this);
                // �A�j���[�V����������A�L�����o�X���A�N�e�B�u�ɐݒ�
                canvasObject.SetActive(false);

            });
        }
        isBlocked = false;

        if (!TimeControllerToggle.isTimeStopped && !TimeLimitController.Instance.isTimerRunning)
        {
            TimeLimitController.Instance.StartTimer();
        }
    }
}
