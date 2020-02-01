using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject ragdoll;

    private GameObject instance;
    private bool active = false;

<<<<<<< HEAD
    public void Spawn() {
        instance = Instantiate(ragdoll, transform.position, transform.rotation);
        Camera.main.GetComponent<UIIcons>().SetTarget(transform); 
        Debug.Log("DONE");
        active = true;
=======
    private void Start() {
        player = GameObject.FindObjectOfType<CarController>().transform;
>>>>>>> 1e4336d5f547cbc902556e55bbb3aa997431aa61
    }

    private void OnTriggerEnter(Collider other) {
        if (!active) return;
        if (other.tag == "Pawn") {
            PlayerQuestManager.Instance.OnGrabPatient();
        }
    }
}
