using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterAnimator : MonoBehaviour
{
    public Animator animator;

    public abstract void setVerticalSpeed(float vy);

    public abstract void setHorizontalSpeed(float vx);

    public abstract void setRunning(bool isRunning);

    public abstract void setFlying(bool isFlying);

    public abstract void setDoubleJump(bool jumpedTwice);

}
