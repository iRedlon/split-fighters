using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetModel : MonoBehaviour
{
    public Color p1Color;
    public Color p2Color;

    public List<GameObject> models;
    // Start is called before the first frame update
    void Start()
    {
        if (this.name == "Character") {
            SetModelColor(p1Color);
        } else {
            SetModelColor(p2Color);
        }
    }

    void SetModelColor(Color c) {
        foreach (GameObject model in models) {
            model.GetComponent<MeshRenderer>().material.color = c;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
