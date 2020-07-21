using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnableInRange : MonoBehaviour
{
    public OutOfRangeDetector detector;
    public GameObject ojectToEnable;
    public bool enableInRange = true;

    void Update()
    {
        ojectToEnable.SetActive(detector.InRange() ^ !enableInRange);
    }
}
