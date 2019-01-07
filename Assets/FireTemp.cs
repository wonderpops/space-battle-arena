using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTemp : Photon.MonoBehaviour {

    public GameObject bullet;
    public GameObject pointBullet;


    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void BulletSpawn()
    {
        Instantiate(bullet, pointBullet.transform.position, pointBullet.transform.rotation);
    }
}
