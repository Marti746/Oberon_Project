using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositionFollower : MonoBehaviour
{
    //public Transform TargetTransform;
    public Vector3 Offset;

    public Transform TargetTransform;
    public float smoothTime = 0.3F;
    private Vector3 velocity = Vector3.zero;









    // Update is called once per frame
    void Update()
    {       





        //transform.position = TargetTransform.position + Offset;

        Vector3 targetPosition = TargetTransform.TransformPoint(new Vector3(0, 0, 0) + Offset);

        transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
    }
}
