using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class FollowTarget2D : MonoBehaviour
{
    public Transform target;
    public double followSpeed = 5;
    public double minimumDistance = 0;

    private Vector2 targetPosition;
    private Vector2 currentPosition;

    private Stopwatch watch = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        if (followSpeed < 0) followSpeed *= -1;
        if (minimumDistance < 0) minimumDistance *= -1;
        watch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        watch.Stop();
        targetPosition = new Vector2(target.position.x, target.position.y);
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 delta = targetPosition - currentPosition;
        float factor = (float)(followSpeed * watch.Elapsed.TotalSeconds);
        if (delta.magnitude < factor && minimumDistance == 0) transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        if (delta.magnitude < minimumDistance || delta.magnitude < factor) return;
        delta.Normalize();
        delta.Scale(new Vector2(factor, factor));
        currentPosition += delta;
        transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
        watch.Reset();
        watch.Start();
    }
}
