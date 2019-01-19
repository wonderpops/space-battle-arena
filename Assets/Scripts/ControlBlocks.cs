using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControlBlocks : MonoBehaviour {
    float blockRate,nextblock,wait;
    bool flag;
    // Use this for initialization
    void Start () {
        blockRate = 4f;
        nextblock = Time.time;
        wait = 5f;
        flag = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (flag && ConnectPhotonServer.isGameStarted)
        {
            wait = wait + Time.time;
            flag = false;
        }
        if ((Time.time > nextblock) && (ConnectPhotonServer.isGameStarted))
        {

            if (Time.time > wait)
            {
                hide();
                transform.GetChild(Random.Range(0, 4)).gameObject.SetActive(true);
                nextblock = Time.time + blockRate;
            }
        }

    }
    void hide()
    {
        for(int i=0;i<4;i++)
            transform.GetChild(i).gameObject.SetActive(false);
    }
}
