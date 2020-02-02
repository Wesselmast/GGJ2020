using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Helicopter : MonoBehaviour
{

    public Transform rotor;

    public float rotorSpeed;
    public float rotateSpeed;
    public float maxRotorSpeed;
    public float maxHeight;
    public float correctionStrenght;

    public float gravity;

    private float pitch;
    private float yaw;

    public float rotorStrenght;
    private Vector3 forward;

    private Rigidbody rb;

    private void Start()
    {

        rb = GetComponent<Rigidbody>();

    }

    void FixedUpdate()
    {
        float airDensity = 1 - transform.position.y / maxHeight;
        rotor.transform.rotation *= Quaternion.Euler(0,0, Mathf.Clamp(rotorStrenght,0,50));

        Vector3 gravityVector = new Vector3(0, -gravity, 0);

        rotorStrenght -= rotorSpeed * Time.deltaTime;

        if(rotorStrenght < 0)
        {
            rotorStrenght = 0;
        }

        if (rotorStrenght >= maxRotorSpeed)
        {
            rotorStrenght = maxRotorSpeed;
        }

        if (Input.GetAxisRaw("Trigger") != 0 || Input.GetAxisRaw("Jump") != 0)
        {
            rotorStrenght += rotorSpeed * 2 * Time.deltaTime;
            
        }

        Vector3 controllTorque = new Vector3(Input.GetAxisRaw("Vertical") * 2, 0, 0);
        Vector3 torque = controllTorque - (Vector3.up * -Input.GetAxisRaw("Horizontal") * rotateSpeed);

        rb.AddRelativeTorque(torque * rotateSpeed, ForceMode.Force);

        //currectivetorque
        float correctiveAngle = Vector3.SignedAngle(transform.up, Vector3.Cross(transform.forward, GetRight()) * Mathf.Sign(transform.up.y), transform.forward);
        float correctivePitchAngle = Vector3.Angle(Vector3.Cross(Vector3.up, GetRight()), transform.forward) / 90 - 1;

        rb.AddRelativeTorque(Mathf.Sign(correctiveAngle) * correctivePitchAngle * correctionStrenght * Vector3.forward);


        rb.AddRelativeForce(Vector3.up * rotorStrenght * airDensity,ForceMode.Force);
        rb.AddForce(Vector3.down * gravity);

        if (transform.position.y < 0.5f) {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }

    private Vector3 GetRight()
    {
        return Quaternion.Euler(0, transform.rotation.eulerAngles.y, 0) * Vector3.right;
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.relativeVelocity.magnitude > 25)
        {
            Destroy(gameObject);
        }
    }

}
