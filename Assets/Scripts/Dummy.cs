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


    // Start is called before the first frame update
    void Start()
    {

        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // movement = ;

        if (!_controller.isGrounded) {
            gravityVec += new Vector3(0, -gravity, 0) * Time.deltaTime;
        } else {
            gravityVec = new Vector3(0, 0, 0);
        }

        _controller.Move((movement + gravityVec) * Time.deltaTime);

        if (transform.position.x <= opponent.transform.position.x) {
            transform.localScale = new Vector3(1, 1, 1);
        } else {
            transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
