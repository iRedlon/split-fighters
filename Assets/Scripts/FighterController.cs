using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum CharacterState { Block, Attack, Move, HitStun };
public enum ControlSystem { UpDown, LeftRight, DownUp, RightLeft }

public class FighterController : MonoBehaviour
{


    public ControlSystem controlSystem = ControlSystem.UpDown;

    private MovementController _movementController;
    private AttackController _attackController;

    public bool binaryMovement = false;



    void UpDownSplitInputs(string up, string down) {

        float analogX = Input.GetAxis(down+"AnalogX");

        if (!_attackController.attackInProgress) {

            if (binaryMovement) {
                if (analogX >= 0.5) {
                    _movementController.MoveRight();
                }
                if (analogX <= -0.5) {
                    _movementController.MoveLeft();
                }
            } else {
                _movementController.MoveJoystick(analogX);
            }

        }

        if (Input.GetButtonDown(down+"Bumper")) {
            _attackController.StartLowAttack();
        }

        if (Input.GetButtonDown(up+"Bumper")) {
            _attackController.StartHighAttack();
        }
    }

    void LeftRightSplitInputs(string left, string right) {

        float rightAnalogX = Input.GetAxis(left + "AnalogX");

        float leftAnalogX = Input.GetAxis(right + "AnalogX");

        if (!_attackController.attackInProgress) {
            float movementInput = (rightAnalogX + leftAnalogX) / 2f;

            if(binaryMovement) {
                if (movementInput >= 0.5) {
                    _movementController.MoveRight();
                }
                if (movementInput <= -0.5) {
                    _movementController.MoveLeft();
                }
            } else {

                _movementController.MoveJoystick(movementInput);
            }

        }

        if (Input.GetButtonDown(right + "Bumper")) {
            _attackController.StartLowAttack();
        }

        if (Input.GetButtonDown(left + "Bumper")) {
            _attackController.StartHighAttack();
        }
    }

    // Start is called before the first frame update
    void Start() {
        _movementController = GetComponent<MovementController>();
        _attackController = GetComponent<AttackController>();
    }

    // Update is called once per frame
    void Update() {

        switch(controlSystem) {
            case ControlSystem.UpDown:
                UpDownSplitInputs("Left", "Right");
                break;
            case ControlSystem.DownUp:
                UpDownSplitInputs("Right", "Left");
                break;
            case ControlSystem.LeftRight:
                LeftRightSplitInputs("Left", "Right");
                break;
            case ControlSystem.RightLeft:
                LeftRightSplitInputs("Right", "Left");
                break;
        }

        Debug.Log("Right Bumper: " + Input.GetAxis("RightBumper"));
        Debug.Log("Left Bumper: " + Input.GetAxis("LeftBumper"));

        Debug.Log("Left Analog X: " + Input.GetAxis("LeftAnalogX"));
        Debug.Log("Left Analog Y: " + Input.GetAxis("LeftAnalogY"));


        Debug.Log("Right Analog X: " + Input.GetAxis("RightAnalogX"));
        Debug.Log("Right Analog Y: " + Input.GetAxis("RightAnalogY"));
    }

}