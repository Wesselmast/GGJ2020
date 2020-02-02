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

    public float heightDistMultiplier = 3f;
    public LayerMask collisionMask;

    private void FixedUpdate()
    {
        float currentDistance = Vector3.Distance(target.position, transform.position);
        float targetAngle = target.eulerAngles.y * Mathf.Deg2Rad;
        transform.LookAt(target);

        Vector3 cameraDestination = new Vector3(Mathf.Sin(targetAngle), 0, Mathf.Cos(targetAngle)) * distance + Vector3.up * height + -Vector3.up * distance / currentDistance * heightDistMultiplier;
        Vector3 pos = target.position + cameraDestination;
        float targetToCameraDistance = Vector3.Distance(pos, target.position);

        RaycastHit hit;
        Vector3 point;
        if (Physics.Raycast(target.position, transform.position - cameraDestination, out hit, targetToCameraDistance, collisionMask)) {
            point = hit.point;
            transform.position = point;
        } else {
            transform.position = Vector3.SmoothDamp(transform.position, pos, ref angleVelocity, 0.1f);
        }

    }

    private void OnDrawGizmos() {
        Gizmos.DrawLine(target.position, transform.position);
    }
}
