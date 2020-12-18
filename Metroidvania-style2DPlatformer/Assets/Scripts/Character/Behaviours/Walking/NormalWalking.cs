using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalWalking : WalkingBehaviour
{
    public void Walk(float speed, Rigidbody2D rigidbody, float input)
    {
        rigidbody.velocity = Vector2.right * input * speed * Time.fixedDeltaTime;
    }
}
