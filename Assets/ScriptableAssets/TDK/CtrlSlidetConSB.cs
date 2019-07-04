using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class CtrlSlidetConSB : PlayableBehaviour
{
    public GameObject sc;
    public bool isPlaying=false;
    public PlayableDirector director;
    bool isSet = false;
	// Called when the owning graph starts playing
	public override void OnGraphStart(Playable playable) {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable) {
		
	}

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info) {

        if (Application.isPlaying && info.weight>0)
        {
            isPlaying = true;
        }
	}

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (Application.isPlaying&&isPlaying)
        {
            GameManager.instance.CtrlPlayableDirPause(director, true);
            if (!GameManager.instance.isSet)
            {
                director.gameObject.SetActive(false);
                director.gameObject.SetActive(true);
                GameManager.instance.isSet = true;
            }
        }	
	}

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info)
    {
        sc.SetActive(true);
    }
}
