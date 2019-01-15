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
   // public GameObject autobullet;
    float fireRate,nextFire;
    public Image hpBar;
    private Vector2 halfScreen;
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
        halfScreen = Camera.main.ViewportToWorldPoint(new Vector2(0, 0.5f));

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
                //if (r2d.transform.position.y > 1)
                //{
                //    r2d.MovePosition(new Vector2(touchPos.x - deltaX, 1));
                //    //r2d.velocity = Vector2.zero;

                //}
                //else
                //{

                r2d.MovePosition(new Vector2(touchPos.x - deltaX, touchPos.y - deltaY));
                //}

                break;
            case TouchPhase.Ended:
                r2d.velocity = Vector2.zero;
                break;
        }
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
                Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
                Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));
                
                if (transform.position.x > max.x)
                {
                    transform.SetPositionAndRotation(new Vector2(max.x,transform.position.y), transform.rotation);
                } else
                {
                    Control(touch, touchPos);
                }
                if (transform.position.x < min.x){
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
        else
        {
            Sync();
            
        }

        if (Time.time > nextFire)
        {

            Shoot();
           // Shoot1();
        }
      
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
    //auto shoot
    // void Shoot1()
    //{
    //    Instantiate(autobullet, PointBullet.transform.position, PointBullet.transform.rotation);
    //    nextFire = Time.time + fireRate;
    //}
}
