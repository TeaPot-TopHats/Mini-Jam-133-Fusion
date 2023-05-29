using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCAnimationHandler
{

    public Animator Animator;
    private string currentState;

    public NPCAnimationHandler(Animator Animator)
    {
        this.Animator = Animator;
    }

    public void ChangeAnimationState(string newState)
    {
        if (currentState == newState)
            return;
        Animator.Play(newState);
        currentState = newState;
    }
}
