using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MultiTargetCamera : MonoBehaviour
{
    public List<Transform> targets;

    public Vector3 offset;
    public float smoothTime = .5f;
    public float minZoom = 80f;
    public float maxZoom = 30f;
    public float zoomLimiter = 10f;

    private Vector3 velocity;
    private Camera cam;

    void Start(){
        cam = GetComponent<Camera>();
    }

    void LateUpdate(){
        if (targets[1].name == "Empty") {
            Debug.Log("Finding second controller");
            GameObject a = GameObject.Find("CharacterFighter(Clone)");
            if (a != null) {
                targets[1] = a.transform;

            }
        }

        if (targets.Count ==0){
            return;
        }
        Move();
        Zoom();
    }

    void Zoom(){
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance()/zoomLimiter);
        //cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
        cam.fieldOfView = newZoom;
        
    }

    void Move(){
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance(){
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++){
            bounds.Encapsulate(targets[i].position);
        }
        return bounds.size.x;
    }


    Vector3 GetCenterPoint(){
        if (targets.Count ==1){
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Count; i++){
            bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
}
