using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CharacterState { Block, Attack, Move, HitStun, Idle };
public enum ControlSystem { UpDown, LeftRight, DownUp, RightLeft }

/*
State Explanations: 
Block - Hold block and take chip damage for high attack and full damage for low attack, can't move or attack.
Attack - High attack and low attack, can't move while attacking. Think about attack timing, where both players 
attack at the same time. If both high or low, cancel, if not, then because of different start/end lag, one move is prioritized and takes effect first.
Move - Move around, jump and run.
HitStun - Player is hit by low or high attack, and they can't do anything for half a second. Any hit also causes knock back.
*/
public class FighterController : MonoBehaviour
{

    public GameObject _animModel;
    private Animator _animator;

    public ControlSystem controlSystem = ControlSystem.UpDown;

    public CharacterState state = CharacterState.Idle;

    private MovementController _movementController;
    private AttackController _attackController;

    public bool binaryMovement = false;
    public bool overrideSplitControls;
    private float damageTimer;
    public float damageCooldown = 0.5f;
    public AudioSource audioSource;
    public AudioClip[] punchAudioClips;

    public float health;

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

    public void TakeDamage(float damage) {
        if (damageTimer > damageCooldown) {
            damageTimer = 0f;
            audioSource.PlayOneShot(punchAudioClips[Random.Range(0, punchAudioClips.Length)], 1.0F);
            health -= damage;
            // Debug.Log("Fighter Damage Taken: " + damage);
        }
    }

    // Start is called before the first frame update
    void Start() {
        _movementController = GetComponent<MovementController>();
        _attackController = GetComponent<AttackController>();
        _animator = _animModel.GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        health = 100f;
        damageTimer = 0;
    }

    // Update is called once per frame
    void Update() {
        damageTimer += Time.deltaTime;

        if (overrideSplitControls) {
            bool inputRead = false;
            if (Input.GetAxis("Horizontal") > 0.5f) {
                _movementController.MoveRight();
                _animator.SetTrigger("WalkForwardAnim");
                inputRead = true;
            }
            if (Input.GetAxis("Horizontal") < -0.5f) {
                _movementController.MoveLeft();
                _animator.SetTrigger("WalkBackwardAnim");
                inputRead = true;
            }
            if (Input.GetAxis("Vertical") > 0.5f) {
                _movementController.Jump();
                inputRead = true;
            }

            if (Input.GetKeyDown(KeyCode.UpArrow)) {
                _attackController.StartHighAttack();
                _animator.SetTrigger("AttackAnim");
                inputRead = true;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow)) {
                _attackController.StartLowAttack();
                inputRead = true;
            }
                
            if (!inputRead) {
                _animator.SetTrigger("IdleAnim");
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
