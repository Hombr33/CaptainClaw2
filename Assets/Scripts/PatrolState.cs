using UnityEngine;
using System.Collections;

public class PatrolState : IEnemyState

{
    private readonly StatePatternEnemy enemy;
    private int nextWayPoint = 1;
    private float patrolSpeed = 3f;
    private Rigidbody2D rb;
    private Transform wayPointPos;
    public bool facingRight = false;

    public PatrolState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
        facingRight = Flip(facingRight);
    }

    public void UpdateState()
    {
        Detected();
        Patrol();
    }

    public void ToPatrolState()
    {
        Debug.Log("Can't transition to same state");
    }

    public void ToChaseState()
    {
        enemy.currentState = enemy.chaseState;
    }

    bool Detected()
    {
        if (facingRight)
            return Physics2D.Raycast(enemy.eyes.transform.position, new Vector2(-1, 0), 0.5f); 
        else
            return Physics2D.Raycast(enemy.eyes.transform.position, new Vector2(1, 0), 0.5f);
    }

    public bool Flip(bool facingRight)
    {
        if (facingRight)
            enemy.transform.localScale = new Vector3(3, 3, 6);
        else enemy.transform.localScale = new Vector3(-3, 3, 6);

        facingRight = !facingRight;
        return facingRight;
    }

    void Patrol()
    {
        if (Detected())
        {
            ToChaseState();
        }

        rb = enemy.GetComponent<Rigidbody2D>();
        wayPointPos = enemy.wayPoints[nextWayPoint];
        if (Mathf.Abs(enemy.transform.position.x - wayPointPos.transform.position.x) <= 1.00f)
        {
            enemy.transform.localScale = new Vector3(-enemy.transform.localScale.x, 3, 6);
            nextWayPoint = (nextWayPoint + 1) % 2;
            wayPointPos = enemy.wayPoints[nextWayPoint];
            patrolSpeed = -patrolSpeed;
        }
        enemy.anim.Play("WalkingDog");
        rb.velocity = new Vector2(patrolSpeed, rb.velocity.y);

    }
}