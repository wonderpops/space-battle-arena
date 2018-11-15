using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {
    public float speed = 2f;
    public Joystick joystick;

	// Use this for initialization
	void Start () {
		
	}
<<<<<<< HEAD
	
	// Update is called once per frame
	void Update () {

     
            transform.position =  new Vector3(0, 0 +0.5f);
   
    }

    private void SleepTimeout(int v)
    {
        throw new NotImplementedException();
=======

    // Update is called once per frame
    void Update() {
     
        transform.position = transform.position + new Vector3 (joystick.Direction.x * speed, joystick.Direction.y * speed, 0);
>>>>>>> 0a7582032b7b0fcba0079a9ccb4af38d531f0500
    }
}
