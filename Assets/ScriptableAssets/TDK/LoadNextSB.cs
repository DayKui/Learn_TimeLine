using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class LoadNextSB : PlayableBehaviour
{
    public bool isPlayed = false;
    public PlayableDirector director;
	// Called when the owning graph starts playing
	public override void OnGraphStart(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable)
    {
        if (Application.isPlaying && isPlayed)
        {
            if (director.name== "wb_Sc03_51_TimeLine")
            {
                GameManager .instance.CtrlEveryChapterShow(3);
            }
            string num = director.transform.parent.name.Substring(9,1);
            int nextNum = int.Parse(num)+1;
            GameManager.instance.ShowPrefab(nextNum);
        }	
	}

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info) {
		
	}

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info) {

		
	}

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info)
    {
        if (Application.isPlaying && info .weight>0)
        {
            isPlayed = true;
        }	
	}
}
