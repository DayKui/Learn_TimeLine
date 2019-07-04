using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CtrlCan : MonoBehaviour {

    public GameObject can;
	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (GameManager.instance.isSet)
        {
            can.SetActive(true);
        }
        if (GameManager.instance.isDisSliderl&& GameManager.instance.sliderShowNum==2)
        {
            can.SetActive(false);
        }
        
	}
}
