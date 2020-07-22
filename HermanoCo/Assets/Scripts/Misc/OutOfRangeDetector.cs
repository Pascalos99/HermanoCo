using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class OutOfRangeDetector : MonoBehaviour
{
    public Camera mainView;
    public Transform target;
    public float tolerance = 0;

    private bool inRange = false;
    private bool hasTicked = false;
    private int resolution = 1;

    private void Start()
    {
        if (resolution < 0) resolution = 0;
        if (resolution > 30) resolution = 30;
    }

    void Update()
    {
        Vector3 pos = mainView.WorldToViewportPoint(target.position);
        if (pos.x < 0 || pos.x > 1 || pos.y < 0 || pos.y > 1) inRange = false;
        else inRange = true;
        if (!inRange || resolution == 0) { hasTicked = true;  return; }
        int count = 2 << resolution;
        double minAngle = 2*math.PI_DBL / count;
        for (int i=0; i < count; i++)
        {
            double angle = minAngle * i;
            Vector3 rot = new Vector3((float)math.cos(angle), (float)math.sin(angle), 0);
            Vector3 outpos = mainView.WorldToViewportPoint(target.position + tolerance * rot);
            if (outpos.x < 0 || outpos.x > 1 || outpos.y < 0 || outpos.y > 1)
            {
                inRange = false;
                break;
            }
        }
        hasTicked = true;
    }

    public bool InRange()
    {
        if (!hasTicked) Update();
        return inRange;
    }
}
