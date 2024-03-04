using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{
    public string moveAxisName = "Horizontal";
    public string jumpButtonName = "Jump";

    public float move { get; private set; }
    public bool jump { get; private set; }
    public bool jumpRelease { get; private set; }

    private void Update()
    {


        this.move = Input.GetAxisRaw(moveAxisName);
        //this.move = Mathf.roun this.move
        this.jump = Input.GetButtonDown(jumpButtonName);
        if (jump == true)
            Debug.Log("점프 키 눌림..");
        this.jumpRelease = Input.GetButtonUp(jumpButtonName);


    }


}
