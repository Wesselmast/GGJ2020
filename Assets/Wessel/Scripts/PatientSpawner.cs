using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatientSpawner : MonoBehaviour {

    [SerializeField]
    private GameObject ragdoll;

    private GameObject instance;
    private bool active = false;

    public void Spawn() {
        instance = Instantiate(ragdoll, transform.position, transform.rotation);
        Camera.main.GetComponent<UIIcons>().SetTarget(transform); 
        Debug.Log("DONE");
        active = true;
    }

    private void OnTriggerEnter(Collider other) {
        if (!active) return;
        if (other.tag == "Pawn") {
            PlayerQuestManager.Instance.OnGrabPatient();
        }
    }
}
