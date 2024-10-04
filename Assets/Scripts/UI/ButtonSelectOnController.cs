using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;  

public class ButtonSelectOnController : MonoBehaviour
{
    [SerializeField] private ButtonManager buttonManager;
    [SerializeField] private float inputCooldown = 0.5f;
    [SerializeField] private float colorTransitionDuration = 0.3f;
    [SerializeField] private float scaleMultiplier = 1.2f;
    [SerializeField] private Color selectedColor = Color.green;
    [SerializeField] private Color defaultColor = Color.white;
    [SerializeField] private List<ButtonTagPair> buttonTagPairs;

    private int currentindex = 0;
    private bool canInput = true;
    private Dictionary<int, Vector3> originalScales = new Dictionary<int, Vector3>();
    private List<Button> buttonList = new List<Button>();

    [SerializeField] VolumeSettings volume;

    // Start is called before the first frame update
    void Start()
    {
       if(buttonManager != null && buttonManager.buttons.Count > 0)
        {
            buttonList = new List<Button>(new Button[buttonManager.buttons.Count]);

            foreach(var pair in buttonTagPairs)
            {
                for(int i = 0; i < buttonManager.buttons.Count; i++)
                {
                    if (buttonManager.buttons[i].tag == pair.tag)
                    {
                        buttonList[pair.index] = buttonManager.buttons[i].button;
                        break;
                    }
                }
            }
        }

       for(int i = 0; i < buttonList.Count; i++)
        {
            if (buttonList[i] != null)
            {
                originalScales[i] = buttonList[i].transform.localScale;
            }
        }

        UpdateButtonSelection();
    }

    // Update is called once per frame
    void Update()
    {
        if (!canInput) return;

        Vector2 stickInput = new Vector2(Input.GetAxis("1P_Select_X"), Input.GetAxis("1P_Select_Y"));

        Vector2 debugInput = Vector2.zero;
        debugInput.x = Input.GetAxis("Horizontal");
        debugInput.y = Input.GetAxis("Vertical");

        Debug.Log("値は : " + stickInput);
        if (buttonManager.Is_Locked)
        {
            if (Input.GetButtonDown("1P_Back"))
            {
                if (currentindex >= 0 && currentindex < buttonList.Count && buttonList[currentindex] != null)
                {
                    buttonList[currentindex].onClick.Invoke();
                }
                StartCooldown(); // クールダウン開始
                return;
            }

            if (stickInput.y > 0.05f && volume.onSeVolume)
            {
                volume.EnableBgmVolumeControl();
                ScenesAudio.ClickSe();
                StartCooldown();
                return;
            }

            else if (stickInput.y < -0.05f && !volume.onSeVolume)
            {
                volume.EnableSeVolumeControl();
                ScenesAudio.ClickSe();
                StartCooldown();
                return;
            }

            if (volume != null)
            {
                if (!volume.onSeVolume)
                    volume.AddBgmVolume(stickInput.x * 5.0f);
                else
                    volume.AddSeVolume(stickInput.x * 5.0f);
            }
        }

        if (buttonManager.Is_Locked)
            return;

        if (stickInput.y < -0.05f || debugInput.y < -0.05f)
        {
            SelectNextButton();
            StartCooldown();
            return;
        }
        else if (stickInput.y > 0.05f || debugInput.y > 0.05f)
        {
            SelectPreviousButton();
            StartCooldown();
            return;
        }

        if (Input.GetButtonDown("1P_Decision") || Input.GetKeyDown(KeyCode.Return))
        {
            if (currentindex >= 0 && currentindex < buttonList.Count && buttonList[currentindex] != null)
            {
                buttonList[currentindex].onClick.Invoke();
                Debug.Log(currentindex + "が押されました");
            }
            StartCooldown();
            return;
        }
    }

    private void SelectPreviousButton()
    {
        currentindex = (currentindex - 1) % buttonList.Count;
        UpdateButtonSelection();
    }

    private void SelectNextButton()
    {
        currentindex = (currentindex + 1) % buttonList.Count;
        UpdateButtonSelection();
    }

    private void UpdateButtonSelection()
    {
        if(buttonList == null || buttonList.Count == 0) return;

        for(int i = 0; i < buttonList.Count; i++) 
        {
            Button button = buttonList[i];
            if(button == null) continue;

            Transform buttonTransform = button.transform;

            if(i == currentindex)
            {
                button.GetComponent<Image>().DOColor(selectedColor, colorTransitionDuration);
                Vector3 targetScale = originalScales[i] * scaleMultiplier;
                buttonTransform.DOScale(targetScale, colorTransitionDuration);
            }
            else
            {
                button.GetComponent<Image>().DOColor(defaultColor, colorTransitionDuration);
                buttonTransform.DOScale(originalScales[i], colorTransitionDuration);
            }
        }
    }

    private void StartCooldown()
    {
        canInput = false;
        DOVirtual.DelayedCall(inputCooldown, () =>
        {
            canInput = true;
        });
    }

    [System.Serializable]

    public class ButtonTagPair
    {
        public int index;
        public string tag;
    }
}
