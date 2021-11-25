using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OverlaySwitch : MonoBehaviour
{
    public GameObject overlayCanvas;
    GameObject spawnedCanvas;
    public Dropdown drop;


    void Start()
    {
        if (!GameObject.Find("Overlay Canvas(Clone)") && GameObject.Find("Start Overlay"))
        {
            spawnedCanvas = Instantiate(overlayCanvas, transform.position, Quaternion.identity);
            Destroy(GameObject.Find("Start Overlay"));
        }
    }

    public void toggleOverlay()
    {
        if (!GameObject.Find("Overlay Canvas(Clone)"))
            spawnedCanvas = Instantiate(overlayCanvas, transform.position, Quaternion.identity);
        else
            Destroy(GameObject.Find("Overlay Canvas(Clone)"));
    }


    public void toggleOverlayDropDown()
    {

        if (drop.value == 0)
        {
            if (!GameObject.Find("Overlay Canvas(Clone)"))
                spawnedCanvas = Instantiate(overlayCanvas, transform.position, Quaternion.identity);
        }
        else if (drop.value == 1)
        {
            if(GameObject.Find("Overlay Canvas(Clone)"))
                Destroy(GameObject.Find("Overlay Canvas(Clone)"));
        }
    }
}
