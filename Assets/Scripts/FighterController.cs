using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{


    private CharacterController _controller;

    public GameObject opponent;

    public float speed = 1.0f;
    public float gravity = 9.8f;
    public float jumpVel = 3f;

    public LayerMask layerMask;
    public GameObject hitBox;


    private Vector3 gravityVec = new Vector3();
    private Vector3 movement = new Vector3();
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

        if (Input.GetAxis("Fire1") > 0.5) {
            Attack();
        }
    }

    void Attack() {
        Collider[] hitColliders = Physics.OverlapBox(hitBox.transform.position, hitBox.transform.localScale / 2, Quaternion.identity, layerMask);

        for (int i = 0; i < hitColliders.Length; i++) {
            Debug.Log("Hit : " + hitColliders[i].name + i);

            if (hitColliders[i].GetComponent<HealthScript>() != null) {
                hitColliders[i].GetComponent<HealthScript>().TakeDamage(1);
            }

        }
    }

    void Block() {
        
    }

    void MoveLeft() {
        movement += new Vector3(1f, 0, 0);
    }

    void MoveRight() {
        movement -= new Vector3(1f, 0, 0);
    }

    void Jump() {
        if (jumped) {
            return;
        }
        jumped = true;
        gravityVec = new Vector3(0, jumpVel, 0);
    }


    void firstMovementControls() {
        ReadInputs();
        movement = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (!_controller.isGrounded) {
            gravityVec += new Vector3(0, -gravity, 0) * Time.deltaTime;
        } else {
            jumped = false;
            gravityVec = new Vector3(0, 0, 0);
        }

        _controller.Move((movement + gravityVec) * Time.deltaTime * speed);



        // Makes sure the player is facing the opponent at all times
        if (transform.position.x <= opponent.transform.position.x) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }

    // Update is called once per frame
    void Update()
    {
        firstMovementControls();
    }
}
