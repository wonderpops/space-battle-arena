using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonPress : MonoBehaviour {
	public Animator FadeOut;
	private Boolean wasPressed;
	public float targetTime;

		void Update () {
        if (wasPressed)
            targetTime -= Time.deltaTime;

        if (targetTime <= 1.8f)
        {
            FadeOut.enabled = true;
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
    	SceneManager.LoadScene("MenuScene_v3", LoadSceneMode.Single);
    }
}
