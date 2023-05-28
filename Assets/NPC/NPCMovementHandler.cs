using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCMovementHandler
{
    private Rigidbody2D RB;
    private Vector2 Direction;
    private float speed;

    public NPCMovementHandler(Rigidbody2D RB)
    {
        this.RB = RB;
    }

    public void Move()
    {
        Debug.Log("Moving");
        Vector2 Force = speed * Time.deltaTime * Direction;
        RB.velocity = Force;
        Debug.Log("Velocity: " + RB.velocity);
        Debug.Log("Force: " + Force);
        Debug.Log("Speed: " + speed);
        Debug.Log("Time: " + Time.deltaTime);
        Debug.Log("Direction: " + Direction);
    }

    public void StopMoving()
    {
        RB.velocity = Vector2.zero;
    }

    public void SetDirection(Vector2 Direction)
    {
        this.Direction = Direction;
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
        if (speed == 0)
            StopMoving();
        
    }

}
