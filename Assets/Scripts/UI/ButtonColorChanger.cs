using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using DG.Tweening;

public class ButtonColorChanger : MonoBehaviour
{
    [SerializeField] private Button button;
    [SerializeField] private Color normalColor = Color.white;
    [SerializeField] private Color highlightedColor = Color.gray;
    [SerializeField] private float transitionDuration = 1.0f; // �F���ς��܂ł̎���

    private Image buttonImage;

    void Start()
    {
        buttonImage = button.GetComponent<Image>();

        // EventTrigger���擾�܂��͒ǉ�
        EventTrigger trigger = button.GetComponent<EventTrigger>();
        if (trigger == null)
        {
            trigger = button.gameObject.AddComponent<EventTrigger>();
        }

        // PointerEnter�C�x���g��ݒ�
        EventTrigger.Entry entryEnter = new EventTrigger.Entry();
        entryEnter.eventID = EventTriggerType.PointerEnter;
        entryEnter.callback.AddListener((eventData) => { OnPointerEnter(); });
        trigger.triggers.Add(entryEnter);

        // PointerExit�C�x���g��ݒ�
        EventTrigger.Entry entryExit = new EventTrigger.Entry();
        entryExit.eventID = EventTriggerType.PointerExit;
        entryExit.callback.AddListener((eventData) => { OnPointerExit(); });
        trigger.triggers.Add(entryExit);
    }

    public void OnPointerEnter()
    {
        buttonImage.DOKill(); // �����̃g�D�C�[�����~
        buttonImage.DOColor(highlightedColor, transitionDuration);
    }

    public void OnPointerExit()
    {
        buttonImage.DOKill(); // �����̃g�D�C�[�����~
        buttonImage.DOColor(normalColor, transitionDuration);
    }
}



