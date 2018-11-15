using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SpaceShipControl : MonoBehaviour
{
    [SerializeField]
    public float deltaX, deltaY;
    Rigidbody2D r2d;
   public  GameObject bullet;
    float fireRate,nextFire;

    void Start()
    {
        r2d = GetComponent<Rigidbody2D>();
        fireRate = 1f;
        nextFire = Time.time;
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0) ;
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
        CheckIfTime();


    }
    void CheckIfTime()
    {
        if (Time.time> nextFire)
        {
            Instantiate(bullet, transform.position, Quaternion.identity);
            nextFire = Time.time + fireRate;
        }
    }
}
