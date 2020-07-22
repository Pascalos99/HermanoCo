using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quickRotator : MonoBehaviour
{
    public float rotationSpeed = 50;

    private float dirX = 0;
    private float dirY = 0;
    private float dirZ = 0;

    void Update()
    {
        dirX = 0;
        dirY = dirX; dirZ = dirY;
        if (Input.GetKey(KeyCode.A)) dirX -= 1;
        if (Input.GetKey(KeyCode.D)) dirX += 1;
        if (Input.GetKey(KeyCode.S)) dirY -= 1;
        if (Input.GetKey(KeyCode.W)) dirY += 1;
        if (Input.GetKey(KeyCode.Q)) dirZ -= 1;
        if (Input.GetKey(KeyCode.E)) dirZ += 1;
    }

    void FixedUpdate()
    {

        Quaternion q = transform.rotation;
        q.eulerAngles = new Vector3(
            q.eulerAngles.x + dirX * Time.fixedDeltaTime * rotationSpeed,
            q.eulerAngles.y + dirY * Time.fixedDeltaTime * rotationSpeed,
            q.eulerAngles.z + dirZ * Time.fixedDeltaTime * rotationSpeed);
        transform.rotation = q;
    }
}
