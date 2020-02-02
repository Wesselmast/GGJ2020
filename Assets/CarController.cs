using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController : MonoBehaviour
{
    Rigidbody rb;

    public float speed;
    public float accelaration;
    public float deaccelaration;
    public float maxSpeed;
    public float reverseMaxSpeed;
    public float maxBoostSpeed;
    private float currentTurnSpeed;

    public float turnSpeed;
    public float boostTurnSpeed;
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


        if (Input.GetAxis("Jump") > 0 || Input.GetAxis("Trigger") < 0 || Input.GetKeyDown(KeyCode.S)) {
            //backwards
            if (speed > -reverseMaxSpeed) {
                speed -= Time.fixedDeltaTime * accelaration * 10f;
            }
            
        } else if (Input.GetAxis("Trigger") > 0 || Input.GetKeyDown(KeyCode.Space)) {
            //boost
            speed += Time.fixedDeltaTime * accelaration + maxBoostSpeed * Time.fixedDeltaTime;
            if (currentTurnSpeed > boostTurnSpeed) {
                currentTurnSpeed -= Time.fixedDeltaTime * 3f;
            }
            if (currentTurnSpeed < boostTurnSpeed) {
                currentTurnSpeed += Time.fixedDeltaTime * 3f;
            }
            speed = Mathf.Clamp(speed, 0, maxSpeed + maxBoostSpeed);
        } else if (Input.GetAxis("Fire1") > 0 || Input.GetKeyDown(KeyCode.W)) {

            //forwards
            speed += Time.fixedDeltaTime * accelaration;
            if (speed > maxSpeed) {
                speed -= Time.fixedDeltaTime * deaccelaration;
            } else {
                speed += Time.fixedDeltaTime * accelaration;
            }
            if (currentTurnSpeed < turnSpeed) {
                currentTurnSpeed += Time.fixedDeltaTime * 3f;
            }
        } else {
            if (speed > 0) {
                speed -= Time.fixedDeltaTime * deaccelaration;
            } else if (speed < 0){
                speed += Time.fixedDeltaTime * deaccelaration;
            } else {
                rb.velocity = Vector3.zero;
            }
           if (currentTurnSpeed > 0) {
                currentTurnSpeed = speed/maxSpeed * turnSpeed;
            }
        } 
    }

    private void OnCollisionEnter(Collision collision) {
        //speed *= -bounceForce;
        rb.velocity = Vector3.zero;
    }

    private void OnCollisionExit(Collision collision) {
        rb.velocity = Vector3.zero;
    }
}
