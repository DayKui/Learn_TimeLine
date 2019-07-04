using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TimeSA : PlayableAsset
{
    public ExposedReference <GameObject>nextObj;
	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        TimeSB sb = new TimeSB();
        sb.nextpre = nextObj.Resolve(graph.GetResolver());

        return ScriptPlayable<TimeSB>.Create(graph, sb);
    }
}
