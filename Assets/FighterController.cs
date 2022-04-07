using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterController : MonoBehaviour
{

    private CharacterController _controller;
    public float speed = 1.0f;

    public float gravity = 9.8f;

    // Start is called before the first frame update
    void Start()
    {
        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, 0);

        if (!_controller.isGrounded) {
            move += new Vector3(0, -gravity, 0) * Time.deltaTime;
        } else {
            //move.y = -gravity;
        }

        _controller.Move(move * Time.deltaTime * speed);

    }
}
