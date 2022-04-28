using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplitRotate : MonoBehaviour
{
    private FighterController controller;
    public Vector3 upDownAngle = new Vector3(0f, 0f, 0f);
    public Vector3 downUpAngle = new Vector3(0f, 0f, 180f);
    public Vector3 leftRightAngle = new Vector3(0f, 0f, 90f);
    public Vector3 rightLeftAngle = new Vector3(0f, 0f, 270f);
 
    private Vector3 currentAngle;
    public float rotationVelocity = 5.0f;

    void Start()
    {
        controller = GetComponentInParent<FighterController>();
        currentAngle = transform.eulerAngles;
    }

    // Update is called once per frame
    void Update()
    {
        switch (controller.controlSystem) {
            case ControlSystem.UpDown: //0
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, upDownAngle.x, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.y, upDownAngle.y, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.z, upDownAngle.z, Time.deltaTime*rotationVelocity));
 
                transform.eulerAngles = currentAngle;
                break;
            case ControlSystem.DownUp: //180
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, downUpAngle.x, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.y, downUpAngle.y, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.z, downUpAngle.z, Time.deltaTime*rotationVelocity));
 
                transform.eulerAngles = currentAngle;

                break;
            case ControlSystem.LeftRight: //90
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, leftRightAngle.x, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.y, leftRightAngle.y, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.z, leftRightAngle.z, Time.deltaTime*rotationVelocity));
 
                transform.eulerAngles = currentAngle;

                break;
            case ControlSystem.RightLeft: //270
                currentAngle = new Vector3(
                    Mathf.LerpAngle(currentAngle.x, rightLeftAngle.x, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.y, rightLeftAngle.y, Time.deltaTime*rotationVelocity),
                    Mathf.LerpAngle(currentAngle.z, rightLeftAngle.z, Time.deltaTime*rotationVelocity));
 
                transform.eulerAngles = currentAngle;
                break;
        }
    }
}
