using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExploseDestr : Photon.MonoBehaviour {

	void DestroyGameObject()
    {
        Destroy(gameObject);
    }
		
	
}
