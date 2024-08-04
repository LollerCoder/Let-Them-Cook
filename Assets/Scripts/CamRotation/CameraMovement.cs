using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    //https://www.youtube.com/watch?v=iuygipAigew

    private bool bLeft,bRight,bUp,bDown;
    void Start()
    {
        bLeft = false;
        bRight = false;
        bUp = false;
        bDown = false;
    }

    // Update is called once per frame
    void Update()
    { 
        checkKeyDown();

        if (bLeft) transform.Rotate(0,100 * Time.deltaTime,0);
        else if (bRight) transform.Rotate(0,-100 * Time.deltaTime,0);

        //Up and Down will need to clamp
        else if (bUp) 
        {
            transform.Rotate(100 * Time.deltaTime,0,0);   
        }
        
        else if (bDown) transform.Rotate(-100 * Time.deltaTime,0,0);

        checkKeyUp();
    }

    void checkKeyDown()
    {
        if(Input.GetKeyDown(KeyCode.LeftArrow) && !bLeft) bLeft = true;
        else if( Input.GetKeyDown(KeyCode.RightArrow) && !bRight) bRight = true;
        else if (Input.GetKeyDown(KeyCode.UpArrow) && !bUp) bUp = true;
        else if (Input.GetKeyDown(KeyCode.DownArrow) && !bDown) bDown = true;
    }
    void checkKeyUp()
    {

        if(Input.GetKeyUp(KeyCode.LeftArrow) && bLeft) bLeft = false;
        else if( Input.GetKeyUp(KeyCode.RightArrow) && bRight) bRight = false;
        else if (Input.GetKeyUp(KeyCode.UpArrow) && bUp) bUp = false;
        else if (Input.GetKeyUp(KeyCode.DownArrow) && bDown) bDown = false;
    }
}
