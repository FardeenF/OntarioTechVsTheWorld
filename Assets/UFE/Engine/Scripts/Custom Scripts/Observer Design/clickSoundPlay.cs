using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickSoundPlay : MonoBehaviour
{
    private AudioSource _audiosource;

    private void Awake()
    {
        _audiosource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        DefaultMainMenuScreen.userClick += PlayAudio;
    }

    private void OnDisable()
    {
        DefaultMainMenuScreen.userClick -= PlayAudio;
    }

    private void PlayAudio()
    {
        _audiosource.Play();
    }
}
