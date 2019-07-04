using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Spine;
using Spine.Unity;

public class CtrlAnimation : MonoBehaviour {

    public SkeletonAnimation skeAni;
	// Use this for initialization
	void Start ()
    {
        Invoke("CtrlAniSta",0.2f);
    }
    void CtrlAniSta()
    {
        skeAni.state.SetAnimation(0, "Sc001_M001_0002", false);
    }
	// Update is called once per frame
	void Update () {
		
	}
}
