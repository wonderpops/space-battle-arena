using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : Photon.MonoBehaviour {

    public int Damage;
    private Vector3 realpos = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.up * Time.deltaTime * 5f);
        if ((gameObject.transform.position.y > 7) || (gameObject.transform.position.y < -7))
            Destroy(gameObject);
    }

}
