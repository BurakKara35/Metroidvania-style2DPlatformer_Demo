using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterBase : MonoBehaviour
{
    InputHandler inputHandler;

    public float walkingSpeed;
    public float jumpingSpeed;
    public float specialJumpingSpeed;
    public float jumpingTimeInSeconds;
    public float specialJumpingTimeInSeconds;

    float gravityScale;

    [HideInInspector] public bool isOnGround = true;
    [HideInInspector] public bool isFacingRight = true;
    [HideInInspector] public bool isPerformedSpecialJump = false;

    [HideInInspector] public Rigidbody2D rigidbody;

    private void Awake()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        inputHandler = GameObject.FindGameObjectWithTag("InputController").GetComponent<InputHandler>();
        gravityScale = rigidbody.gravityScale;
    }

    public float HorizontalInput()
    {
        return inputHandler.GetHorizontalInput();
    }

    public bool JumpKeyPressed()
    {
        return inputHandler.GetJumpInput();
    }

    public void FlipByDirection()
    {
        if (HorizontalInput() < 0)
        {
            transform.eulerAngles = new Vector3(0, 180, 0);
            isFacingRight = false;
        }
        else if (HorizontalInput() > 0)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
            isFacingRight = true;
        }
    }

    public void ZeroGravity()
    {
        rigidbody.gravityScale = 0;
    }

    public void OpenGravity()
    {
        rigidbody.gravityScale = gravityScale;
    }
}
