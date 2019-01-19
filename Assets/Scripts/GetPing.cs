using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GetPing : Photon.MonoBehaviour
{

    public GameObject guiTextLink;
    int ping;

    void Start () {
       

    }

	void Update () {
        ping = PhotonNetwork.GetPing();
        guiTextLink.GetComponent<Text>().text = ping.ToString();
    }
}
