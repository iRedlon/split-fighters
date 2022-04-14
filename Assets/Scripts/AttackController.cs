using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackController : MonoBehaviour
{
    
    public LayerMask layerMask;
    public GameObject hitBoxHigh;
    public GameObject hitBoxLow;

    public float highAttackDuration = 0.5f;
    public float lowAttackDuration = 0.5f;


    private bool highAttacked = false;
    private bool lowAttacked = false;

    public bool attackInProgress = false;

    // Start is called before the first frame update
    void Start()
    {
        hitBoxHigh.GetComponent<MeshRenderer>().enabled = false;
        hitBoxLow.GetComponent<MeshRenderer>().enabled = false;
    }

    void ReadInputs() {
        if (Input.GetButtonDown("Attack 1")) {
            //if (!highAttacked) {
                StartCoroutine(HighAttack());
            //}
        }

        if (Input.GetButtonDown("Attack 2")) {
            //if (!lowAttacked) {
                StartCoroutine(LowAttack());
            //}
        }
    }

    public void StartHighAttack() {
        StartCoroutine(HighAttack());
    }
    public void StartLowAttack() {
        StartCoroutine(LowAttack());
    }

    IEnumerator HighAttack() {
        if (highAttacked) {
            yield break;
        }

        highAttacked = true;

        float timer = 0.0f;

        hitBoxHigh.GetComponent<MeshRenderer>().enabled = true;

        while (timer < highAttackDuration) {
            Collider[] hitColliders = Physics.OverlapBox(hitBoxHigh.transform.position,
                        hitBoxHigh.transform.localScale / 2, Quaternion.identity, layerMask);

            for (int i = 0; i < hitColliders.Length; i++) {
                if (hitColliders[i].GetComponent<HealthScript>() != null) {
                    hitColliders[i].GetComponent<HealthScript>().TakeDamage(1);
                }

                if (hitColliders[i].GetComponent<Dummy>() != null) {
                    hitColliders[i].GetComponent<Dummy>().KnockBack(10f, new Vector3(1f, 0.1f, 0f));
                }
            }

            timer += Time.deltaTime;

            yield return null;
        }


        hitBoxHigh.GetComponent<MeshRenderer>().enabled = false;
        highAttacked = false;
    }

    IEnumerator LowAttack() {
        if (lowAttacked) {
            yield break;
        }

        lowAttacked = true;

        float timer = 0.0f;

        hitBoxLow.GetComponent<MeshRenderer>().enabled = true;

        while (timer < lowAttackDuration) {
            Collider[] hitColliders = Physics.OverlapBox(hitBoxLow.transform.position,
                        hitBoxLow.transform.localScale / 2, Quaternion.identity, layerMask);

            for (int i = 0; i < hitColliders.Length; i++) {
                if (hitColliders[i].GetComponent<HealthScript>() != null) {
                    hitColliders[i].GetComponent<HealthScript>().TakeDamage(1);
                }

                if (hitColliders[i].GetComponent<Dummy>() != null) {
                    hitColliders[i].GetComponent<Dummy>().KnockBack(10f, new Vector3(0.5f, 1f, 0f));
                }
            }
            timer += Time.deltaTime;
            yield return null;
        }

        hitBoxLow.GetComponent<MeshRenderer>().enabled = false;
        lowAttacked = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (highAttacked || lowAttacked) {
            attackInProgress = true;
        } else {
            attackInProgress = false;
        }
        //ReadInputs();
    }
}