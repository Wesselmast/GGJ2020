using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerQuestManager : MonoBehaviour {

    private static PlayerQuestManager instance;
    public static PlayerQuestManager Instance { get { return instance; } }

    private PatientSpawner[] patientSpawners;
    private Transform player;
    private Hospital hospital;

    [SerializeField]
    private float startSpawnerDistance = 150;
    [SerializeField]
    private float spawnerDistanceIncrement = 100;

    private float spawnerDistance;

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }

    private void OnEnable() {
        WorldGenerator.OnLoadWorld += OnLoad;
    }

    private void OnDisable() {
        WorldGenerator.OnLoadWorld -= OnLoad;
    }

    private void Start() {
        player = GameObject.FindObjectOfType<CarController>().transform;
        spawnerDistance = startSpawnerDistance;
    }

    private void OnLoad() {
        Assign(spawnerDistance);
    }

    public void OnComplete() {
        spawnerDistance += spawnerDistanceIncrement;
        player.Find("BackSeat").gameObject.SetActive(false);
        FindObjectOfType<Timer>().AddTime(1.0f);
        Assign(spawnerDistance);
    }

    public void OnGrabPatient() {
        Hospital[] ps = Resources.FindObjectsOfTypeAll(typeof(Hospital)) as Hospital[];
        ps[0].setEnabled(true);
        FindObjectOfType<Timer>().AddTime(0.5f);
        player.Find("BackSeat").gameObject.SetActive(true);
        player.gameObject.GetComponent<Animator>().Play("AmbulanceAnimation");
    }

    private void Assign(float distance) {
        float smallest = float.MaxValue;
        PatientSpawner winner = null;
        foreach (PatientSpawner spawner in Resources.FindObjectsOfTypeAll(typeof(PatientSpawner))) {
            float dist = Vector3.Distance(spawner.transform.position, player.transform.position) - distance;
            if (dist < smallest && dist > 0.0f) {
                smallest = dist;
                winner = spawner;
            }
        }
        winner.Spawn();
    }
}
