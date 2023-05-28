using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCIdleState : IState
{
    NPCStateMachine StateMachine;
    float delayedStart;
    public NPCIdleState(NPCStateMachine StateMachine)
    {
        this.StateMachine = StateMachine;
        delayedStart = 1;
    }

    public void Enter()
    {
        //set to idle animation
        Debug.Log("Enter Idle");
    }

    public void Exit()
    {
        delayedStart = 1;
    }

    public void FixedTick()
    {
        
    }

    public void Tick()
    {
        delayedStart -= Time.deltaTime;
        if(delayedStart <= 0)
            StateMachine.ChangeState(StateMachine.ChasingState);
        StateMachine.AtkCoolDownIteration();

    }

}
