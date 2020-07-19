using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : CharacterAnimator
{
    public string VerticalSpeed = "VerticalSpeed";
    public string HorizontalSpeed = "HorizontalSpeed";
    public string isRunning = "isRunning";
    public string isFlying = "isFlying";
    public string jumpedTwice = "jumpedTwice";
    public override void setVerticalSpeed(float vy)
    {
        animator.SetFloat(VerticalSpeed, vy);
    }

    public override void setHorizontalSpeed(float vx)
    {
        animator.SetFloat(HorizontalSpeed, vx);
    }

    public override void setRunning(bool isRunning)
    {
        animator.SetBool(this.isRunning, isRunning);
    }

    public override void setFlying(bool isFlying)
    {
        animator.SetBool(this.isFlying, isFlying);
    }

    public override void setDoubleJump(bool jumpedTwice)
    {
        animator.SetBool(this.jumpedTwice, jumpedTwice);
    }
}
