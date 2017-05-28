using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogHealth : EnemyHealth {

	// Use this for initialization
	void Start () {
        currentHealth = 30; 
	}
	
	// Update is called once per frame
	void Update () {
        invincibleTime -= Time.deltaTime;
        Die();
    }
}
