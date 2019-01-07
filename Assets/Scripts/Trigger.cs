using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : Photon.MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HP(Collider2D col)
    {
        SpaceShip player = col.GetComponent<SpaceShip>();
        PhotonView pv = col.GetComponent<PhotonView>();
        player.playerStats.Health -= 1;
        player.hpBar.fillAmount = (float)player.playerStats.Health  / 10;
        //pv.RPC("PlayerSoundTrigger", PhotonTargets.All, player.playerStats.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SpaceShip player = col.GetComponent<SpaceShip>();

        if (col.tag == "Player")
        {
            HP(col);
            if (photonView.isMine)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
