using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

[System.Serializable]

public class ButtonData
{
    public Button button;

    public string tag;
}
public class ButtonManager : MonoBehaviour
{
    public List<ButtonData> buttons = new List<ButtonData>();

    public bool Is_Locked = false;
    [SerializeField] string specialTag;
    // Start is called before the first frame update
    void Start()
    {
        foreach(ButtonData buttonData in buttons)
        {
            if(buttonData.button != null)
            {
                buttonData.button.onClick.AddListener(() => OnButtonClicked(buttonData));
            }
        }
    }

    private void OnButtonClicked(ButtonData buttonData)
    {
        if(Is_Locked)
        {
            Debug.Log("Buttons are locked, no action performed.");
            return;
        }
    }

    public void LockButtons()
    {
        Is_Locked = true;
        foreach(ButtonData buttonData in buttons) 
        {
            buttonData.button.interactable = buttonData.tag == specialTag;        
        }
    }

    public void UnlockButtons() 
    {
        Is_Locked = false;
        foreach(ButtonData buttonData in buttons)
        {
            buttonData.button.interactable = true;
        }
    }

    public void ToggleLockButtons()
    {
        if(Is_Locked)
        {
            UnlockButtons();
        }
        else
        {
            LockButtons();
        }
    }
}
