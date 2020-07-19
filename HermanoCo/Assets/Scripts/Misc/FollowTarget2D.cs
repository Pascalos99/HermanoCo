using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Diagnostics;
using UnityEngine;

public class FollowTarget2D : MonoBehaviour
{
    public Transform target;
    public double followSpeed;
    public double minimumDistance;
    private double follow_speed;
    private double minimum_distance;

    private Vector2 targetPosition;
    private Vector2 currentPosition;

    private Stopwatch watch = new Stopwatch();

    // Start is called before the first frame update
    void Start()
    {
        follow_speed = followSpeed;
        minimum_distance = minimumDistance;
        if (followSpeed < 0) follow_speed *= -1;
        if (minimumDistance < 0) minimum_distance *= -1;
        watch.Start();
    }

    // Update is called once per frame
    void Update()
    {
        watch.Stop();
        targetPosition = new Vector2(target.position.x, target.position.y);
        currentPosition = new Vector2(transform.position.x, transform.position.y);
        Vector2 delta = targetPosition - currentPosition;
        float factor = (float)(follow_speed * watch.Elapsed.TotalSeconds);
        if (delta.magnitude < minimum_distance || delta.magnitude < factor) return;
        delta.Normalize();
        delta.Scale(new Vector2(factor, factor));
        currentPosition += delta;
        transform.position = new Vector3(currentPosition.x, currentPosition.y, transform.position.z);
        watch.Reset();
        watch.Start();
    }
}
