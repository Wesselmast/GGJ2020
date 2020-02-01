using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Brancard : MonoBehaviour
{
    public Transform pivot;
    public float followSpeed;

    private Vector3 velocity;
    float frictionTimer;

    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        Vector3 newPos = pivot.position;
        float dist = Vector3.Distance((newPos - Vector3.up * 6), transform.position);

        velocity += ((newPos - Vector3.up * 6) - transform.position).normalized * dist * 17f * Time.deltaTime;
        frictionTimer += Time.deltaTime;

        while (frictionTimer >= 0.05f)
        {
            velocity *= 0.9f;
            frictionTimer -= 0.05f;
        }

        transform.position = transform.position + velocity * Time.deltaTime * followSpeed;
        Vector3 lookPos = (newPos - transform.position);

        transform.rotation = Quaternion.LookRotation(Vector3.Cross(Vector3.right, lookPos), lookPos);
    }

    void OnCollisionEnter(Collision collision)
    {

        Debug.Log("henk");
        if (collision.relativeVelocity.magnitude > 2)
            Destroy(gameObject);
    }
}
