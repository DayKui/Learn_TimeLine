using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlSliderScript : MonoBehaviour {
  
    public  GameObject sl;
    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (!sl.activeInHierarchy)
        {
            sl.SetActive(true);
        }	
	}
}
