using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PatientSpawner : MonoBehaviour {

    [SerializeField]
    private Sprite img = null;

    [SerializeField]
    private GameObject ragdoll;

    private GameObject instance;
    private bool active = false;

    private UIIcons icons;
    private SphereCollider col;

    private void OnEnable() {
        icons = Camera.main.GetComponent<UIIcons>();
        col = GetComponent<SphereCollider>();
    }

    public void Spawn() {
        instance = Instantiate(ragdoll, transform.position + new Vector3(0.0f, 2f, 0.0f), transform.rotation);
        active = true;
        icons.SetTarget(transform).SetTexture(img);
    }

    private void OnTriggerEnter(Collider other) {
        if (!active) return;
        if (other.tag == "Pawn") {
            PlayerQuestManager.Instance.OnGrabPatient();
            active = false;
            instance.SetActive(false);
        }
    }
}
