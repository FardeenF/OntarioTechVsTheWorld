using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SoarSDK;
using UnityEngine.UI;

public class PlaybackControls : MonoBehaviour
{

    public VolumetricRender playbackComponent;
    public Slider scrubbingSlider;
    private bool getSliderHandle;
    private int volumetricIndex;

    // Start is called before the first frame update
    void Start()
    {

    }

    private void Update()
    {

        if(playbackComponent != null)
        {
            scrubbingSlider.maxValue = playbackComponent.GetFullDuration(0);

            if (!getSliderHandle)
            {
                scrubbingSlider.value = playbackComponent.GetCurrentPosition(0);
            }
            else
            {
                Debug.Log("Scrubbing");
            }
        }
    }

    public void SeekToTimestamp()
    {
        getSliderHandle = false;
        playbackComponent.SeekToCursor(volumetricIndex, (int)scrubbingSlider.value);
        PlaybackStart();
    }

    public void GetSliderHandle()
    {
        getSliderHandle = true;
        PlaybackStop();
    }

    public void PlaybackStart()
    {
        playbackComponent.StartPlayback(volumetricIndex);
    }

    public void PlaybackPause()
    {
        playbackComponent.PauseModel(volumetricIndex);
    }

    public void PlaybackStop()
    {
        playbackComponent.StopModel(volumetricIndex);
    }
}
