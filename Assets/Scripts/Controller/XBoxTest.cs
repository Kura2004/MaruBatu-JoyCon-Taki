using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBoxTest : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetButtonDown("A_Button"))
        {
            Debug.Log("A�{�^��");
        }
        if(Input.GetButtonDown("B_Button"))
        {
            Debug.Log("B�{�^��");
        }
        if(Input.GetButtonDown("Y_Button"))
        {
            Debug.Log("Y�{�^��");
        }
        if(Input.GetButtonDown("X_Button"))
        {
            Debug.Log("X�{�^��");
        }
        if(Input.GetButtonDown("L1_Button"))
        {
            Debug.Log("L1");
        }
        if(Input.GetButtonDown("R1_Button"))
        {
            Debug.Log("R1");
        }
        if(Input.GetAxis("L_Stick1") != 0)
        {
            Debug.Log("���X�e�B�b�N���E");
        }
        if(Input.GetAxis("L_Stick2") != 0)
        {
            Debug.Log("���X�e�B�b�N�㉺");
        }
        if(Input.GetAxis("R_Stick1") != 0)
        {
            Debug.Log("�E�X�e�B�b�N���E");
        }
        if(Input.GetAxis("R_Stick2") != 0)
        {
            Debug.Log("�E�X�e�B�b�N�㉺");
        }
    }
}
