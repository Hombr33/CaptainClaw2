using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

	public int currentHealth;
	public float invincibleTime;



    public void TakeDamage(int amount)
	{
		if (invincibleTime < 0f) 
		{
			currentHealth -= amount;
			invincibleTime = 0.3f;
		}

	}

	public void Die()
	{
		if (currentHealth <= 0) 
		{
			//anim.Play ("Death");
			Destroy(gameObject);
		}
	}


	// Update is called once per frame
	void Update () {
		invincibleTime -= Time.deltaTime;
		Die ();
	}
}
