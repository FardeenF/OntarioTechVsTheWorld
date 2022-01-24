using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   This class is responsible for rotation manipulation controls for AR objects.
/// </summary>
public class PinchToRotate : MonoBehaviour
{
    /// <summary>
    ///   Represents bool tracking if gameobject is being rotated
    /// </summary>
    [HideInInspector] public bool startRotate;
    /// <summary>
    ///   Represents desired rotation value
    /// </summary>
    [HideInInspector] public Quaternion desiredRotation;
    /// <summary>
    ///   Represents object that is being rotated
    /// </summary>
    [HideInInspector] public GameObject hitObject;
    // Start is called before the first frame update
    void Start()
    {
        startRotate = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	void LateUpdate()
	{
        if (Input.touchCount == 2)
        {
            startRotate = true;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Mesh" && hitObject == null)
                {
                    hitObject = hit.transform.gameObject;
                    startRotate = true;
                }

            }

            if (startRotate == true && hitObject != null)
            {
                desiredRotation = hitObject.transform.rotation;

                DetectTouchMovement.Calculate();


                if (Mathf.Abs(DetectTouchMovement.turnAngleDelta) > 0)
                { 
                    Vector3 rotationDeg = Vector3.zero;
                    rotationDeg.y = -DetectTouchMovement.turnAngleDelta;
                    desiredRotation *= Quaternion.Euler(rotationDeg);
                }

                hitObject.transform.rotation = desiredRotation;
            }
        }


        if(Input.touchCount <= 1)
        {
            startRotate = false;
            hitObject = null;
        }
	}
}
