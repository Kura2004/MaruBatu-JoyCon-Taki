using UnityEngine;
using DG.Tweening;
using System.Collections.Generic;

public class ColorTransitionEffect : MonoBehaviour
{
    // �F���^�O�t�����邽�߂�enum
    public enum ColorTag
    {
        Red,
        Green,
        Blue,
        Yellow,
        White,
        Black,
        Gray,
        Cyan,
        Orange
    }

    [SerializeField]
    private float fadeDuration = 2f; // �F���ω�����܂ł̎��ԁi�b�j

    // �C�[�W���O��ݒ�\��
    [SerializeField]
    private Ease easeType = Ease.Linear;

    // �F���C���X�y�N�^�[�ŕҏW�ł���悤�ɂ���
    [SerializeField]
    private Color redColor = Color.red;
    [SerializeField]
    private Color greenColor = Color.green;
    [SerializeField]
    private Color blueColor = Color.blue;
    [SerializeField]
    private Color yellowColor = Color.yellow;
    [SerializeField]
    private Color whiteColor = Color.white;
    [SerializeField]
    private Color blackColor = Color.black;
    [SerializeField]
    private Color grayColor = Color.gray;      // �D�F
    [SerializeField]
    private Color cyanColor = Color.cyan;      // ���F
    [SerializeField]
    private Color orangeColor = new Color(1f, 0.5f, 0f); // �I�����W�F

    private Dictionary<ColorTag, Color> colorDictionary;

    private SpriteRenderer spriteRenderer; // SpriteRenderer�̎Q��

    private void Awake()
    {
        // SpriteRenderer�R���|�[�l���g���擾
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogWarning("SpriteRenderer component is missing on the GameObject.");
        }

        // �F�̎�����������
        colorDictionary = new Dictionary<ColorTag, Color>()
        {
            { ColorTag.Red, redColor },
            { ColorTag.Green, greenColor },
            { ColorTag.Blue, blueColor },
            { ColorTag.Yellow, yellowColor },
            { ColorTag.White, whiteColor },
            { ColorTag.Black, blackColor },
            { ColorTag.Gray, grayColor },
            { ColorTag.Cyan, cyanColor },
            { ColorTag.Orange, orangeColor }
        };
    }

    private void OnEnable()
    {
        onEffect = false;
        StartColorTransition(ColorTag.Gray);
    }

    /// <summary>
    /// �w�肵���F�ɕω������郁�\�b�h
    /// </summary>
    /// <param name="colorTag">�ω�������F�̃^�O</param>
    public void StartColorTransition(ColorTag colorTag)
    {
        if (spriteRenderer != null && colorDictionary.ContainsKey(colorTag))
        {
            // ���݂̐F����w�肵���F�܂�DoTween�ŕ⊮�I�ɕω�������
            Color targetColor = colorDictionary[colorTag];
            spriteRenderer.DOColor(targetColor, fadeDuration)
                          .SetEase(easeType);
        }
        else
        {
            Debug.LogWarning("Invalid color tag or missing SpriteRenderer.");
        }
    }

    /// <summary>
    /// �J���[�^�O�ƐF�̎����ɐV�����F��ǉ����郁�\�b�h
    /// </summary>
    /// <param name="colorTag">�V�����F�̃^�O</param>
    /// <param name="color">�ǉ�����F</param>
    public void AddColor(ColorTag colorTag, Color color)
    {
        if (!colorDictionary.ContainsKey(colorTag))
        {
            colorDictionary.Add(colorTag, color);
        }
        else
        {
            Debug.LogWarning("ColorTag already exists.");
        }
    }

    bool onEffect = false;
    [SerializeField] bool setOrange = false;

    private void Update()
    {
        var turnMana = GameTurnManager.Instance;
        if ((turnMana.IsCurrentTurn(GameTurnManager.TurnState.PlayerRotateGroup) ||
            turnMana.IsCurrentTurn(GameTurnManager.TurnState.OpponentRotateGroup))
            && !onEffect)
        {
            onEffect = true;
            if (setOrange)
            {
                StartColorTransition(ColorTag.Orange);
            }

            else
            {
                StartColorTransition(ColorTag.Cyan);
            }
        }

        if ((turnMana.IsCurrentTurn(GameTurnManager.TurnState.PlayerPlacePiece) ||
            turnMana.IsCurrentTurn(GameTurnManager.TurnState.OpponentPlacePiece))
            && onEffect)
        {
            onEffect = false;
            StartColorTransition(ColorTag.Gray);
        }
    }
}
