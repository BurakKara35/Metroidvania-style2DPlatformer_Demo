using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bob : CharacterAbstract
{
    JumpingBehaviour specialJumping = new GroundBreakerJumping();
    JumpingBehaviour normalJumping = new NormalJumping();
    WalkingBehaviour walkingBehaviour = new NormalWalking();

    CharacterBase character;

    private enum CharacterJumpBehaviour { None, FirstJump, Falling, SpecialJump }
    private CharacterJumpBehaviour characterJumpBehaviour;

    private IEnumerator jumpCoroutine;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    private void Update()
    {
        character.FlipByDirection();

        if (character.isOnGround)
        {
            if (character.JumpKeyPressed())
            {
                characterJumpBehaviour = CharacterJumpBehaviour.FirstJump;
                character.isOnGround = false;
            }
        }
        else
        {
            if (character.JumpKeyPressed() && (characterJumpBehaviour == CharacterJumpBehaviour.FirstJump || characterJumpBehaviour == CharacterJumpBehaviour.Falling))
            {
                characterJumpBehaviour = CharacterJumpBehaviour.SpecialJump;
                character.isPerformedSpecialJump = true;
            }
        }
    }

    private void FixedUpdate()
    {
        if (characterJumpBehaviour != CharacterJumpBehaviour.SpecialJump)
        {
            Walk();

            if (characterJumpBehaviour == CharacterJumpBehaviour.FirstJump)
            {
                Jump();
                jumpCoroutine = JumpingCoroutine();
                StartCoroutine(jumpCoroutine);
            }
        }
        else
        {
            StopCoroutine(jumpCoroutine);
            SpecialJump();
        }
    }

    public IEnumerator JumpingCoroutine()
    {
        yield return new WaitForSeconds(character.jumpingTimeInSeconds);
        if (!character.isPerformedSpecialJump)
            characterJumpBehaviour = CharacterJumpBehaviour.Falling;
    }

    protected override void Walk()
    {
        walkingBehaviour.Walk(character.walkingSpeed, character.rigidbody, character.HorizontalInput());
    }

    protected override void Jump()
    {
        normalJumping.Jump(character.rigidbody, character.jumpingSpeed);
    }

    protected override void SpecialJump()
    {
        specialJumping.Jump(character.rigidbody, character.specialJumpingSpeed);
    }

    protected override void Attack()
    {
        throw new System.NotImplementedException();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            characterJumpBehaviour = CharacterJumpBehaviour.None;
            character.isOnGround = true;
            character.isPerformedSpecialJump = false;
            StopAllCoroutines();
        }
        else
        {
            if (characterJumpBehaviour == CharacterJumpBehaviour.SpecialJump)
                characterJumpBehaviour = CharacterJumpBehaviour.None;
        }
    }
}
