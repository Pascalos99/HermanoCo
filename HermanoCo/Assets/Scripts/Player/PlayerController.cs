using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public double groundMovementSpeed = 500;
    public double airMovementSpeed = 300;
    public double rotationSpeed = 200;
    public float jumpStrength = 400;
    public bool flightControl = false;
    public bool allowDoubleJump = false;
    public Transform groundCheck = null;
    public float groundCheckRadius = 0.75f;
    public CharacterAnimator animator;

    private Stopwatch watch;
    private Rigidbody2D rigid;

    public bool allowMovement
    {
        get
        {
            if (flightControl) return true;
            return isGrounded;
        }
    }

    public bool isFlying { get { return !isGrounded; } set { isGrounded = !value; } }

    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        watch = new Stopwatch();
        if (groundCheck == null) groundCheck = gameObject.transform;
        rigid = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        watch.Stop();
        double dt = watch.Elapsed.TotalSeconds;
        double movementSpeed = groundMovementSpeed;
        if (isFlying) movementSpeed = airMovementSpeed;
        bool isRunning = false;
        if (GlobalControls.GetKeyDown(GlobalControls.Jump)) Jump();
        if (GlobalControls.GetKeyDown(GlobalControls.Up)) Jump();
        if (GlobalControls.GetKey(GlobalControls.Left) && allowMovement) { AddForce(new Vector2((float)(-movementSpeed * dt), 0)); isRunning = true; }
        if (GlobalControls.GetKey(GlobalControls.Right) && allowMovement) { AddForce(new Vector2((float)(movementSpeed * dt), 0)); isRunning = true; }
        if (GlobalControls.GetKey(GlobalControls.RotateLeft) && allowMovement) AddRotation((float)(rotationSpeed * dt));
        if (GlobalControls.GetKey(GlobalControls.RotateRight) && allowMovement) AddRotation((float)(-rotationSpeed * dt));

        PlayerRotationCheck();

        ContactFilter2D filter = new ContactFilter2D();
        filter.SetLayerMask(1);
        filter.useLayerMask = true;
        RaycastHit2D[] results = new RaycastHit2D[10];
        int res = Physics2D.Raycast(new Vector2(groundCheck.position.x, groundCheck.position.y), -Vector2.up, filter, results, groundCheckRadius);
        if (res > 0) onGround();
        else onAir();

        float speedX = rigid.velocity.x;
        float speedY = rigid.velocity.y;
        animator.setHorizontalSpeed(Math.Abs(speedX));
        animator.setVerticalSpeed(speedY);
        animator.setRunning(isRunning && !isFlying);
        animator.setFlying(isFlying);
        animator.setDoubleJump(jumpedTwice);

        watch.Reset();
        watch.Start();
    }
    
    private void PlayerRotationCheck()
    {
        Quaternion q = transform.rotation;
        if (rigid.velocity.x > 0.01) q.eulerAngles = new Vector3(q.eulerAngles.x, 0, q.eulerAngles.z);
        if (rigid.velocity.x < -0.01) q.eulerAngles = new Vector3(q.eulerAngles.x, 180, q.eulerAngles.z);
        transform.rotation = q;
    }

    private void onGround()
    {
        isGrounded = true;
        jumpedTwice = false;
        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(q.eulerAngles.x, q.eulerAngles.y, 0);
        transform.rotation = q;
        rigid.constraints = RigidbodyConstraints2D.FreezeRotation;
    }
    private void onAir()
    {
        isGrounded = false;
        rigid.constraints = RigidbodyConstraints2D.None;
    }

    private bool jumpedTwice = false;
    private void Jump()
    {
        if (!isFlying || (allowDoubleJump && !jumpedTwice))
        {
            AddForce(new Vector2(0, jumpStrength));
            if (isFlying) jumpedTwice = true;
        }
    }

    private void AddForce(Vector2 force)
    {
        rigid.AddForce(force);
    }
    private void AddRotation(float torque)
    {
        rigid.AddTorque(torque);
    }
}
