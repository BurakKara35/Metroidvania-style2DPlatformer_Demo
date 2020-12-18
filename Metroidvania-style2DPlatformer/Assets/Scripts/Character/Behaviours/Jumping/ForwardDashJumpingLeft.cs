using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardDashJumpingLeft : JumpingBehaviour
{
    public void Jump(Rigidbody2D rigidbody, float speed)
    {
        rigidbody.velocity = (-Vector2.right * speed * Time.fixedDeltaTime);
    }
}
