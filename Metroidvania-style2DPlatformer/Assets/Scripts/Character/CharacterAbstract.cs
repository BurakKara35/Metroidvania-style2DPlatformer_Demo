using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAbstract : MonoBehaviour
{
    protected abstract void Walk();
    protected abstract void Jump();
    protected abstract void SpecialJump();
    protected abstract void Attack();

}
