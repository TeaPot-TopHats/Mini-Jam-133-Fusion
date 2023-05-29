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
        StateMachine.AnimationHandler.ChangeAnimationState("SlimeDead");
        GameObject.FindWithTag("Player").GetComponent<PlayerData>().AddScore(666);
        if(Random.Range(0, 4) == 0 )
            StateMachine.DropItem();
    }

    public void Exit()
    {
        timer = 0;
        StateMachine.Die();
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
