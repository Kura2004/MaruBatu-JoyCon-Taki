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
        if(Input.GetButtonDown("1P_Decision"))
        {
            Debug.Log("1P_Aボタン");
        }
        if(Input.GetButtonDown("1P_Back"))
        {
            Debug.Log("1P_Bボタン");
        }
        if(Input.GetButtonDown("2P_Decision"))
        {
            Debug.Log("2P_Aボタン");
        }
        if(Input.GetButtonDown("2P_Back"))
        {
            Debug.Log("2P_Bボタン");
        }
        if(Input.GetButtonDown("1P_L1"))
        {
            Debug.Log("1P_L1");
        }
        if(Input.GetButtonDown("1P_R1"))
        {
            Debug.Log("1P_R1");
        }
        if(Input.GetAxis("1P_Select_X") != 0)
        {
            Debug.Log("1P左右");
        }
        if(Input.GetAxis("1P_Select_Y") != 0)
        {
            Debug.Log("1P上下");
        }
        if(Input.GetAxis("2P_Select_X") != 0)
        {
            Debug.Log("2P左右");
        }
        if(Input.GetAxis("2P_Select_Y") != 0)
        {
            Debug.Log("2P上下");
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
