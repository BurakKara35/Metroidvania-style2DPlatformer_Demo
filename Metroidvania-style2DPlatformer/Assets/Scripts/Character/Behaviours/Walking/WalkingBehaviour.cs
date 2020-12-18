using System.Collections;
using System.Collections.Generic;
using UnityEngine;

interface WalkingBehaviour
{
    void Walk(float speed, Rigidbody2D rigidbody, float input);
}
