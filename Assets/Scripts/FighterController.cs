using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// enum CharacterState { Block, Attack, Move, HitStun };
public enum ControlSystem { UpDown, LeftRight, DownUp, RightLeft }

public class FighterController : MonoBehaviour
{

    public GameObject _animModel;
    private Animator _animator;

    public ControlSystem controlSystem = ControlSystem.UpDown;

    private MovementController _movementController;
    private AttackController _attackController;

    public bool binaryMovement = false;
    public bool overrideSplitControls;

    void UpDownSplitInputs(string up, string down) {

        bool inputRead = false;

        float analogX = Input.GetAxis(down+"AnalogX");

        if (!_attackController.attackInProgress) {

            if (binaryMovement) {
                if (analogX >= 0.5) {
                    _movementController.MoveRight();
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                }
                if (analogX <= -0.5) {
                    _movementController.MoveLeft();
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                }
            } else {
                if (analogX >= 0.01) {
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                }
                if (analogX <= -0.01) {
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                }

                _movementController.MoveJoystick(analogX);
            }

        }

        if (Input.GetButtonDown(down+"Bumper")) {
            _attackController.StartLowAttack();
            inputRead = true;
        }

        if (Input.GetButtonDown(up+"Bumper")) {
            _attackController.StartHighAttack();
            _animator.SetTrigger("AttackAnim");
            inputRead = true;
        }

        if (!inputRead) {
            _animator.SetTrigger("IdleAnim");
        }
    }

    void LeftRightSplitInputs(string left, string right) {
        bool inputRead = false;

        float rightAnalogX = Input.GetAxis(left + "AnalogX");

        float leftAnalogX = Input.GetAxis(right + "AnalogX");

        if (!_attackController.attackInProgress) {
            float movementInput = (rightAnalogX + leftAnalogX) / 2f;

            if(binaryMovement) {
                if (movementInput >= 0.5) {
                    _movementController.MoveRight();
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                }
                if (movementInput <= -0.5) {
                    _movementController.MoveLeft();
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                }
            } else {
                if (movementInput >= 0.01) {
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                }
                if (movementInput <= -0.01) {
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                }
                _movementController.MoveJoystick(movementInput);
            }

        }

        if (Input.GetButtonDown(right + "Bumper")) {
            _attackController.StartLowAttack();
            inputRead = true;
        }

        if (Input.GetButtonDown(left + "Bumper")) {
            _attackController.StartHighAttack();
            _animator.SetTrigger("AttackAnim");
            inputRead = true;
        }

        if (!inputRead) {

            _animator.SetTrigger("IdleAnim");
        }
    }

    // Start is called before the first frame update
    void Start() {
        _movementController = GetComponent<MovementController>();
        _attackController = GetComponent<AttackController>();
        _animator = _animModel.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update() {
        if (overrideSplitControls) {
            if (Input.GetAxis("Horizontal") > 0.5f) {
                _movementController.MoveRight();
                _animator.SetTrigger("WalkForwardAnim");
            }
            if (Input.GetAxis("Horizontal") < -0.5f) {
                _movementController.MoveLeft();
                _animator.SetTrigger("WalkBackwardAnim");
            }
            if (Input.GetAxis("Vertical") > 0.5f) {
                _movementController.Jump();
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _attackController.StartHighAttack();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                _attackController.StartLowAttack();
            }
        } else {
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
}
