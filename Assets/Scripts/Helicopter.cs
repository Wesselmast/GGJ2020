using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helicopter : MonoBehaviour
{

    public Transform rotor;

    public float rotorSpeed;
    public float rotateSpeed;
    public float maxRotorSpeed;

    public float gravity;

    private float pitch;
    private float yaw;

    public float rotorStrenght;
    // Update is called once per frame
    void Update()
    {

        rotor.transform.rotation *= Quaternion.Euler(0, rotorStrenght, 0);

        Vector3 gravityVector = new Vector3(0, -gravity, 0);

        rotorStrenght -= rotorSpeed * Time.deltaTime;

        if(rotorStrenght < 0)
        {
            rotorStrenght = 0;
        }

        if (rotorStrenght > maxRotorSpeed)
        {
            rotorStrenght = maxRotorSpeed;
        }

        if (Input.GetAxisRaw("Jump") != 0)
        {
            rotorStrenght += rotorSpeed * 2 * Time.deltaTime;
            
        }

        pitch += rotateSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        yaw += rotateSpeed * Time.deltaTime * Input.GetAxisRaw("Horizontal");

        transform.rotation = Quaternion.AngleAxis(pitch, transform.right) * Quaternion.AngleAxis(yaw ,Vector3.up);

        transform.position += gravityVector * Time.deltaTime + transform.up * rotorStrenght * Time.deltaTime;

        if(transform.position.y < 0.5f)
        {
            transform.position = new Vector3(transform.position.x, 0.5f, transform.position.z);
        }
    }
}
