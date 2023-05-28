using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAttackingState : IState
{
    NPCStateMachine StateMachine;
    NPCStats Stats;
    
    public NPCAttackingState(NPCStateMachine StateMachine)
    {
        this.StateMachine = StateMachine;
        Stats = StateMachine.Stats;
    }

    public void Enter()
    {
        Debug.LogWarning("Enter Attack");
        //start attacking animation
        Stats.atkCoolDown = 0;

    }

    public void Exit()
    {
        
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        
    }

    public void AttackingPlayer()
    {
        if (StateMachine.PlayerInRange())
            Debug.Log("Call method from player to handle attack");
    }

    public void AttackEnded()
    {
        StateMachine.RevertState();
    }
}
