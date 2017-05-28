using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour {

	public int currentHealth;
	public int currentLives;
	public float invincibleTime;

	private Animator anim;


	public void TakeDamage(int amount)
	{
		if (invincibleTime < 0f) 
		{
			currentHealth -= amount;
			if (amount <= 20)
				anim.Play ("Low_hit");
			else
				anim.Play ("Big_hit");
			invincibleTime = 0.3f;
		}

	}

	public void Die()
	{
		if (currentHealth < 0) 
		{
			currentLives -= 1; 
			anim.Play ("Death"); 
			//Respawn and block movement code
		}
	}

	// Use this for initialization
	void Awake () {
		anim = GetComponent<Animator> ();
		currentHealth = 100;
		currentLives = 3;
		invincibleTime = 0.3f;
	}
	
	// Update is called once per frame
	void Update () {
		invincibleTime -= Time.deltaTime;
		Die ();
	}
}
