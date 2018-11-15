using System.Collections;
using System.Collections.Generic;
using System.Timers;
using UnityEngine;
using UnityEngine.Experimental.UIElements;

public class startButtonPress : MonoBehaviour {

    private Button btn;

    private Animator anim;
    public Animator animbtn;
    public Animator animShip;
    

	// Use this for initialization
	void Start () {
        anim = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        anim.enabled = true;
        animbtn.enabled = true;
        animShip.SetInteger("State", 3);
        //animShip.SetInteger("State", 1);
        //       System.Timers.Timer aTimer = new System.Timers.Timer();
        //       aTimer.Elapsed+=new ElapsedEventHandler(OnTimedEvent);
        //       aTimer.Interval=1000;
        //       aTimer.Enabled=true;
        //}

        //   public void OnTimedEvent(object source, ElapsedEventArgs e){
        //       animShip.SetInteger("State", 2);
    }
}
