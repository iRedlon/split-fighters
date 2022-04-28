using UnityEngine;
using System.Collections;


[ExecuteInEditMode]
public class WriteToDepth : MonoBehaviour
{
    void OnEnable() {
        GetComponent<Camera>().depthTextureMode = DepthTextureMode.DepthNormals;
    }
}
