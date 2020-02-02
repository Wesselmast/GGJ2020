using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float accelaration;
    public float maxSpeed;
    public float maxBoostSpeed;
    private float currentTurnSpeed;

    public float turnSpeed;
    [Range(0,1)]
    public float bounceForce;

    public Transform Chase;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
        currentTurnSpeed = turnSpeed;
    }

    void FixedUpdate()
    {


        rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
        rb.AddRelativeTorque(Vector3.up * Input.GetAxisRaw("Horizontal") * currentTurnSpeed);

        Quaternion targetRotation = Quaternion.Euler(0, 0, -Input.GetAxisRaw("Horizontal") * 7);
        Chase.localRotation = Quaternion.RotateTowards(Chase.localRotation, targetRotation, Time.deltaTime * 20f);

        if (Input.GetAxis("Trigger") > 0) {
            speed += Time.deltaTime * accelaration + maxBoostSpeed * Input.GetAxis("Trigger") * Time.deltaTime;
            if (currentTurnSpeed > turnSpeed * 0.5f) {
                currentTurnSpeed -= Time.deltaTime;
            }
        } else {
            speed -= Time.deltaTime * 4f;
            if (currentTurnSpeed < turnSpeed) {
                currentTurnSpeed += Time.deltaTime;
            }
        }
        
        speed = Mathf.Clamp(speed, maxSpeed, maxSpeed + maxBoostSpeed);

    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    speed *= -bounceForce;
    //}
}
