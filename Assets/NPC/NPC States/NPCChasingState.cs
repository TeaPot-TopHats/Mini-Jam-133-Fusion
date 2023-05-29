using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCChasingState : IState
{
    NPCStateMachine StateMachine;
    NPCMovementHandler MovementHandler;
    NPCStats Stats;
    public NPCChasingState(NPCStateMachine StateMachine)
    {
        this.StateMachine = StateMachine;
        MovementHandler = StateMachine.MovementHandler;
        Stats = StateMachine.Stats;

    }

    public void Enter()
    {
        StateMachine.AnimationHandler.ChangeAnimationState("SlimeMove");
        MovementHandler.SetSpeed(Stats.speed);
        Debug.Log("Enter Chase");
    }

    public void Exit()
    {
        MovementHandler.SetSpeed(0);
    }

    public void FixedTick()
    {
        MovementHandler.Move();
        if (StateMachine.PlayerInRange())
            StateMachine.ChangeState(StateMachine.AttackingState);
    }

    public void Tick()
    {
        StateMachine.AtkCoolDownIteration();
    }

}
