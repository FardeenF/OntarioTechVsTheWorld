using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class PlacementExample : MonoBehaviour
{

    public InputActionReference placeReference = null;
    public GameObject volModel;


    private void Awake()
    {
        placeReference.action.started += Place;
    }

    private void OnDestroy()
    {
        placeReference.action.started -= Place;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Place(InputAction.CallbackContext context)
    {
        RaycastHit hit;
        if(Physics.Raycast(gameObject.transform.position, gameObject.transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Instantiate(volModel, hit.point, Quaternion.identity);
        }
    }
}
