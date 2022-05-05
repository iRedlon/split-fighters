using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateParticleSystem : MonoBehaviour
{

    public ParticleSystem sparkParticle;
    public bool activate = false;
    private FighterController controller;

    public GameObject parent;

    // Start is called before the first frame updat
    void Start(){
        Debug.Log("HEYYYY");
        sparkParticle = GetComponent<ParticleSystem>();
        controller = parent.GetComponent<FighterController>();
        //controller = GetComponentInParent<FighterController>();
    }
    void Update()
    {
        Debug.Log(controller.isHit);
        
        if (controller.isHit){
            //sparkParticle.Play();
        }
    }
    


    
}
