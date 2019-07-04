using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class CtrlSliedetCon : PlayableAsset
{
    public ExposedReference<GameObject> sliCon;
    public GameObject sli;
	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        CtrlSlidetConSB sb = new CtrlSlidetConSB();
        sb.sc = sliCon .Resolve(graph.GetResolver()); ;
		return ScriptPlayable<CtrlSlidetConSB>.Create(graph,sb);
	}
}
