using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
///   This class is responsible for having the model face toward the user on initial placement
/// </summary>
public class FaceUserOnPlacement : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Vector3 targetPos = new Vector3(Camera.main.transform.position.x, transform.position.y, Camera.main.transform.position.z);
        transform.LookAt(targetPos);
        gameObject.transform.eulerAngles = new Vector3(gameObject.transform.eulerAngles.x, gameObject.transform.eulerAngles.y - 90.0f, gameObject.transform.eulerAngles.z);

    }
}
