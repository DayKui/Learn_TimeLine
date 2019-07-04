using System;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Timeline;

[Serializable]
public class YlClip : PlayableAsset, ITimelineClipAsset
{
    public YlBehaviour template = new YlBehaviour ();

    public ClipCaps clipCaps
    {
        get { return ClipCaps.Blending; }
    }

    public override Playable CreatePlayable (PlayableGraph graph, GameObject owner)
    {
        var playable = ScriptPlayable<YlBehaviour>.Create (graph, template);
        return playable;
    }
}
