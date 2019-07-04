using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Playables;

public class Sc001M02 : MonoBehaviour {

    public PlayableDirector director;
    public Button greenRole;
	// Use this for initialization
	void Start () {
        greenRole.onClick.AddListener(delegate() {
            GameManager.instance.CtrlPlayableDirPause(director,false);
        });
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
