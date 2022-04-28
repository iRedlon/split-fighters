using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementController : MonoBehaviour
{


    private CharacterController _controller;

    public GameObject opponent;

    public float speed = 1.0f;
    public float gravity = 9.8f;
    public float jumpVel = 3f;
    public float damping = 0.9f;

    public float knockbackScale = 10f;


    private Vector3 gravityVec = new Vector3();
    private Vector3 movement = new Vector3();
    private Vector3 knockback = new Vector3();
    bool jumped = false;



    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();

    }

    void ReadInputs() {
        if (Input.GetAxis("Horizontal") > 0.5f) {
            MoveRight();
        }

        if (Input.GetAxis("Horizontal") < -0.5f) {
            MoveLeft();
        }

        if (Input.GetAxis("Vertical") > 0.5f) {
            Jump();
        }

    }

    public void MoveJoystick(float strength) {
        movement = new Vector3(strength, 0, 0);
    }

    public void MoveLeft() {
        movement = new Vector3(-1f, 0, 0);
    }

    public void MoveRight() {
        movement = new Vector3(1f, 0, 0);
    }

    public void Knockback(float damage, int direction) {
        knockback = new Vector3(direction * knockbackScale * damage, 0, 0);
    }

    public void Jump() {
        if (jumped) {
            return;
        }
        jumped = true;
        gravityVec = new Vector3(0, jumpVel, 0);
    }

    void FaceOpponent() {
        if (opponent == null) {
            Debug.Log("Finding Opponent");
            //GameObject.Find("");

            if (this.name == "Character") {
                // P1
                //opponent = GameObject.Find("Character");
                opponent = GameObject.Find("CharacterFighter(Clone)");
            } else {
                // P2
                opponent = GameObject.Find("Character");
            }

            return;
        }


        if (opponent != null) {
            // Makes sure the player is facing the opponent at all times
            if (transform.position.x <= opponent.transform.position.x) {
                transform.localScale = new Vector3(1, 1, 1);
            } else {
                transform.localScale = new Vector3(-1, 1, 1);
            }
        }
        // pass
    }

    void firstMovementControls() {
        // ReadInputs();
        //movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (!_controller.isGrounded) {
            gravityVec += new Vector3(0, -gravity, 0) * Time.deltaTime;
        } else {
            jumped = false;
            gravityVec = new Vector3(0, 0, 0);
        }

        knockback = knockback * damping;
        Vector3 movementVector = (movement * speed + gravityVec + knockback) * Time.deltaTime;
        _controller.Move(movementVector);
        movement = new Vector3();


    }

    // Update is called once per frame
    void Update()
    {


        FaceOpponent();

        firstMovementControls();
    }
}
