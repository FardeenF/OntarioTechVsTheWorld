using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAudio : MonoBehaviour
{
    public AudioSource clip;
    public bool play;

    private void OnCollisionEnter(Collision collision)
    {
        if (play)
            clip.Play();
    }
}
