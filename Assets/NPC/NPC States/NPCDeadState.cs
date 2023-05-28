using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCDeadState : IState
{
    NPCStateMachine StateMachine;
    private float deathTime;
    private float timer;
    public NPCDeadState(NPCStateMachine StateMachine)
    {
        this.StateMachine = StateMachine;
        deathTime = StateMachine.Stats.deathTime;
        timer = 0;
    }

    public void Enter()
    {
        //change to dead sprite
    }

    public void Exit()
    {
        timer = 0;
        Debug.Log("Yo I'm dead, do something");
    }

    public void FixedTick()
    {

    }

    public void Tick()
    {
        if (timer < deathTime)
            timer += Time.deltaTime;
        else
            StateMachine.ChangeState(StateMachine.IdleState);
    }

}
