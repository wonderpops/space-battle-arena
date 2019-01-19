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
    public Animator animFade;
    public Animator animBtn;
    public float targetTime;
    private Boolean wasPressed = false;



    // Use this for initialization
    void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (wasPressed)
            targetTime -= Time.deltaTime;

        if (targetTime <= 1.8f)
        {
            animFade.enabled = true;
            animBtn.enabled = true;
        }

        if (targetTime <= 0.0f)
        {
            timerEnded();
        }
    }

   public void Press () {
        wasPressed = true;
    }
    
    void timerEnded()
    {
        SceneManager.LoadScene("Battle", LoadSceneMode.Single);
    }
}
