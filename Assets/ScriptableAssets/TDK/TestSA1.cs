using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

[System.Serializable]
public class TestSA1 : PlayableAsset
{
    // Factory method that generates a playable based on this asset
    TestSB1 sb1 = new TestSB1();
    public override Playable CreatePlayable(PlayableGraph graph, GameObject go) {
        Debug.LogError(go.name + "==6666666666666666666");
        return ScriptPlayable<TestSB1>.Create(graph,sb1);
	}
}
