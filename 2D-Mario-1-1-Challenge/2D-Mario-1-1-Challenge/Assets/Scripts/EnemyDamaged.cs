using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamaged : MonoBehaviour {

    public GoombaController goomba;

    void Start () {
		
	}
	
	void Update () {
		
	}

    public void activate()
    {
        goomba.Killed();
        gameObject.SetActive(false);
    }
}
