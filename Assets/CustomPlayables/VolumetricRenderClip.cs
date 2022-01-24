using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

public class VolumetricRenderClip : PlayableAsset
{


    public override Playable CreatePlayable(PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<VolumetricRenderBehavior>.Create(graph);
        VolumetricRenderBehavior volumetricRenderBehavior = playable.GetBehaviour();
        return playable;
    } 

}
