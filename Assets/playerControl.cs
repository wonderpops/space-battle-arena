using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

     
            transform.position =  new Vector3(0, 0 +0.5f);
   
    }

    private void SleepTimeout(int v)
    {
        throw new NotImplementedException();
    }
}
