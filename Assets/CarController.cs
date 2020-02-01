using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float accelaration;
    public float maxSpeed;

    public float turnSpeed;
    [Range(0,1)]
    public float bounceForce;

    public Transform Chase;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    void FixedUpdate()
    {


        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        rb.AddRelativeTorque(Vector3.up * Input.GetAxisRaw("Horizontal") * turnSpeed);

        Quaternion targetRotation = Quaternion.Euler(0, 0, -Input.GetAxisRaw("Horizontal") * 7);
        Chase.localRotation = Quaternion.RotateTowards(Chase.localRotation, targetRotation, Time.deltaTime * 12.5f);

        speed += Time.deltaTime * accelaration;
        speed = Mathf.Clamp(speed, -maxSpeed, maxSpeed);

    }

    private void OnCollisionEnter(Collision collision)
    {
        speed *= -bounceForce;
    }
}
