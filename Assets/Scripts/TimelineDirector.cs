using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;
using SoarSDK;

public class TimelineDirector : MonoBehaviour
{
    public TimelineAsset timelineasset;
    public PlayableDirector director;
    public bool ready;

    private void Awake()
    {
        director = GetComponent<PlayableDirector>();
        director.stopped += Director_stopped;
        director.paused += Director_paused;
    }

    private void Director_paused(PlayableDirector obj)
    {
        var outputTracks = timelineasset.GetOutputTracks();

        foreach (var outputTrack in outputTracks)
        {
            if (outputTrack is VolumetricRenderTrack)
            {
                VolumetricRender volRender = director.GetGenericBinding(outputTrack) as VolumetricRender;
                var index = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                volRender.PauseModel(index);
            }

        }
    }

    private void Director_stopped(PlayableDirector obj)
    {
        var outputTracks = timelineasset.GetOutputTracks();
        foreach (var outputTrack in outputTracks)
        {
            if (outputTrack is VolumetricRenderTrack)
            {
                VolumetricRender volRender = director.GetGenericBinding(outputTrack) as VolumetricRender;
                var index = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                volRender.StopModel(index);
            }

        }
    }

    private void Update()
    {

        if(ready == false)
        {
            var outputTracks = timelineasset.GetOutputTracks();

            foreach (var outputTrack in outputTracks)
            {
                if (outputTrack is VolumetricRenderTrack)
                {
                    VolumetricRender volRender = director.GetGenericBinding(outputTrack) as VolumetricRender;
                    if(volRender.instanceRef.Count > 0)
                    {
                        var index = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                        
                        if (volRender.GetFullDuration(index) != 0)
                        {
                            if (volRender.GetComponent<MeshRenderer>().material.GetTexture("_CameraRGB") != null)
                            {
                                var c = outputTrack.GetClips();
                                foreach (var clip in c)
                                {
                                    var duration = (volRender.GetFullDuration(index) / 1000000.0f);
                                    clip.duration = duration;
                                }
                                ready = true;
                            }
                        }
                    }
                }

            }
        }
    }

    public void PlayTimeline()
    {
        director.Play();
    }

    public void StopTimeline()
    {
        director.Stop();
    }

    public void PauseTimeline()
    {
        director.Pause();
    }
}
