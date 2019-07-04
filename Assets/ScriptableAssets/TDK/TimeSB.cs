using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class TimeSB : PlayableBehaviour
{
    public bool isPlayed;
    public PlayableDirector director;
    public GameObject nextpre;
	// Called when the owning graph starts playing
	public override void OnGraphStart(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable) {
		
	}

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info) {
		
	}

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (isPlayed&&Application.isPlaying)
        {
            nextpre.SetActive(true);
        }	
	}

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (!isPlayed && info.weight > 0f)
        {
            isPlayed = true;
        }
    }
}
