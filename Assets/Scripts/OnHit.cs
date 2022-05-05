using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnHit : MonoBehaviour
{

    private FighterController controller;
    private ParticleSystem PS;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponentInParent<FighterController>();
        PS = GetComponent<ParticleSystem>();
       
    }

    // Update is called once per frame
    void Update()
    {
        var em = PS.emission;
        if (controller.isHit){
            em.enabled = true;
        }
    }
}
