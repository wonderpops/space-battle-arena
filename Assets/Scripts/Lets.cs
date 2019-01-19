using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lets : Photon.MonoBehaviour
{
    // Use this for initialization
    
    void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate((Vector3.right) * Time.deltaTime * 5f);
        if ((gameObject.transform.position.x > 3) || (gameObject.transform.position.x < -3))
            Destroy(gameObject);   
    }
}
