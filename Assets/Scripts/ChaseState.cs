using UnityEngine;
using System.Collections;

public class ChaseState : IEnemyState

{

    private readonly StatePatternEnemy enemy;
    private readonly DogAttack script;
    


    public ChaseState(StatePatternEnemy statePatternEnemy)
    {
        enemy = statePatternEnemy;
        script = enemy.GetComponent<DogAttack>();
    }

    public void UpdateState()
    {
        Chase();
    }

    public void ToPatrolState()
    {
        enemy.currentState = enemy.patrolState;
    }


    public void ToChaseState()
    {
        Debug.Log("Can't translate to same state");
    }

    

    private void Chase()
    {
        script.Attack();
    }


}