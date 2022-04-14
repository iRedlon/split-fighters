using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dummy : MonoBehaviour
{
    public GameObject opponent;
    public GameObject hitBox;
    public LayerMask layerMask;


    public float gravity = 9.8f;
    public float movementDamping = 0.99f;

    public bool enableMovement = true;
    public bool enableAttacks = true;

    private CharacterController _controller;
    private Vector3 gravityVec = new Vector3();
    private Vector3 movement = new Vector3();

    private float attackTimer = 0f;

    public float attackReload = 2f;


    // Start is called before the first frame update
    void Start()
    {

        _controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        movement *= movementDamping;

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

        attackTimer += Time.deltaTime;
        if (attackTimer > attackReload) {
            attackTimer = 0f;
            Attack();
        }
    }

    public void TakeHit(float knockback) {
        if (enableMovement) {
            movement = new Vector3(knockback, 0, 0);
        }
    }

    void Attack() {
        if (!enableAttacks)
            return;

        Collider[] hitColliders = Physics.OverlapBox(hitBox.transform.position, hitBox.transform.localScale, Quaternion.identity, layerMask);

        for (int i = 0; i < hitColliders.Length; i++) {
            Debug.Log("Dummy Found: " + hitColliders[i].name + " #" + i);
            if (hitColliders[i].GetComponent<HealthScript>() != null && hitColliders[i].name != "Dummy") {
                hitColliders[i].GetComponent<HealthScript>().TakeDamage(1);
                Debug.Log("Dummy Hit: " + hitColliders[i].name + " #" + i);
            }
        }
    }
}
