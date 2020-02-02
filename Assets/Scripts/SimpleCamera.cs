using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleCamera : MonoBehaviour
{
    public Transform target;
    public float distance;
    public float height;

    private float angle = 0;
    private Vector3 angleVelocity;

    private void FixedUpdate()
    {

        float targetAngle = target.eulerAngles.y * Mathf.Deg2Rad;
        Vector3 pos = target.position + new Vector3(Mathf.Sin(targetAngle), 0 , Mathf.Cos(targetAngle)) * distance + Vector3.up * height;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref angleVelocity, 0.2f);
        transform.LookAt(target);

    }
}
