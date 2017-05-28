using UnityEngine;
using System.Collections;

public class StatePatternEnemy : MonoBehaviour
{
    public float searchingTurnSpeed = 120f;
    public float searchingDuration = 4f;
    public float sightRange = 20f;
    public Transform[] wayPoints;
    public Transform eyes;

    [HideInInspector]
    public Animator anim;
    [HideInInspector]
    public Transform chaseTarget;
    [HideInInspector]
    public IEnemyState currentState;
    [HideInInspector]
    public ChaseState chaseState;
    [HideInInspector]
    public PatrolState patrolState;

    private void Awake()
    {
        chaseState = new ChaseState(this);
        patrolState = new PatrolState(this);

    }

    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();
        currentState = patrolState;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        currentState.UpdateState();
    }

}