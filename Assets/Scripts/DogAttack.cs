using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DogAttack : MonoBehaviour {


    public GameObject sword;
    private GameObject Sword_clone;
    private GameObject[] swords;
   // private Animator anim;
    private float attackFreq = 4f;

    IEnumerator CleanerCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        swords = GameObject.FindGameObjectsWithTag("DogSword");
        foreach (GameObject sword in swords)
        {
            Destroy(sword);
        }

    }

    // Use this for initialization
    void Start () {
        //anim = GetComponent<Animator>();
    }

    public void Attack()
    {
        if (attackFreq <= 0f)
        {
            Sword_clone = (GameObject)Instantiate(sword, new Vector3(transform.position.x - 2.3f, transform.position.y + 1.0f, transform.position.z), Quaternion.identity);
            StartCoroutine(CleanerCoroutine());
            //anim.Play("MeleeDog");
            attackFreq = 4f;
        }
        
    }
	
	// Update is called once per frame
	void Update () {
        attackFreq -= Time.deltaTime;
	}
}
