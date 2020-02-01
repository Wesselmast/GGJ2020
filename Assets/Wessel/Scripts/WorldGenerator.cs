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

    [SerializeField]
    private Transform player;

    public  float loadingDistance = 100;

    private GameObject[] spawnedTiles;

    private float timer;

    private static WorldGenerator instance;
    public static WorldGenerator Instance { get { return instance; } }

    private void Awake() {
        if (instance != null && instance != this) {
            Destroy(gameObject);
        }
        else {
            instance = this;
        }
    }

    private void Start() {
        Spawn(homeTile, 0, 0);

        int worldSize = 15;

        for (int x = -worldSize; x < worldSize + 1; x++) {
            for (int y = -worldSize; y < worldSize + 1; y++) {
                if (x == 0 && y == 0) continue;
                Spawn(extraTiles[Random.Range(0, extraTiles.Length)], x, y);
            }
        }
    }

    void Spawn(GameObject tile, int xPos, int zPos) {
        Vector3 scaledPos = new Vector3(xPos, 0.0f, zPos) * 100;
        GameObject instance = Instantiate(tile, transform.position + scaledPos, transform.rotation) as GameObject;
        instance.transform.SetParent(transform);
        instance.SetActive(false);
    }

    private void Update() {
        if (timer >= .75f) {
            foreach (Transform child in transform) {
                float dist = Vector2.Distance(new Vector2(child.position.x, child.position.z), new Vector2(player.position.x, player.position.z));
                child.gameObject.SetActive(dist < loadingDistance);
            }
            timer = 0;
        }
        else {
            timer += Time.deltaTime;
        }
    }
}
