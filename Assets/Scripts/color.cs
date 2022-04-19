using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class color : MonoBehaviour
{
    public Color c;
    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<MeshRenderer>().material.color = c;
    }
}
