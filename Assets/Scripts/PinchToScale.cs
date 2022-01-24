using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
///   This class is responsible for scaling manipulation controls for AR objects.
/// </summary>
public class PinchToScale : MonoBehaviour
{
    /// <summary>
    ///   Represents initial distance between first and second touch point
    /// </summary>
    [HideInInspector] public float initialDistance;
    /// <summary>
    ///   Represents initial scale of gameobject
    /// </summary>
    [HideInInspector] public Vector3 initialScale;
    /// <summary>
    ///   Represents current distance between first and second touch point
    /// </summary>
    [HideInInspector] public float currentDistance;
    /// <summary>
    ///   Represents factor gamobject scale changes by
    /// </summary>
    [HideInInspector] public float factor;
    /// <summary>
    ///   Represents bool checking if gameobject has been tapped
    /// </summary>
    [HideInInspector] public bool checkHit;
    /// <summary>
    ///   Represents bool checking if the initial scale has been set
    /// </summary>
    [HideInInspector] public bool startScale;
    /// <summary>
    ///   Represents gameobject that has been tapped on
    /// </summary>
    [HideInInspector] public GameObject hitObject;

    // Start is called before the first frame update
    void Start()
    {
        startScale = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount == 2)
        {
            var touchZero = Input.GetTouch(0);
            var touchOne = Input.GetTouch(1);
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(0).position);
            if (Physics.Raycast(ray, out hit))
            {
                if (hit.transform.gameObject.tag == "Mesh" && hitObject == null)
                {
                    startScale = true;
                    hitObject = hit.transform.gameObject;
                }

            }

            if(startScale == true && hitObject != null)
            {

                if (touchZero.phase == TouchPhase.Began || touchOne.phase == TouchPhase.Began)
                {
                    checkHit = true;
                    initialDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    initialScale = hitObject.transform.localScale;
                }
                else
                {
                    currentDistance = Vector2.Distance(touchZero.position, touchOne.position);
                    if (Mathf.Approximately(initialDistance, 0))
                    {
                        return;
                    }
                    if (checkHit == true)
                    {
                        factor = currentDistance / initialDistance;
                        hitObject.transform.localScale = factor * initialScale;
                    }

                }
            }
        }

        if(Input.touchCount <= 1)
        {
            startScale = false;
            checkHit = false;
            hitObject = null;
        }
    }
}
