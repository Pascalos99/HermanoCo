using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

public class KeyboardController2D : MonoBehaviour
{
    public double movementSpeed = 500;
    public double rotationSpeed = 200;
    public float jumpStrength = 400;
    public bool flightControl = false;
    public bool allowDoubleJump = false;
    public string floorTag = "Ground";

    private Stopwatch watch;

    public bool allowMovement
    {
        get
        {
            if (flightControl) return true;
            return isGrounded;
        }
    }

    public bool isFlying { get { return !isGrounded; } }

    private bool isGrounded = true;

    // Start is called before the first frame update
    void Start()
    {
        isGrounded = false;
        watch = new Stopwatch();
    }

    // Update is called once per frame
    void Update()
    {
        watch.Stop();
        double dt = watch.Elapsed.TotalSeconds;
        if (GlobalControls.GetKeyDown(GlobalControls.Jump)) Jump();
        if (GlobalControls.GetKeyDown(GlobalControls.Up)) Jump();
        if (GlobalControls.GetKey(GlobalControls.Left) && allowMovement) AddForce(new Vector2((float)(-movementSpeed * dt), 0));
        if (GlobalControls.GetKey(GlobalControls.Right) && allowMovement) AddForce(new Vector2((float)(movementSpeed * dt), 0));
        if (GlobalControls.GetKey(GlobalControls.RotateLeft) && allowMovement) AddRotation((float)(rotationSpeed * dt));
        if (GlobalControls.GetKey(GlobalControls.RotateRight) && allowMovement) AddRotation((float)(-rotationSpeed * dt));
        watch.Reset();
        watch.Start();
    }

    void OnCollisionEnter2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == floorTag)
        {
            isGrounded = true;
            jumpedTwice = false;
            // touched the ground now
        }
    }

    void OnCollisionExit2D(Collision2D theCollision)
    {
        if (theCollision.gameObject.tag == floorTag)
        {
            isGrounded = false;
            // lift off from the ground!
        }
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
        gameObject.GetComponent<Rigidbody2D>().AddForce(force);
    }
    private void AddRotation(float torque)
    {
        gameObject.GetComponent<Rigidbody2D>().AddTorque(torque);
    }
}
