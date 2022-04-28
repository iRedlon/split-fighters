using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputController : MonoBehaviour
{
    public Vector2 leftStick;
    public float leftBumper;
    public float leftTrigger;
    public Vector2 dPad;


    public Vector2 rightStick;
    public float rightBumper;
    public float rightTrigger;
    public float upButton;
    public float downButton;
    public float leftButton;
    public float rightButton;


    public void LeftStick(InputAction.CallbackContext context) {
        // Debug.Log(context.ReadValue<Vector2>());
        leftStick = context.ReadValue<Vector2>();
    }

    public void RightStick(InputAction.CallbackContext context) {
        // Debug.Log(context.ReadValue<Vector2>());
        rightStick = context.ReadValue<Vector2>();
    }

    public void RightBumper(InputAction.CallbackContext context) {
        // Debug.Log("Right bumper");
        rightBumper = context.ReadValue<float>();
    }

    public void LeftBumper(InputAction.CallbackContext context) {
        // Debug.Log("Left bumper");
        leftBumper = context.ReadValue<float>();
    }

    public void RightTrigger(InputAction.CallbackContext context) {
        // Debug.Log(context.ReadValue<float>());
        rightTrigger = context.ReadValue<float>();
    }

    public void LeftTrigger(InputAction.CallbackContext context) {
        // Debug.Log(context.ReadValue<float>());
        leftTrigger = context.ReadValue<float>();
    }

    public void DPad(InputAction.CallbackContext context) {
        // Debug.Log(context.ReadValue<Vector2>());
        dPad = context.ReadValue<Vector2>();
    }

    public void UpButton(InputAction.CallbackContext context) {
        // Debug.Log("Up Button");
        upButton = context.ReadValue<float>();
    }

    public void RightButton(InputAction.CallbackContext context) {
        // Debug.Log("Right Button");
        rightButton = context.ReadValue<float>();
    }
    public void DownButton(InputAction.CallbackContext context) {
        // Debug.Log("Down Button");
        downButton = context.ReadValue<float>();
    }
    public void LeftButton(InputAction.CallbackContext context) {
        // Debug.Log("Left Button");
        leftButton = context.ReadValue<float>();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
