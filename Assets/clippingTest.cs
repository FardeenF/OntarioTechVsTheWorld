using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clippingTest : MonoBehaviour
{
    Camera myCamera;
    // Start is called before the first frame update
    void Start()
    {
        myCamera = this.GetComponent<Camera>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        myCamera.nearClipPlane = 0.05f;
        myCamera.farClipPlane = 400.0f;
    }
}
