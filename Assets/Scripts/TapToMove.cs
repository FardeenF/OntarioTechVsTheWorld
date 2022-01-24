using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

/// <summary>
///   This class is responsible for movement manipulation of AR objects.
/// </summary>
public class TapToMove : MonoBehaviour
{

    /// <summary>
    ///   Represents moving object
    /// </summary>
    [HideInInspector] public GameObject tappedObject;
    /// <summary>
    ///   Represents position of the moving object in the previous frame
    /// </summary>
    [HideInInspector] public Vector3 tappedObjectLastPosition;
    /// <summary>
    ///   Represents position of the moving object in the current frame
    /// </summary>
    [HideInInspector] public Vector3 tappedObjectCurrentPosition;
    /// <summary>
    ///   Represents the change in the touch position
    /// </summary>
    [HideInInspector] public Vector2 screenDelta;


    void Update()
    {
        if (Input.touchCount == 1)
        {
            screenDelta = Input.GetTouch(0).deltaPosition;
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Mesh")
                {
                    if (tappedObject == null)
                    {
                        tappedObject = hit.transform.gameObject;
                    }
                }

            }
            if (tappedObject != null)
            {
                tappedObject.transform.GetChild(1).GetComponent<Rigidbody>().AddRelativeForce(new Vector3((screenDelta.x / 20), 0, (screenDelta.y / 20)), ForceMode.Force);
                tappedObject.GetComponent<Rigidbody>().velocity = tappedObject.transform.GetChild(1).GetComponent<Rigidbody>().velocity;
                tappedObject.GetComponent<Rigidbody>().angularVelocity = tappedObject.transform.GetChild(1).GetComponent<Rigidbody>().angularVelocity;
                tappedObjectCurrentPosition = tappedObject.transform.position;
                tappedObjectLastPosition = tappedObjectCurrentPosition;
            }

        }

        if (Input.touchCount == 0)
        {
            if (tappedObject != null)
            {
                tappedObject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                tappedObject.transform.GetChild(1).GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                tappedObject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
                tappedObject.transform.GetChild(1).GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
                screenDelta = new Vector2(0, 0);
            }
        }
    }

}
