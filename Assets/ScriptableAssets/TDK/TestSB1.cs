using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;

// A behaviour that is attached to a playable
public class TestSB1 : PlayableBehaviour
{
    private PlayableDirector director;
    private bool isReadPaused=false;
    private bool isClipPlayed = false;
   
    // Called when the owning graph starts playing
    public override void OnPlayableCreate(Playable playable)
    {
        director = (playable.GetGraph().GetResolver() as PlayableDirector);
    }
    public override void OnGraphStart(Playable playable) {
		
	}

	// Called when the owning graph stops playing
	public override void OnGraphStop(Playable playable) {
		
	}

	// Called when the state of the playable is set to Play
	public override void OnBehaviourPlay(Playable playable, FrameData info)
    {
        if (!isClipPlayed && info.weight > 0f)
        {
            isClipPlayed = true;
            if (Application.isPlaying)
            {
                if (director.transform.childCount>0)
                {
                    director.transform.GetChild(0).gameObject.SetActive(true);
                }
                if (!isReadPaused)
                {
                    isReadPaused = true;
                }
                if (director.transform .parent.name== "Sc001_M002_0001_02")
                {
                    GameManager.instance.CtrlCancasIsShow(true,0);
                    GameManager.instance.CtrlSlidetContShow(true);
                    return;
                }
                if (director .gameObject .name== "wb_Sc003_TimeLine")
                {
                    return;
                }
                if (director.gameObject .name== "wb_P1-P2caozuo_TimeLine")
                {
                    GameManager.instance.sliderShowNum = 2;
                    GameManager.instance.CtrlSlidetContShow(true);
                    return;
                }
                else
                {
                    GameManager.instance.CtrlCancasIsShow(false);
                }
            }

        }
    }

	// Called when the state of the playable is set to Paused
	public override void OnBehaviourPause(Playable playable, FrameData info)
    {
        if (isReadPaused)
        {
            GameManager.instance.CtrlPlayableDirPause(director,true);
        }	
	}

	// Called each frame while the state is set to Play
	public override void PrepareFrame(Playable playable, FrameData info) {
		
	}
}
