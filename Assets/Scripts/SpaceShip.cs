
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

    float fireRate, nextFire;
    float tryConRate, nextCon;
    public GameObject hpBar1;
    public GameObject hpBar2;
    private Boolean isGameEnded = false;

    private Vector3 realpos = Vector3.zero;
    private int realHealth;
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

        fireRate = 0.8f;
        tryConRate = 10f;
        nextCon = Time.time + tryConRate;
        nextFire = Time.time;

        playerStats.Health = 10;
        playerStats.Name = GetComponent<PhotonView>().viewID.ToString();
        playerStats.Team = 1;

        gameObject.name = playerStats.Name;

        if (GetComponentInParent<SpriteRenderer>().sprite.ToString() == "Ship_B1 (UnityEngine.Sprite)") {
            hpBar1 = GameObject.Find("Bar1");
            hpBar2 = GameObject.Find("Bar2");
        } else {
            playerStats.Team = 2;
            hpBar1 = GameObject.Find("Bar2");
            hpBar2 = GameObject.Find("Bar1");
        }

        hpBar1.GetComponent<Image>().enabled = true; 
        hpBar2.GetComponent<Image>().enabled = true; 
    }
    
    void Update()
    {
        //your ship cheker
        if ((photonView.isMine))
        {
            // control
            if (Input.touchCount > 0)
            {
                if (Input.touchCount > 0)
                {
                    Touch touch = Input.GetTouch(0);
                    Vector2 touchPos = Camera.main.ScreenToWorldPoint(touch.position);
                    Vector2 min;
                    Vector2 max;

                    if (playerStats.Team == 1) {
                        min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                        max = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
                    } else {
                        max = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                        min = Camera.main.ViewportToWorldPoint(new Vector2(1, 0.5f));
                    }

                    // if (playerStats.Team == 1){
                    //     hpBar1.GetComponent<Image>().fillAmount = (float)playerStats.Health  / 10;
                    //     hpBar2.GetComponent<Image>().fillAmount = (float)realHealth  / 10;
                    // } else {
                    //     hpBar1.GetComponent<Image>().fillAmount = (float)realHealth  / 10;
                    //     hpBar2.GetComponent<Image>().fillAmount = (float)playerStats.Health  / 10;
                    // }

                    if (transform.position.x > max.x)
                    {
                        transform.SetPositionAndRotation(new Vector2(max.x, transform.position.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }
                    if (transform.position.x < min.x)
                    {
                        transform.SetPositionAndRotation(new Vector2(min.x, transform.position.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }
                    if (transform.position.y > max.y)
                    {
                        transform.SetPositionAndRotation(new Vector2(transform.position.x, max.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }
                    if (transform.position.y < min.y)
                    {
                        transform.SetPositionAndRotation(new Vector2(transform.position.x, min.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }


                    if ((transform.position.x > max.x) && (transform.position.y < min.y))
                    {
                        transform.SetPositionAndRotation(new Vector2(max.x, min.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }
                    if ((transform.position.x < min.x) && (transform.position.y < min.y))
                    {
                        transform.SetPositionAndRotation(new Vector2(min.x, min.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }
                    if ((transform.position.y > max.y) && (transform.position.x > max.x))
                    {
                        transform.SetPositionAndRotation(new Vector2(max.x, max.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }
                    if ((transform.position.y > max.y) && (transform.position.x < min.x))
                    {
                        transform.SetPositionAndRotation(new Vector2(min.x, max.y), transform.rotation);
                    }
                    else
                    {
                        Control(touch, touchPos);
                    }


                }
            }

            if (ConnectPhotonServer.isGameFinished)
            {
                FinishGame();
            }

            if ((PhotonNetwork.playerList.Length == 1) && (ConnectPhotonServer.isGameStarted) && (!isGameEnded))
            {
                FinishGame();
                isGameEnded = false;
            }

            // shooting
            if ((Time.time > nextFire) && (ConnectPhotonServer.isGameStarted) && (!ConnectPhotonServer.isGameFinished))
            {
                Shoot();
            }

            if ((Time.time > nextCon) && (PhotonNetwork.playerList.Length == 1) && (!ConnectPhotonServer.isGameStarted)) {
                PhotonNetwork.LeaveRoom();
                PhotonNetwork.JoinRandomRoom();
                nextCon = Time.time + tryConRate; 
            }
        }
        else
        {
            Sync();   
        }

        if (playerStats.Health == 0)
        {
            ConnectPhotonServer.isGameFinished = true;
        }

    }

    void FinishGame()
    {
        GameObject endWindow;
        isGameEnded = true;
        PhotonNetwork.DestroyAll();
        

        if (playerStats.Health <= 0)
        {
            if (photonView.isMine)
            {
                endWindow = GameObject.Find("YouLose");
                endWindow.GetComponent<Image>().enabled = true;
                foreach (var img in endWindow.GetComponentsInChildren<Image>())
                {
                    img.enabled = true;
                }
                foreach (var txt in endWindow.GetComponentsInChildren<Text>())
                {
                    txt.enabled = true;
                }
            }
        }
        else
        {
            endWindow = GameObject.Find("YouWin");
            endWindow.GetComponent<Image>().enabled = true;
            foreach (var img in endWindow.GetComponentsInChildren<Image>())
            {
                img.enabled = true;
            }
            foreach (var txt in endWindow.GetComponentsInChildren<Text>())
            {
                txt.enabled = true;
            }
        }

        playerStats.Health = 10;
        ConnectPhotonServer.isGameStarted = false;
        ConnectPhotonServer.isGameFinished = false;
        PhotonNetwork.Disconnect();
    }

    void Control(Touch touch, Vector2 touchPos)
    {

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
              //  r2d.velocity = Vector2.zero;
                break;
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
            transform.position = Vector3.Lerp(transform.position, realpos, 10f * Time.deltaTime);
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

    //auto shoot
    // void Shoot1()
    //{
    //    Instantiate(autobullet, PointBullet.transform.position, PointBullet.transform.rotation);
    //    nextFire = Time.time + fireRate;
    //}
}
