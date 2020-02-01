using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {
    [SerializeField]
    private GameObject[] extraTiles;

    [SerializeField]
    private GameObject[] patientTiles;

    [SerializeField]
    private GameObject homeTile;

    private void Start() {
        Instantiate(homeTile, transform);
    }

    private void Update() {

    }
}
