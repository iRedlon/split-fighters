using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModel : MonoBehaviour
{
    public FighterController fighterController;

    public Color hitColor;
    public Color mainP1Color;
    public Color mainP2Color;


    public Color p1Color;
    public Color p2Color;

    public Color p3Color;
    public Color p4Color;


    public GameObject Head;
    public GameObject Torso;

    public GameObject BicepL;
    public GameObject ArmL;
    
    public GameObject LegL;
    public GameObject ThighL;

    public GameObject BicepR;
    public GameObject ArmR;

    public GameObject LegR;
    public GameObject ThighR;

    List<GameObject> allLimbs = new List<GameObject>();

    // public ControlSystem controlScheme = default;

    public List<GameObject> models;
    // Start is called before the first frame update
    void Start()
    {
        allLimbs.Add(Head);
        allLimbs.Add(Torso);
        allLimbs.Add(BicepL);
        allLimbs.Add(BicepR);
        allLimbs.Add(ArmL);
        allLimbs.Add(ArmR);
        allLimbs.Add(LegL);
        allLimbs.Add(LegR);
        allLimbs.Add(ThighR);
        allLimbs.Add(ThighL);
    }

    void SetModelColor_UpDownSplitInputs(Color leftControllerColor, Color rightControllerColor) {
        // TOP
        SetLimbColor(BicepL, leftControllerColor);
        SetLimbColor(ArmL, leftControllerColor);

        SetLimbColor(BicepR, leftControllerColor);
        SetLimbColor(ArmR, leftControllerColor);

        // Bottom
        SetLimbColor(ThighL, rightControllerColor);
        SetLimbColor(LegL, rightControllerColor);

        SetLimbColor(ThighR, rightControllerColor);
        SetLimbColor(LegR, rightControllerColor);
    }

    void SetModelColor_LeftRightSplitInputs(Color leftControllerColor, Color rightControllerColor) {
        // TOP
        SetLimbColor(BicepL, leftControllerColor);
        SetLimbColor(ArmL, leftControllerColor);

        SetLimbColor(BicepR, rightControllerColor);
        SetLimbColor(ArmR, rightControllerColor);

        // Bottom
        SetLimbColor(ThighL, leftControllerColor);
        SetLimbColor(LegL, leftControllerColor);

        SetLimbColor(ThighR, rightControllerColor);
        SetLimbColor(LegR, rightControllerColor);
    }

    void SetLimbColor(GameObject limb, Color c) {
        limb.GetComponent<MeshRenderer>().material.color = c;
    }

    void SetModelColor(Color leftControllerColor, Color rightControllerColor) {
        switch (fighterController.controlSystem) {
            case ControlSystem.UpDown:
                SetModelColor_UpDownSplitInputs(leftControllerColor, rightControllerColor);
                break;
            case ControlSystem.RightLeft:
                SetModelColor_LeftRightSplitInputs(rightControllerColor, leftControllerColor);
                break;
            case ControlSystem.DownUp:
                SetModelColor_UpDownSplitInputs(rightControllerColor, leftControllerColor);
                break;
            case ControlSystem.LeftRight:
                SetModelColor_LeftRightSplitInputs(leftControllerColor, rightControllerColor);
                break;
        }
    }

    /*
    void SetModelColor(Color c) {
        foreach (GameObject model in models) {
            model.GetComponent<MeshRenderer>().material.color = c;
        }
        
    }
    */

    public void StartTakeDamageEffect(float duration) {
        StartCoroutine(TakeDamageEffect(duration));
    }


    // duration (in secs)
    IEnumerator TakeDamageEffect(float duration) {
        
        float rate = 0.1f;

        int count = (int)(duration / rate);

        int i = 0;
        while (i < count) {
            foreach (GameObject model in allLimbs) {
                model.GetComponent<MeshRenderer>().material.color = hitColor;
            }

            yield return new WaitForSeconds(rate);

            i++;
        }

    }

    void SetTorsoColor(Color c) {
        SetLimbColor(Head, c);
        SetLimbColor(Torso, c);
    }

    // Update is called once per frame
    void Update()
    {
        if (this.name == "Character") {
            SetModelColor(p1Color, p2Color);
            SetTorsoColor(mainP1Color);
        } else {
            // SetModelColor(p2Color);
            SetModelColor(p3Color, p4Color);
            SetTorsoColor(mainP2Color);
        }
    }
}
