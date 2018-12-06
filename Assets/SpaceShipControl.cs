using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpaceShipControl : Photon.MonoBehaviour
{
    [SerializeField]
    public float deltaX, deltaY;
    Rigidbody2D r2d;
    public  GameObject bullet;
    public GameObject PointBullet;
    float fireRate,nextFire;
    public Image hpBar;

    private Vector3 realpos = Vector3.zero;
    float TimeShoot;

    public PlayerStats playerStats = new PlayerStats();

    public class PlayerStats
    {
        public string name { get; set; }
        public int hp { get; set; }
    }

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        fireRate = 2f;
        nextFire = Time.time;

        playerStats.hp = 10;
        playerStats.name = GetComponent<PhotonView>().viewID.ToString();
        gameObject.name = playerStats.name;
    }
    
    void Update()
    {
        //your ship cheker
        if (photonView.isMine && gameObject.tag == "Player")
        {
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

        }
        else
        {
            Sync();   
        }

        if (Time.time > nextFire) 
            Shoot();
    }

    //streams
    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
            stream.SendNext((string)playerStats.name);
            stream.SendNext((int)playerStats.hp);
        }
        else
        {
            realpos = (Vector3)stream.ReceiveNext();
            playerStats.name = (string)stream.ReceiveNext();
            playerStats.hp = (int)stream.ReceiveNext();
        }
    }

    void Sync()
    {
        if(gameObject.tag == "Player")
            transform.position = Vector3.Lerp(transform.position, realpos, 0.1f);
    }

    private void Shoot()
    {
        Instantiate(bullet, PointBullet.transform.position, PointBullet.transform.rotation);
        nextFire = Time.time + fireRate;        
    }
}
