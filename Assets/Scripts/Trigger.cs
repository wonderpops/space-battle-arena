using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Trigger : Photon.MonoBehaviour
{

    private Collider2D col;
    public GameObject Explosionclass;
  

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void SetHealth(Collider2D col)
    {
        SpaceShip player = col.GetComponent<SpaceShip>();
        PhotonView pv = col.GetComponent<PhotonView>();
        player.playerStats.Health -= 1;
        //Debug.Log(player.playerStats.Name + " " + player.playerStats.Health);
        player.hpBar1.GetComponent<Image>().fillAmount = (float)player.playerStats.Health / 10;
        //player.hpBar2.GetComponent<Image>().fillAmount = (float)player.playerStats.opponentHealth / 10;
        //pv.RPC("PlayerSoundTrigger", PhotonTargets.All, player.playerStats.name);
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        //if (col != null){
        //    if (stream.isWriting){
        //        Debug.Log("YES");
        //        stream.SendNext(col.GetComponent<SpaceShip>().playerStats.Health);
        //    } else {
        //        col.GetComponent<SpaceShip>().playerStats.Health = (int)stream.ReceiveNext();
        //    }

        //}
    }


    private void OnTriggerEnter2D(Collider2D col)
    {
        SpaceShip player = col.GetComponent<SpaceShip>();
        Debug.Log("Player: "+ col.tag + "Bullet: " + tag);

        if (((tag == "bullet2") && (col.tag == "Player1")) || ((tag == "bullet1") && (col.tag == "Player2")))
        {

            Debug.Log("aga");
            // PlayExplosion();
            SetHealth(col);
        }

        if (((tag == "bullet1") && (col.tag != "Player1")) || ((tag == "bullet2") && (col.tag != "Player2")))
        {
            if ((col.tag == "bullet1") && (tag == "bullet2") || (col.tag == "bullet2") && (tag == "bullet1"))
            {

            }
            else
            { 
               PlayExplosion();
               PhotonNetwork.Destroy(gameObject);
            }
        }
       

    }

    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosionclass);
        explosion.transform.position = transform.position;
    }
}

