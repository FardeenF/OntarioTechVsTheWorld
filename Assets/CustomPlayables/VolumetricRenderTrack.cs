using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Timeline;
using SoarSDK;
using UnityEngine.Playables;

[TrackBindingType(typeof(VolumetricRender))]
[TrackClipType(typeof(VolumetricRenderClip))]
public class VolumetricRenderTrack : TrackAsset
{
    public override Playable CreateTrackMixer(PlayableGraph graph, GameObject go, int inputCount)
    {
        return ScriptPlayable<VolumetricRenderTrackMixer>.Create(graph, inputCount);
    }
}
