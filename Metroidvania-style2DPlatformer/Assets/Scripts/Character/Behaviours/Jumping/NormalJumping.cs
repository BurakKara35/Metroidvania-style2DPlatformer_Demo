using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalJumping : JumpingBehaviour
{
    public void Jump(Rigidbody2D rigidbody, float speed)
    {
        rigidbody.AddForce(Vector2.up * speed * Time.fixedDeltaTime, ForceMode2D.Impulse);
    }
}
