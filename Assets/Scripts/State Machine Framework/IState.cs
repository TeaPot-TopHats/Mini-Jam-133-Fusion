using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IState
{
    //called when entering state
    void Enter();

    //acts as Update()
    void Tick();


    //acts as FixedUpdate()
    void FixedTick();

    //called when leaving state
    void Exit();
}
