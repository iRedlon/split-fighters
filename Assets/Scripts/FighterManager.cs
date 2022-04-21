using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationTrigger { Timer, OnHit }

public class FighterManager : MonoBehaviour
{
    [SerializeField] public FighterController p1FC, p2FC;
    
    public RotationTrigger rotationTrigger = RotationTrigger.Timer;

    private float triggerTimer = 10f;

    void Update()
    {
        if (rotationTrigger == RotationTrigger.Timer)
        {
            UpdateTriggerTimer();
        }
    }

    void UpdateTriggerTimer()
    {
        if (triggerTimer > 0)
        {
            triggerTimer -= Time.deltaTime;
        }
        else
        {
            RotateSplitLine(p1FC);

            if (p2FC)
            {
                RotateSplitLine(p2FC);
            }

            triggerTimer = 10f;
        }
    }

    public void RotateSplitLine(FighterController fc)
    {
        ControlSystem newControlSystem = default;

        switch (fc.controlSystem)
        {
            case ControlSystem.UpDown:
                newControlSystem = ControlSystem.RightLeft;
                break;
            case ControlSystem.RightLeft:
                newControlSystem = ControlSystem.DownUp;
                break;
            case ControlSystem.DownUp:
                newControlSystem = ControlSystem.LeftRight;
                break;
            case ControlSystem.LeftRight:
                newControlSystem = ControlSystem.UpDown;
                break;
        }

        fc.controlSystem = newControlSystem;
    }
}