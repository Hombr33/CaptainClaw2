using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBehaviour : MonoBehaviour {

    private EnemyHealth script;
    private PlayerHealth script1;


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            switch(other.name)
            {
                case "Dog":
                    script = other.GetComponent<DogHealth>();
                    script.TakeDamage(25);
                    break;
            } 
        }
        else if (other.tag == "Player")
        {
            script1 = other.GetComponent<PlayerHealth>();
            script1.TakeDamage(20);
        }

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
