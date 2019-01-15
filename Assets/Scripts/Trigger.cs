using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : Photon.MonoBehaviour {

    private Collider2D col;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void SetHealth(Collider2D col)
    {
        SpaceShip player = col.GetComponent<SpaceShip>();
        PhotonView pv = col.GetComponent<PhotonView>();
        player.playerStats.Health -= 1;
        Debug.Log(player.playerStats.Name +" "+ player.playerStats.Health);
        player.hpBar1.GetComponent<Image>().fillAmount = (float)player.playerStats.Health  / 10;
        //pv.RPC("PlayerSoundTrigger", PhotonTargets.All, player.playerStats.name);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (col != null){
            if (stream.isWriting){
                Debug.Log("YES");
                stream.SendNext(col.GetComponent<SpaceShip>().playerStats.Health);
            } else {
                col.GetComponent<SpaceShip>().playerStats.Health = (int)stream.ReceiveNext();
            }

        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        SpaceShip player = col.GetComponent<SpaceShip>();

        if (col.tag == "Player")
        {
            SetHealth(col);
            // if (photonView.isMine)
            //     PhotonNetwork.Destroy(gameObject);
        }
    }
}
