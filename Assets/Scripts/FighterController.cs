using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using System.Runtime;

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
    private GameManager gameManager;
    private UIManager uiManager;
    private FighterManager fighterManager;


    public Vector3 p1StartingPosition = new Vector3(-3 ,0 ,0);
    public Vector3 p2StartingPosition = new Vector3(3, 0, 0);

    public ControlSystem controlSystem = ControlSystem.UpDown;

    public CharacterState state = CharacterState.Idle;

    private InputController _inputController;
    private MovementController _movementController;
    private AttackController _attackController;

    public bool binaryMovement = false;
    public bool overrideSplitControls;
    private float damageTimer;
    public float damageCooldown = 0.5f;
    private float hitStunTimer;
    public float hitStunCooldown = 0.5f;
    private float idleReturnTimer;
    public float idleReturnCooldown = 0.15f;
    public AudioSource audioSource;
    public AudioClip[] punchAudioClips;

    public float maxHealth = 100f;
    public float health;

    public float blockDurationS = .5f;
    public float blockEndLagS = .25f;
    public float blockTimer = 99;

    public void ResetGame() {

        float upButton = _inputController.upButton;

        if (upButton >= 0.5f) {
            if (this.name == "Character") {
                transform.position = p1StartingPosition;
            } else {
                transform.position = p2StartingPosition;
            }
            health = maxHealth;
            uiManager.UpdateHealthSlider(gameObject, health, maxHealth);

        }


    }

    public float attackEndLagS = .5f;
    
    public float attackDurationS = .5f;
    public float attackTimer = 99;


    void UpDownSplitInputs(string up, string down) {

        bool inputRead = false;
        float analogX;
        float analogY;
        float attackButton;
        float kickButton;
        float blockTrigger;

        if (up == "Left") {
            analogX = _inputController.rightStick.x;
            analogY = _inputController.rightStick.y;
            kickButton = _inputController.rightBumper;
            attackButton = _inputController.leftBumper;
            blockTrigger = _inputController.leftTrigger;
        } else {
            analogX = _inputController.leftStick.x;
            analogY = _inputController.leftStick.y;
            kickButton = _inputController.leftBumper;
            attackButton = _inputController.rightBumper;
            blockTrigger = _inputController.rightTrigger;
        }

        attackTimer += Time.deltaTime;
        if (state == CharacterState.Idle || state == CharacterState.Attack || state == CharacterState.Move) {
            if (state != CharacterState.Attack && attackTimer > attackDurationS + attackEndLagS) { // Can attack
                if (kickButton == 1 && !(attackButton == 1)) { // Low attack
                    _attackController.StartLowAttack();
                    inputRead = true;
                    state = CharacterState.Attack;
                    attackTimer = 0;
                } else if (attackButton == 1 && !(kickButton == 1)) { // High attack
                    _attackController.StartHighAttack();
                    _animator.SetTrigger("AttackAnim");
                    state = CharacterState.Attack;
                    inputRead = true;
                    attackTimer = 0;
                }
            } else if (state == CharacterState.Attack) {
                if (attackTimer > attackDurationS) {
                    state = CharacterState.Idle;
                    // TODO Maybe knock the character back?
                    Debug.Log("Not Blocking! Has end lag!");
                }
            }
        }

        if (!_attackController.attackInProgress && (state == CharacterState.Idle || state == CharacterState.Move)) {
            if (binaryMovement) {
                if (analogX >= 0.5) {
                    _movementController.MoveRight();
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }
                if (analogX <= -0.5) {
                    _movementController.MoveLeft();
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }

            } else {
                if (analogX >= 0.01) {
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }
                if (analogX <= -0.01) {
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }

                _movementController.MoveJoystick(analogX);
                
                if (analogY > 0.5f) {
                    _movementController.Jump(analogY*2, binaryMovement);
                }
            }

        }

        blockTimer += Time.deltaTime;
        if (state == CharacterState.Idle || state == CharacterState.Block || state == CharacterState.Move) {
            if (blockTrigger >= 0.5 && state != CharacterState.Block && blockTimer > blockDurationS + blockEndLagS) {
                Debug.Log("Block!");
                inputRead = true;
                blockTimer = 0;
                state = CharacterState.Block;
                _animator.SetTrigger("BlockAnim");
            } else if (state == CharacterState.Block) {
                if (blockTimer >= blockDurationS) {
                    state = CharacterState.Idle;
                    // TODO Maybe knock the character back?
                    Debug.Log("Not Blocking! Has end lag!");
                }
            }
        }

        // attackTimer += T
        // if (state == CharacterState.Idle || state == CharacterState.Attack) {
        //     if (kickButton == 1 & !attackButton == 1) {
        //         _attackController.StartLowAttack();
        //         inputRead = true;
        //         state = CharacterState.Attack;
        //     }

        //     if (attackButton == 1) {
        //         _attackController.StartHighAttack();
        //         _animator.SetTrigger("AttackAnim");
        //         state = CharacterState.Attack;
        //         inputRead = true;
        //       }
        // }

        if (!inputRead) {
            _animator.SetTrigger("IdleAnim");
            if (state != CharacterState.Idle && idleReturnTimer > idleReturnCooldown) {
                state = CharacterState.Idle;
            }
        } else {
            if (state != CharacterState.Idle) {
                idleReturnTimer = 0;
            }
        }
    }

    void LeftRightSplitInputs(string left, string right) {
        bool inputRead = false;

        float rightAnalogX;
        float leftAnalogX;
        float analogY;
        float attackButton;
        float kickButton;
        float blockTrigger;

        if (left == "Left") {
            leftAnalogX = _inputController.leftStick.x;
            rightAnalogX = _inputController.rightStick.x;

            analogY = (_inputController.leftStick.y + _inputController.rightStick.y) / 2;

            attackButton = _inputController.leftBumper;
            kickButton = _inputController.rightBumper;
            blockTrigger = _inputController.leftTrigger;
        } else {
            leftAnalogX = _inputController.rightStick.x;
            rightAnalogX = _inputController.leftStick.x;

            analogY = (_inputController.leftStick.y + _inputController.rightStick.y) / 2;

            attackButton = _inputController.rightBumper;
            kickButton = _inputController.leftBumper;
            blockTrigger = _inputController.rightTrigger;
        }

        // if (state == CharacterState.Idle || state == CharacterState.Attack) {
        //     if (kickButton == 1) {
        //         _attackController.StartLowAttack();
        //         inputRead = true;
        //         state = CharacterState.Attack;
        //     }

        //     if (attackButton == 1) {
        //         _attackController.StartHighAttack();
        //         _animator.SetTrigger("AttackAnim");
        //         state = CharacterState.Attack;
        //         inputRead = true;
        //     }
        // }

        attackTimer += Time.deltaTime;
        if (state == CharacterState.Idle || state == CharacterState.Attack || state == CharacterState.Move) {
            if (state != CharacterState.Attack && attackTimer > attackDurationS + attackEndLagS) { // Can attack
                if (kickButton == 1 && !(attackButton == 1)) { // Low attack
                    _attackController.StartLowAttack();
                    inputRead = true;
                    state = CharacterState.Attack;
                    attackTimer = 0;
                } else if (attackButton == 1 && !(kickButton == 1)) { // High attack
                    _attackController.StartHighAttack();
                    _animator.SetTrigger("AttackAnim");
                    state = CharacterState.Attack;
                    inputRead = true;
                    attackTimer = 0;
                }
            } else if (state == CharacterState.Attack) {
                if (attackTimer > attackDurationS) {
                    state = CharacterState.Idle;
                }
            }
        }

        blockTimer += Time.deltaTime;
        if (state == CharacterState.Idle || state == CharacterState.Block || state == CharacterState.Move) {
            if (blockTrigger >= 0.5 && state != CharacterState.Block && blockTimer > blockDurationS + blockEndLagS) {
                Debug.Log("Block!");
                inputRead = true;
                blockTimer = 0;
                state = CharacterState.Block;
                _animator.SetTrigger("BlockAnim");
            } else if (state == CharacterState.Block) { // isBlocking
                if (blockTimer >= blockDurationS) {
                    state = CharacterState.Idle;
                    // TODO Maybe knock the character back?
                    Debug.Log("Not Blocking! Has end lag!");
                }
            }
        }

        if (!_attackController.attackInProgress && (state == CharacterState.Idle || state == CharacterState.Move)) {
            float movementInput = (rightAnalogX + leftAnalogX) / 2f;
           

            if(binaryMovement) {
                if (movementInput >= 0.5) {
                    _movementController.MoveRight();
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }
                if (movementInput <= -0.5) {
                    _movementController.MoveLeft();
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }
                
            } else {
                if (movementInput >= 0.01) {
                    _animator.SetTrigger("WalkForwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }
                if (movementInput <= -0.01) {
                    _animator.SetTrigger("WalkBackwardAnim");
                    inputRead = true;
                    state = CharacterState.Move;
                }
                _movementController.MoveJoystick(movementInput);
            }

            if (analogY*2 > 0.5f) {
                _movementController.Jump(analogY*2, binaryMovement);
            }

        }





        if (!inputRead) {
            _animator.SetTrigger("IdleAnim");
            if (state != CharacterState.Idle && idleReturnTimer > idleReturnCooldown) {
                state = CharacterState.Idle;
            }
        } else {
            if (state != CharacterState.Idle) {
                idleReturnTimer = 0;
            }
        }
    }

    public void TakeDamage(float damage) {
        if (damageTimer > damageCooldown) {
            damageTimer = 0f;
            audioSource.PlayOneShot(punchAudioClips[UnityEngine.Random.Range(0, punchAudioClips.Length)], 1.0F);
            health -= state == CharacterState.Block && damage == AttackController.HIGH_ATTACK_DAMAGE ? 1f : damage;
            uiManager.UpdateHealthSlider(gameObject, health, maxHealth);
            // Debug.Log("Fighter Damage Taken: " + damage);
            hitStunTimer = 0f;
            state = CharacterState.HitStun;

            if (health <= 0)
            {
                gameManager.EndGame();
            }
        }
    }

    // Start is called before the first frame update
    void Start() {
        _movementController = GetComponent<MovementController>();
        _attackController = GetComponent<AttackController>();
        _inputController = GetComponent<InputController>();
        _animator = _animModel.GetComponent<Animator>();
        gameManager = FindObjectOfType<GameManager>();
        uiManager = FindObjectOfType<UIManager>();
        fighterManager = FindObjectOfType<FighterManager>();
        audioSource = GetComponent<AudioSource>();
        health = maxHealth;
        damageTimer = 0;
        hitStunTimer = 0;
        idleReturnTimer = 0;

        if (this.name != "Character")
        {
            fighterManager.p2FC = this;
        }

        RandomizeControlSystem();
    }

    void RandomizeControlSystem()
    {
        Array values = Enum.GetValues(typeof(ControlSystem));
        System.Random random = new System.Random();
        ControlSystem randomControlSystem = (ControlSystem)values.GetValue(random.Next(values.Length));

        controlSystem = randomControlSystem;
    }

    // Update is called once per frame
    void Update() {
        ResetGame();
        damageTimer += Time.deltaTime;

        if (state == CharacterState.HitStun && hitStunTimer > hitStunCooldown) {
            state = CharacterState.Idle;
        } else if (state == CharacterState.HitStun && hitStunTimer <= hitStunCooldown) {
            hitStunTimer += Time.deltaTime;
        }

        if (state != CharacterState.Idle) {
            idleReturnTimer += Time.deltaTime;
        }
        switch (controlSystem) {
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
        if (overrideSplitControls) {
            /*
            if (Input.GetAxis("Horizontal") > 0.5f) {
                _movementController.MoveRight();
            }
            if (Input.GetAxis("Horizontal") < -0.5f) {
                _movementController.MoveLeft();
            }

            if (state == CharacterState.Idle || state == CharacterState.Attack) {
                if (Input.GetKeyDown(KeyCode.U)) {
                    _attackController.StartHighAttack();
                    _animator.SetTrigger("AttackAnim");
                    inputRead = true;
                    state = CharacterState.Attack;
                }
                if (Input.GetKeyDown(KeyCode.J)) {
                    _attackController.StartLowAttack();
                    inputRead = true;
                    state = CharacterState.Attack;
                }
            }

            if (state == CharacterState.Idle || state == CharacterState.Block) {
                if (Input.GetKey(KeyCode.Space)) {
                    Debug.Log("Block!");
                    inputRead = true;
                    state = CharacterState.Block;
                }
            }

            if (!inputRead) {
                _animator.SetTrigger("IdleAnim");
                if (state != CharacterState.Idle && idleReturnTimer > idleReturnCooldown) {
                    state = CharacterState.Idle;
                }
            } else {
                if (state != CharacterState.Idle) {
                    idleReturnTimer = 0;
                }
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

            /*
        Debug.Log("Right Bumper: " + Input.GetAxis("RightBumper"));
        Debug.Log("Left Bumper: " + Input.GetAxis("LeftBumper"));

        Debug.Log("Left Analog X: " + Input.GetAxis("LeftAnalogX"));
        Debug.Log("Left Analog Y: " + Input.GetAxis("LeftAnalogY"));


        Debug.Log("Right Analog X: " + Input.GetAxis("RightAnalogX"));
        Debug.Log("Right Analog Y: " + Input.GetAxis("RightAnalogY"));
            */
        }
    }

}
