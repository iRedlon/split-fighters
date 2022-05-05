using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RotationTrigger { Timer, OnHit }

public class FighterManager : MonoBehaviour
{
    [SerializeField] public FighterController p1FC, p2FC;
    
    public RotationTrigger rotationTrigger = RotationTrigger.Timer;

    public Image LeftControllerIcon;
    public Image RightControllerIcon;


    public float currentTriggerTimer = 20f;
    public float triggerTimer = 20f;

    void Awake()
    {
        currentTriggerTimer = triggerTimer;
    }

    void Start () {
        var icon1 = GameObject.Find("Left Controller Icon");
        if (icon1 != null) {
            LeftControllerIcon = icon1.GetComponent<Image>();
        }
        icon1 = GameObject.Find("Right Controller Icon");
        if (icon1 != null) {
            RightControllerIcon = icon1.GetComponent<Image>();
        }
    }
    void Update()
    {
        if (rotationTrigger == RotationTrigger.Timer)
        {
            // UpdateTriggerTimer();
        }
    }

    void UpdateTriggerTimer()
    {
        if (currentTriggerTimer > 0)
        {
            currentTriggerTimer -= Time.deltaTime;
        }
        else
        {
            RotateSplitLine(p1FC);

            if (p2FC)
            {
                RotateSplitLine(p2FC);
            }

            triggerTimer -= 2f;
            currentTriggerTimer = Math.Max(5f, triggerTimer);
        }
    }

    public void RotateSplitLine(FighterController fc)
    {   
        Image controllerIcon;
        if (fc == p1FC) {
            controllerIcon = LeftControllerIcon;
        } else { 
            controllerIcon = RightControllerIcon;
        }
        ControlSystem newControlSystem = default;

        switch (fc.controlSystem)
        {
            case ControlSystem.UpDown:
                newControlSystem = ControlSystem.RightLeft;
                controllerIcon.GetComponent<ChangeUIControlScheme>().setControlScheme("right");
                break;
            case ControlSystem.RightLeft:
                newControlSystem = ControlSystem.DownUp;
                controllerIcon.GetComponent<ChangeUIControlScheme>().setControlScheme("down");
                break;
            case ControlSystem.DownUp:
                newControlSystem = ControlSystem.LeftRight;
                controllerIcon.GetComponent<ChangeUIControlScheme>().setControlScheme("left");
                break;
            case ControlSystem.LeftRight:
                newControlSystem = ControlSystem.UpDown;
                controllerIcon.GetComponent<ChangeUIControlScheme>().setControlScheme("up");
                break;
        }

        fc.controlSystem = newControlSystem;
    }
}