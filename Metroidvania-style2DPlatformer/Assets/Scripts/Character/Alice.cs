using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alice : CharacterAbstract
{
    JumpingBehaviour specialJumpingLeft = new ForwardDashJumpingLeft();
    JumpingBehaviour specialJumpingRight = new ForwardDashJumpingRight();
    JumpingBehaviour normalJumping = new NormalJumping();
    WalkingBehaviour walkingBehaviour = new NormalWalking();

    CharacterBase character;

    float horizontalInput;

    private enum CharacterJumpBehaviour { None, FirstJump, FallingAfterFirstJump, SpecialJump, FallingAfterSpecialJump}
    private CharacterJumpBehaviour characterJumpBehaviour;

    private IEnumerator jumpCoroutine;
    private IEnumerator specialJumpCoroutine;

    private void Awake()
    {
        character = GetComponent<CharacterBase>();
    }

    private void Update()
    {
        horizontalInput = character.HorizontalInput();

        character.FlipByDirection();

        if(character.isOnGround)
        {
            if(character.JumpKeyPressed())
            {
                characterJumpBehaviour = CharacterJumpBehaviour.FirstJump;
                character.isOnGround = false;
            }
        }
        else
        {
            if (character.JumpKeyPressed() && (characterJumpBehaviour == CharacterJumpBehaviour.FirstJump || characterJumpBehaviour == CharacterJumpBehaviour.FallingAfterFirstJump))
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
            specialJumpCoroutine = SpecialJumpingCoroutine();
            StartCoroutine(specialJumpCoroutine);
        }
    }

    public IEnumerator JumpingCoroutine()
    {
        yield return new WaitForSeconds(character.jumpingTimeInSeconds);
        if(!character.isPerformedSpecialJump)
            characterJumpBehaviour = CharacterJumpBehaviour.FallingAfterFirstJump;
    }

    public IEnumerator SpecialJumpingCoroutine()
    {
        yield return new WaitForSeconds(character.specialJumpingTimeInSeconds);
        characterJumpBehaviour = CharacterJumpBehaviour.FallingAfterSpecialJump;
        character.OpenGravity();
        character.rigidbody.velocity = Vector2.zero;
    }

    protected override void Walk()
    {
        walkingBehaviour.Walk(character.walkingSpeed, character.rigidbody, horizontalInput);
    }

    protected override void Jump()
    {
        normalJumping.Jump(character.rigidbody, character.jumpingSpeed);
    }

    protected override void SpecialJump()
    {
        character.ZeroGravity();

        if (character.isFacingRight)
            specialJumpingRight.Jump(character.rigidbody, character.specialJumpingSpeed);
        else
            specialJumpingLeft.Jump(character.rigidbody, character.specialJumpingSpeed);
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
            {
                StopCoroutine(specialJumpCoroutine);
                characterJumpBehaviour = CharacterJumpBehaviour.FallingAfterSpecialJump;
            }
        }
    }

}
