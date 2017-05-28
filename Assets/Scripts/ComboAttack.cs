using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboAttack : MonoBehaviour {

    public GameObject sword;
    private GameObject Sword_clone;
    private GameObject[] swords;
    private Rigidbody2D rb;
    private Animator anim;
    private Movement movScript;


    public float[] attackTime;
    public float waitTime = 0.2f;
    private float attackTimer = 0f;
    public bool jumpAttack;
    private int attackType = -1;
    enum attack { Idle, Attacking, Waiting }
    private attack attackState;
    public int totalChainedHits = 0;

    IEnumerator CleanerCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        swords = GameObject.FindGameObjectsWithTag("Sword");
        foreach (GameObject sword in swords)
        {
            Destroy(sword);
        }
        
    }

	// Use this for initialization
	void Start () {
        jumpAttack = true;
        attackType = -1;
        attackState = attack.Idle;
        // tell animation to play Idle
        totalChainedHits = 0;
        anim = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        movScript = GetComponent<Movement>();
    }
	
	// Update is called once per frame
	void Update () {
        CheckForPlayerAttackInput();
        if (movScript.IsGrounded() && !jumpAttack)
            jumpAttack = true;
      
    }

    void CheckForPlayerAttackInput()
    {
        if (!movScript.IsGrounded() && Input.GetButtonDown("Fire1"))
        {
            if (jumpAttack)
            {
                anim.Play("MidAirSword");
                Sword_clone = (GameObject)Instantiate(sword, new Vector3(transform.position.x + 1.3f, transform.position.y, transform.position.z), Quaternion.identity);
                StartCoroutine(CleanerCoroutine());
                jumpAttack = false;
            }
            
        }
        else
        {
            switch (attackState)
            {
                case attack.Idle:
                    if (Input.GetButtonDown("Fire1")) // start attacking
                    {
                        attackType = 0;
                        attackState = attack.Attacking;
                        attackTimer = attackTime[0];
                        anim.Play("Melee04");
                        rb.velocity = new Vector2(0, rb.velocity.y);
                        Sword_clone = (GameObject)Instantiate(sword, new Vector3(transform.position.x + 1.3f, transform.position.y + 0.5f, transform.position.z), Quaternion.identity);
                        StartCoroutine(CleanerCoroutine());
                        totalChainedHits = 1;
                    }
                    break;

                case attack.Attacking:
                    attackTimer -= Time.deltaTime; // wait for attackTimer

                    if (attackTimer < 0)
                    {
                        attackTimer = waitTime;
                        attackState = attack.Waiting;
                    }

                    /* // uncomment this to break the combo while attacking
                    if ( Input.GetMouseButtonDown(0) ) // break attack
                    {
                           attackType = -1;
                           attackState = attack.Idle;
                           // tell animation to play Idle
                    }
                    */
                    break;

                case attack.Waiting:
                    attackTimer -= Time.deltaTime; // check the Waiting Timer (attackTimer)

                    if (attackTimer < 0) // ran out of time to chain combo
                    {
                        attackType = -1;
                        attackState = attack.Idle;
                        // tell animation to play Idle
                    }

                    // IMPLEMENT ATTACK RANGE OF EVERY COMBO SEQUENCE
                    if (Input.GetButtonDown("Fire1")) // continue attacking
                    {
                        attackType++; // go to next attack

                        if (attackType >= attackTime.Length) // check if the combo is over, start a new combo
                        {
                            attackType = 0;
                            attackState = attack.Attacking;
                            attackTimer = attackTime[0];
                            anim.Play("Melee04");
                            rb.velocity = new Vector2(0, rb.velocity.y);
                            Sword_clone = (GameObject)Instantiate(sword, new Vector2(transform.position.x + 1.3f, transform.position.y + 0.5f), Quaternion.identity);
                            StartCoroutine(CleanerCoroutine());
                            totalChainedHits++;
                        }
                        else
                        {
                            attackState = attack.Attacking;
                            attackTimer = attackTime[attackType];
                            if (attackType == 1)
                            {
                                anim.Play("Melee01");
                                rb.velocity = new Vector2(0, rb.velocity.y);
                                Sword_clone = (GameObject)Instantiate(sword, new Vector2(transform.position.x + 1.3f, transform.position.y + 0.5f), Quaternion.identity);
                                StartCoroutine(CleanerCoroutine());
                            }
                            else if (attackType == 2)
                            {
                                anim.Play("Melee02");
                                rb.velocity = new Vector2(0, rb.velocity.y);
                                Sword_clone = (GameObject)Instantiate(sword, new Vector2(transform.position.x + 1.3f, transform.position.y + 0.5f), Quaternion.identity);
                                StartCoroutine(CleanerCoroutine());
                            }
                            else if (attackType == 3)
                            {
                                anim.Play("Melee03");
                                rb.velocity = new Vector2(0, rb.velocity.y);
                                Sword_clone = (GameObject)Instantiate(sword, new Vector2(transform.position.x + 1.3f, transform.position.y + 0.5f), Quaternion.identity);
                                StartCoroutine(CleanerCoroutine());
                            }

                            totalChainedHits++;
                        }
                    }
                    break;


            }
        }
    }
}
