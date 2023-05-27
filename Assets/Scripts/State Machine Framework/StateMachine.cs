using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public IState CurrentState { get; private set; }
    public IState PreviousState { get; private set; }

    bool inTransition = false;

    public void ChangeState(IState NewState)
    {
        if (CurrentState == NewState || inTransition)
            return;

        ChangeStateRoutine(NewState);
    }

    public void RevertState()
    {
        if (PreviousState != null)
            ChangeState(PreviousState);
    }

    public void ChangeStateRoutine(IState NewState)
    {
        inTransition = true;

        if (CurrentState != null)
            CurrentState.Exit();

        if (PreviousState != null)
            PreviousState = CurrentState;

        CurrentState = NewState;

        if (CurrentState != null)
            CurrentState.Enter();

        inTransition = false;
    }

    public void Update()
    {
        if (CurrentState != null && !inTransition)
            CurrentState.Tick();
    }

    public void FixedUpdate()
    {
        if (CurrentState != null && !inTransition)
            CurrentState.FixedTick();
    }
}
