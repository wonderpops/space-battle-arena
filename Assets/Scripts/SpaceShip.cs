
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShip : Photon.MonoBehaviour
{
    [SerializeField]
    public float deltaX, deltaY;
    Rigidbody2D r2d;
    public  GameObject bullet;
    public GameObject PointBullet;
    float fireRate,nextFire;
    public GameObject hpBar1;
    public GameObject hpBar2;

    private Vector3 realpos = Vector3.zero;
    float TimeShoot;

    public PlayerStats playerStats = new PlayerStats();


    public class PlayerStats
    {
        public string Name { get; set; }
        public int Team { get; set; }
        public int Health { get; set; }
    }

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        fireRate = 2f;
        nextFire = Time.time;

        playerStats.Health = 10;
        playerStats.Name = GetComponent<PhotonView>().viewID.ToString();
        playerStats.Team = 1;

        gameObject.name = playerStats.Name;

        hpBar1 = GameObject.Find("Bar1");
        hpBar2 = GameObject.Find("Bar2");

        if (ConnectPhotonServer.isGameStarted){
            if (!hpBar1.GetComponent<Image>().enabled) {
                playerStats.Team = 2;
                hpBar1 = GameObject.Find("Bar2");
                hpBar1.GetComponent<Image>().enabled = true;
                hpBar2 = GameObject.Find("Bar1");
                hpBar2.GetComponent<Image>().enabled = true;
            } else {
                hpBar1.GetComponent<Image>().enabled = true; 
                hpBar2.GetComponent<Image>().enabled = true;     
            }
        }
    }
    
    void Update()
    {
        //your ship cheker
        if ((photonView.isMine))
        {   
            // control
            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                switch (touch.phase)
                {
                    case TouchPhase.Began:
                        deltaX = touchPos.x - +transform.position.x;
                        deltaY = touchPos.y - +transform.position.y;
                        break;
                    case TouchPhase.Moved:
                        r2d.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                        break;
                    case TouchPhase.Ended:
                        r2d.velocity = Vector2.zero;
                        break;
                }
            }
            
            // shooting
            if ((Time.time > nextFire) && (ConnectPhotonServer.isGameStarted) && (!ConnectPhotonServer.isGameFinished))
            {
                Shoot();
            }
        }
        else
        {
            Sync();   
        }

        if (playerStats.Health == 0){
           ConnectPhotonServer.isGameFinished = true; 
        } 
        
    }

    //streams
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext(playerStats.Health);
            stream.SendNext((bool)ConnectPhotonServer.isGameStarted);
            stream.SendNext((bool)ConnectPhotonServer.isGameFinished);
        }
        else
        {
            realpos = (Vector3)stream.ReceiveNext();
            playerStats.Health = (int)stream.ReceiveNext();
            if (!ConnectPhotonServer.isGameStarted)
                ConnectPhotonServer.isGameStarted = (bool)stream.ReceiveNext();
            if (ConnectPhotonServer.isGameFinished)
                ConnectPhotonServer.isGameFinished = (bool)stream.ReceiveNext();
        }
    }

    void Sync()
    {
        if(gameObject.tag == "Player")
            transform.position = Vector3.Lerp(transform.position, realpos, 5f * Time.deltaTime);
    }

    private void Shoot()
    {
        if (playerStats.Team == 1){
            PhotonNetwork.Instantiate("Bullet1", PointBullet.transform.position, PointBullet.transform.rotation, 0);
        } else {
            PhotonNetwork.Instantiate("Bullet2", PointBullet.transform.position, PointBullet.transform.rotation, 0); 
        }
        nextFire = Time.time + fireRate;       
    }
}
