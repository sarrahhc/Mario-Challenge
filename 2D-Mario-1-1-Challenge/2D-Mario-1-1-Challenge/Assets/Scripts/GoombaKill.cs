using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHarm : MonoBehaviour {

    public GoombaController goomba;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void activate()
    {
        goomba.Killed();
    }
}
