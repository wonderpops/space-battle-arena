using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthSynchronization : Photon.MonoBehaviour {

	public Image bar;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	  void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting)
        {
            stream.SendNext((float)bar.fillAmount);
        }
        else
        {
            bar.fillAmount = (float)stream.ReceiveNext();
        }
    }

}
