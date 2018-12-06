using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void HP(Collider2D col)
    {
        SpaceShipControl player = col.GetComponent<SpaceShipControl>();
        PhotonView pv = col.GetComponent<PhotonView>();
        player.playerStats.hp -= 1;
        player.hpBar.fillAmount = (float)player.playerStats.hp/ 10;
        Debug.Log(player.playerStats.hp.ToString()+player.playerStats.name);
        //pv.RPC("PlayerSoundTrigger", PhotonTargets.All, player.playerStats.name);
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SpaceShipControl player = col.GetComponent<SpaceShipControl>();

        if (col.tag == "Player")
        {
            HP(col);

            if (!PhotonNetwork.isMasterClient)
                PhotonNetwork.Destroy(gameObject);
        }
    }
}
