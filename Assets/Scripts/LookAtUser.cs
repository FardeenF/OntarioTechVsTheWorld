using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   This class is responsible for making sure an object always face toward the user when placed in AR.
/// </summary>
public class LookAtUser : MonoBehaviour
{

    // Start is called before the first frame update


    private void FixedUpdate()
    {
        gameObject.transform.localPosition = new Vector3(0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Rotate the camera every frame so it keeps looking at the target
        Vector3 targetPos = new Vector3(Camera.main.transform.localPosition.x, transform.localPosition.y, Camera.main.transform.localPosition.z);
        transform.LookAt(targetPos);

    }
}
