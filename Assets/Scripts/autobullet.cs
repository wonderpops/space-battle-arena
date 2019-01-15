using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class autobullet : MonoBehaviour {
    public int damage;
    Vector2 move;
    Rigidbody2D rb;
    PhotonView target;
    PhotonPlayer thisShip;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();


        //move = (target.transform.position - transform.position).normalized * 7f;
        //rb.velocity = new Vector2(move.x, move.y);
       // Destroy(gameObject, 3f);
    }
	
	// Update is called once per frame
	void Update () {
        target = GetComponent<PhotonView>();
        Debug.Log(target.name);

    }
}
