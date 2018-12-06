using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : Photon.MonoBehaviour {
    public int Damage;

	// Use this for initialization
	void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        gameObject.transform.Translate(Vector3.up * 0.1f);
        if (gameObject.transform.position.y > 7)
            Destroy(gameObject);
    }

    ////damage
    //void OnTriggerEnter2D(Collider2D other)
    //{
    //    //if (other.gameObject.tag == "Enemy")
    //    //{
    //    //    other.gameObject.GetComponent<Enemy>().Hp -= Damage;
    //    //    other.gameObject.GetComponent<Enemy>().HpBar.fillAmount = (float)other.gameObject.GetComponent<Enemy>().Hp / other.gameObject.GetComponent<Enemy>().StartHp;
    //    //}

    //    //if (photonView.isMine)
    //    //{  

    //    //}
    //    //else
    //    //{
    //    //    other.gameObject.GetComponent<Enemy>().Hp -= Damage;
    //    //    other.gameObject.GetComponent<Enemy>().HpBar.fillAmount = (float)other.gameObject.GetComponent<Enemy>().Hp / other.gameObject.GetComponent<Enemy>().StartHp;

    //    //}
    //}

}
