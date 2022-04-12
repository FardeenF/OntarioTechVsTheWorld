using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoMusicCheck : MonoBehaviour
{
    public VideoPlayer Video;
    //public Camera cam;
    public GameObject conductor;
   
    // Start is called before the first frame update
    void Start()
    {
        Video = this.gameObject.GetComponentInChildren<VideoPlayer>();
        //cam = FindObjectOfType<Camera>();
        //cam.gameObject.GetComponent<AudioSource>().enabled = true;

        conductor = GameObject.Find("Conductor");

    }

    // Update is called once per frame
    void Update()
    {
        if (Video.isPlaying)
        {
            conductor.gameObject.GetComponent<AudioSource>().mute = true;
            //cam.gameObject.GetComponent<AudioSource>().mute = true;
        }
        else if (!Video.isPlaying)
        {
            conductor.gameObject.GetComponent<AudioSource>().mute = false;
            //cam.gameObject.GetComponent<AudioSource>().mute = false;
        }
    }
}
