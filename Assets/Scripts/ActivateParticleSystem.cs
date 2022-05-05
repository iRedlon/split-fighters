using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParticleSystem : MonoBehaviour
{

    public ParticleSystem sparkParticle;
    public bool activate = false;
    private FighterController controller;


    // Start is called before the first frame updat
    void Start(){
        sparkParticle = GetComponent<ParticleSystem>();
        controller = GetComponentInParent<FighterController>();
    }
    void Update()
    {
        
        if (controller.isHit){
            sparkParticle.Play();
            
        }
    }
    


    
}
