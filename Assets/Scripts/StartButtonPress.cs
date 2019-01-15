using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class StartButtonPress : MonoBehaviour {

    private Button btn;

    private Animator anim;
    public Animator animbtn;
    public Animator animShip;
    public Animator animFade;
    public Button btn2;
    public float targetTime = 60.0f;



    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.enabled = true;
        animbtn.enabled = true;
        animShip.enabled = false;

        targetTime -= Time.deltaTime;
       

        if (targetTime <= 1.8f)
        {
            animFade.enabled = true;
        }

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }



    }

    void timerEnded()
    {
        SceneManager.LoadScene("battle", LoadSceneMode.Single);
    }
}
