using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCInjuredState : IState
{
    NPCStateMachine StateMachine;
    NPCStats Stats;
    float timer;
    public NPCInjuredState(NPCStateMachine StateMachine)
    {
        this.StateMachine = StateMachine;
        Stats = StateMachine.Stats;
    }
    public void Enter()
    {
        //change to injured sprite
    }

    public void Exit()
    {
        timer = 0;
    }

    public void FixedTick()
    {

    }

    public void Tick()
    {
        timer += Time.deltaTime;
        if (timer >= Stats.iFrames)
            StateMachine.RevertState();
    }
}
