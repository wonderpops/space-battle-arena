using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerControl : MonoBehaviour {
    public float speed = 2f;
    public Joystick joystick;

	// Use this for initialization
	void Start () {
		
	}

    // Update is called once per frame
    void Update() {
     
        transform.position = transform.position + new Vector3 (joystick.Direction.x * speed, joystick.Direction.y * speed, 0);
    }
}
