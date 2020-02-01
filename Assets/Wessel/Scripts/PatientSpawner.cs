using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject ragdoll;

    private Transform player;
    private bool notDone = true;

    private void Start() {
        player = GameObject.FindObjectOfType<CarController>().transform;
    }

    private void Update() {
        float dist = Vector2.Distance(new Vector2(transform.position.x, transform.position.z), new Vector2(player.position.x, player.position.z));
        if (dist < 100 && notDone) {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.position + new Vector3(0.0f, -10000.0f, 0.0f), out hit)) {
                if (hit.transform.tag == "Building") {
                    transform.position += transform.forward;
                }
                else {
                    notDone = false;
                    Instantiate(ragdoll, hit.point, transform.rotation);
                }
            }
        }
    }
}
