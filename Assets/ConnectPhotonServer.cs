using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectPhotonServer : Photon.MonoBehaviour {

    public Animator fadeout;

    void Start ()
    {
        //connect to photon cloud
        PhotonNetwork.ConnectUsingSettings("0.1");
	}

    void OnGUI()
    {
        //connect status info in top of the screen
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());
        
        //if (!isSpawning)
        //{
        //    if (!PhotonNetwork.connected)
        //    {
               
        //    }
        //}
        //else if (PhotonNetwork.room == null)
        //{
        //    //PhotonNetwork.JoinLobby();
        //    //PhotonNetwork.CreateRoom("test", new RoomOptions(), TypedLobby.Default);
        //}
       
    }


    void OnJoinedLobby()
    {
        //when player join in lobby
        Debug.Log("OnJoinedLobby");

        PhotonNetwork.JoinRandomRoom();
    }

    void OnJoinedRoom()
    {
        //When player joined in room
        Debug.Log("OnJoinedRoom");

        Vector3 Player1StartPos = Vector3.zero;
        Vector3 Player2StartPos = Vector3.zero;
        Quaternion Player2Quart = Quaternion.Euler(180, 180, 0);
    
        //Players spawn position set
        Player1StartPos.Set(0f, -2.5f, -1f);
        Player2StartPos.Set(0f, 4f, -2f);
        
        //Choose side and spawn player
        if(PhotonNetwork.playerList.Length == 1)
        {
            PhotonNetwork.Instantiate("Player", Player1StartPos, Quaternion.identity, 0);
        } else
        {
            PhotonNetwork.Instantiate("Player", Player2StartPos, Player2Quart, 0);
        }

        //start fadeout animation
        fadeout.enabled = true;
       
    }

    void OnPhotonRandomJoinFailed()
    {
        //Create room if join in random room failed
        Debug.Log("OnJoinRandomFailed");

        //Set room options
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
       
        //create room
        PhotonNetwork.CreateRoom("Room1", options, TypedLobby.Default);
    }
}
