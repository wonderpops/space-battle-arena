using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour {
    [Header("Атрибуты противника")]
    public int StartHp;
    public int Hp;
    public Image HpBar;
	// Use this for initialization
	void Start () {
        Hp = StartHp;
	}
	
	// Update is called once per frame
	void Update () {
        if (Hp <= 0)
           Destroy(gameObject);
	}
}
