using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class PlayNextSA : PlayableAsset
{
    public ExposedReference<GameObject> nextPre;
	// Factory method that generates a playable based on this asset
	public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
       
        PlayNextSB playSb = new PlayNextSB();
        playSb.nextPrefab = nextPre.Resolve(graph.GetResolver());
        return ScriptPlayable<PlayNextSB>.Create(graph, playSb);
    }
}
