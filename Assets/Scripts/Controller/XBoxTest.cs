using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class XBoxTest : MonoBehaviour
{
    private Vector2 input;
    // Update is called once per frame
    void Update()
    {
        input = new Vector2(Input.GetAxis("1P_Select_X"), Input.GetAxis("1P_Select_Y"));

        Debug.Log("�l�� : " + input);

        if(Input.GetButtonDown("1P_Decision"))
        {
            Debug.Log("1P_A�{�^��");
        }
        if(Input.GetButtonDown("1P_Back"))
        {
            Debug.Log("1P_B�{�^��");
        }
        if(Input.GetButtonDown("2P_Decision"))
        {
            Debug.Log("2P_A�{�^��");
        }
        if(Input.GetButtonDown("2P_Back"))
        {
            Debug.Log("2P_B�{�^��");
        }
        if(Input.GetButtonDown("1P_L1"))
        {
            Debug.Log("1P_L1");
        }
        if(Input.GetButtonDown("1P_R1"))
        {
            Debug.Log("1P_R1");
        }
        if(Mathf.Abs(Input.GetAxis("1P_Select_X")) > 0.01f)
        {
            Debug.Log("1P���E");
        }
        if(Mathf.Abs(Input.GetAxis("1P_Select_Y")) > 0.01f)
        {
            Debug.Log("1P�㉺");
        }
        if(Mathf.Abs(Input.GetAxis("2P_Select_X")) > 0.01f)
        {
            Debug.Log("2P���E");
        }
        if(Mathf.Abs(Input.GetAxis("2P_Select_Y")) > 0.01f)
        {
            Debug.Log("2P�㉺");
        }
        if(Input.GetButtonDown("2P_L1"))
        {
            Debug.Log("2P_L1");
        }
        if(Input.GetButtonDown("2P_R1"))
        {
            Debug.Log("2P_R1");
        }
    }
}
