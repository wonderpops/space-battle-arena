﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConnectPhotonServer : Photon.MonoBehaviour {

    public Animator fadeout;
    public Camera mainCam;
    public static bool isGameStarted { get; set; }
    public static bool isGameFinished { get; set; }

    void Start ()
    {
        // connect to photon cloud
        PhotonNetwork.ConnectUsingSettings("0.1");
        isGameStarted = false;
	}

    private void Update()
    {
        if (isGameStarted)
            fadeout.enabled = true;
    }

    void OnGUI()
    {
        // connect status info in top of the screen
        GUILayout.Label(PhotonNetwork.connectionStateDetailed.ToString());    
    }


    void OnJoinedLobby()
    {
        // when player join in lobby
        Debug.Log("OnJoinedLobby");

        PhotonNetwork.JoinRandomRoom();
    }

    void OnJoinedRoom()
    {
        // When player joined in room
        Debug.Log("OnJoinedRoom");

        Vector3 Player1StartPos = Vector3.zero;
        Vector3 Player2StartPos = Vector3.zero;
        Quaternion Player2Quart = Quaternion.Euler(180f, 180f, 0f);
    
        // Players spawn position set
        Player1StartPos.Set(0f, -2.5f, 0f);
        Player2StartPos.Set(0f, 4f, 0f);
        
        // Choose side and spawn player
        if (PhotonNetwork.playerList.Length == 1)
        {
            PhotonNetwork.Instantiate("Player1", Player1StartPos, Quaternion.identity, 0);
        } else
        {
            mainCam.transform.rotation = Player2Quart;
            PhotonNetwork.Instantiate("Player2", Player2StartPos, Player2Quart, 0);
            fadeout.enabled = true;
            isGameStarted = true;
        }
    }

    void OnPhotonRandomJoinFailed()
    {
        // Create room if join in random room failed
        Debug.Log("OnJoinRandomFailed");

        // Set room options
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 2;
       
        // create room 
        PhotonNetwork.CreateRoom(PhotonNetwork.countOfPlayers.ToString(), options, TypedLobby.Default);
    }
}
