using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClonePosition : MonoBehaviour
{
    public Transform target;
    public bool lockX;
    public bool lockY;
    public bool lockZ;

    void Update()
    {
        transform.position = new Vector3(
            lockX ? transform.position.x : target.transform.position.x,
            lockY ? transform.position.y : target.transform.position.y,
            lockZ ? transform.position.z : target.transform.position.z);
    }
}
