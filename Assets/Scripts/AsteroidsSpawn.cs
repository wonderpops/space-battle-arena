using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidsSpawn : Photon.MonoBehaviour
{
    float fireRate, nextFire,wait;
    bool flag;
    // Use this for initialization
    void Start () {
        
        nextFire = Time.time;
    
    }

    void Update()
    {
        fireRate = Random.Range(8, 12);
        if ((Time.time > nextFire) && (ConnectPhotonServer.isGameStarted))
        {       
                PhotonNetwork.Instantiate("Asteroid", transform.position, transform.rotation, 0);
                nextFire = Time.time + fireRate;  
        }
    }
}
