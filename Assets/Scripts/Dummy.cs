using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public GameObject opponent;


    public float gravity = 9.8f;

    private CharacterController _controller;
    private Vector3 gravityVec = new Vector3();
    private Vector3 movement = new Vector3();

    bool opponentToLeft = true;

    private Vector3 impactForce = new Vector3();

    private float startingPositionX = 0.0f;

    // Start is called before the first frame update
    void Start()
    {

        _controller = GetComponent<CharacterController>();
        startingPositionX = transform.position.x;
    }

    public void KnockBack(float impact, Vector3 dir) {
        if (!opponentToLeft) {
            dir.x *= -1;
        }

        //impactForce += impact * dir;
    }

    // Update is called once per frame
    void Update()
    {
        // movement = ;
        //movement = new Vector3();

        if (!_controller.isGrounded) {
            gravityVec += new Vector3(0, -gravity, 0) * Time.deltaTime;
        } else {
            gravityVec = new Vector3(0, 4, 0);
        }

        movement.y = gravityVec.y;


        if (transform.position.x < startingPositionX - 1f) {
            //movement += new Vector3(0.3f, 0, 0);
        } else if (transform.position.x > startingPositionX + 1f) {
            //movement += new Vector3(-0.3f, 0, 0);
        }

        _controller.Move((movement + impactForce) * Time.deltaTime);

        if (transform.position.x <= opponent.transform.position.x) {
            opponentToLeft = false;
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
            opponentToLeft = true;
        }

        impactForce = new Vector3();
    }
}
