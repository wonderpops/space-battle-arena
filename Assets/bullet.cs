using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : Photon.MonoBehaviour {

    public int Damage;
    private Vector3 realpos = Vector3.zero;

    // Use this for initialization
    void Start () {
		
	}

	// Update is called once per frame
	void Update () {
        // if (photonView.isMine)
        // {
        //    gameObject.transform.Translate(Vector3.up * 0.1f);
        //    if ((gameObject.transform.position.y > 7)||(gameObject.transform.position.y < -7))
        //        Destroy(gameObject);
        // }
        // else
        // {
        //    transform.position = Vector3.Lerp(transform.position, realpos, 0.1f);
        // }

        gameObject.transform.Translate(Vector3.up * Time.deltaTime * 5f);
        if ((gameObject.transform.position.y > 7) || (gameObject.transform.position.y < -7))
            Destroy(gameObject);
        Debug.Log(gameObject.transform.position.x+" "+ gameObject.transform.position.y);
    }

    // void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    // {
    //     Vector3 pos = transform.position;
    //     stream.Serialize(ref pos);
    // }

}
