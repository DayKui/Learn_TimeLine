using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dest : MonoBehaviour {

    public GameObject lastobj;
	// Use this for initialization
	void Start () {
        Destroy(lastobj,1f);
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
