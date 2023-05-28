using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//handles the states of the npc
public class NPCStateMachine : StateMachine
{
    //list of states
    public NPCIdleState IdleState { get; private set; }
    public NPCChasingState ChasingState { get; private set; }
    public NPCDeadState DeadState { get; private set; }
    public NPCAttackingState AttackingState { get; private set; }
    public NPCInjuredState InjuredState { get; private set; }

    public NPCMovementHandler MovementHandler;
    public NPCStats Stats;
    public NPCPathFinding Pathfinder;
    [SerializeField] Rigidbody2D RB;

    [SerializeField] Transform RangeTransformer;

    private void Awake()
    {
        MovementHandler = new NPCMovementHandler(RB);
        Pathfinder.SetMovementHandler(MovementHandler);

        IdleState = new NPCIdleState(this);
        ChasingState = new NPCChasingState(this);
        DeadState = new NPCDeadState(this);
        AttackingState = new NPCAttackingState(this);//Needs work
        InjuredState = new NPCInjuredState(this);

    }

    private void OnEnable()
    {
        
    }

    private void OnDisable()
    {
        
    }

    void Start()
    {
        ChangeState(IdleState);
    }


    public void Hurt(int atk)
    {
        if (atk - Stats.defense > 0)
            Stats.health -= (atk - Stats.defense);

        if (Stats.health <= 0)
            ChangeState(DeadState);
        else
            ChangeState(InjuredState);
    }

    public bool PlayerInRange()
    {
        if(Pathfinder.distanceFromPlayer < Stats.range)
            return true;
        return false;
    }

    public void AtkCoolDownIteration()
    {
        if (Stats.atkCoolDown < Stats.StartingStat.initAtkCoolDown)
            Stats.atkCoolDown += Time.deltaTime;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(RangeTransformer.position, Stats.range);
    }
}
