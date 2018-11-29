using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShipControl : Photon.MonoBehaviour
{
    [SerializeField]
    public float deltaX, deltaY;
    Rigidbody2D r2d;
   //public  GameObject bullet;
    float fireRate,nextFire;

    private Vector3 realpos = Vector3.zero;

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
       // CheckIfTime();
        
    }

    void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info) {
        //Vector3 pos = transform.position;
        //stream.Serialize(ref pos);
        //if (stream.isReading)
        //{
        //    transform.position = pos;
        //}
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

    //void CheckIfTime()
    //{
    //    if (Time.time> nextFire)
    //    {
    //        Instantiate(bullet, transform.position, Quaternion.identity);
    //        nextFire = Time.time + fireRate;
    //    }
    //}
}
