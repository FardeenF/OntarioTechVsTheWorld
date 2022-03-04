using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using SoarSDK;
using UnityEngine.Timeline;

public class VolumetricRenderTrackMixer : PlayableBehaviour
{

    public bool outOfClip;
    public bool paused;
    public bool played;
    public bool seeking;
    public bool newClip;

    public override void ProcessFrame(Playable playable, FrameData info, object playerData)
    {
        VolumetricRender volRender = playerData as VolumetricRender;
        if (!volRender.GetComponent<PlaybackInstance>()) { return; }

        int inputCount = playable.GetInputCount();
        for(int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0f)
            {
                ScriptPlayable<VolumetricRenderBehavior> inputPlayable = (ScriptPlayable<VolumetricRenderBehavior>)playable.GetInput(i);
                VolumetricRenderBehavior input = inputPlayable.GetBehaviour();
                PlayableDirector director = playable.GetGraph().GetResolver() as PlayableDirector;
                TimelineAsset timelineAsset = director.playableAsset as TimelineAsset;
                var currentIndex = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                PlaybackState state = volRender.GetInstanceState(currentIndex);

                switch (state)
                {
                    case PlaybackState.READY:
                        director.time += Time.deltaTime;
                        break;
                    case PlaybackState.BUFFERING:
                        director.time += Time.deltaTime;
                        break;
                    default:
                        director.time += 0;
                        Debug.Log("Decoding Video");
                        break;
                }

                if (!outOfClip)
                {
                    if (seeking)
                    {
                        newClip = true;
                    }
                    if (!seeking)
                    {
                        if (newClip)
                        {
                            var newIndex = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                            volRender.SeekToCursor(newIndex, (int)(inputPlayable.GetTime() * 1000000.0f));
                            volRender.StartPlayback(newIndex);
                            outOfClip = true;
                        }
                        if (!newClip)
                        {
                            var index = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                            volRender.SeekToCursor(index, 0);
                            outOfClip = true;
                        }
                    }
                }

                if (director.state == PlayState.Paused)
                {
                    if (info.seekOccurred)
                    {
                        var index = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                        seeking = true;
                        played = false;
                        volRender.SeekToCursor(index, (int)(inputPlayable.GetTime() * 1000000.0f));
                    }
                }

                if (director.state == PlayState.Playing)
                {
                    var index = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                    if (!played)
                    {
                        volRender.StartPlayback(index);
                        played = true;
                        seeking = false;
                    }
                }
            }

            if(inputWeight == 0f)
            {
                ScriptPlayable<VolumetricRenderBehavior> inputPlayable = (ScriptPlayable<VolumetricRenderBehavior>)playable.GetInput(i);
                VolumetricRenderBehavior input = inputPlayable.GetBehaviour();
                PlayableDirector director = playable.GetGraph().GetResolver() as PlayableDirector;

                if (director.state == PlayState.Paused)
                {
                    if (info.seekOccurred)
                    {
                        var index = volRender.instanceRef.IndexOf(volRender.GetComponent<PlaybackInstance>());
                        if(index == 0)
                        {
                            volRender.SeekToCursor(index, (int)(inputPlayable.GetDuration() * 1000000.0f));
                        }
                        if (index > 0)
                        {
                            volRender.SeekToCursor(index, 0);
                        }
                        outOfClip = false;
                        seeking = true;
                        played = false;

                    }
                }
            }
        }
    }

    public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        PlayableDirector director = playable.GetGraph().GetResolver() as PlayableDirector;
        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
        }
    }

    public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        int inputCount = playable.GetInputCount();
        for (int i = 0; i < inputCount; i++)
        {
            float inputWeight = playable.GetInputWeight(i);
            if (inputWeight > 0f)
            {
                played = false;
            }
        }
    }
}
