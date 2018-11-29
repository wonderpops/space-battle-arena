using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShipControl : Photon.MonoBehaviour
{
    [SerializeField]
    public float deltaX, deltaY;
    Rigidbody2D r2d;
    public  GameObject bullet;
    public GameObject PointBullet;
    float fireRate,nextFire;

    private Vector3 realpos = Vector3.zero;
    float TimeShoot;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        fireRate = 1f;
        nextFire = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        //your ship cheker
        if (photonView.isMine)
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

        } else
        {
            Sync();   
        }

        if (Time.time > nextFire) 
            Shoot();     
        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        if (stream.isWriting)
        {
            stream.SendNext(transform.position);
        }
        else
        {
            realpos = (Vector3)stream.ReceiveNext();
        }
    }

    void Sync()
    {
        transform.position = Vector3.Lerp(transform.position, realpos, 0.1f);
    }

    private void Shoot()
    {
        Instantiate(bullet,PointBullet.transform);
        nextFire = Time.time + fireRate;        
    }
}
