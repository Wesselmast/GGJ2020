using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintBounds : MonoBehaviour {
    void Start() {
        Debug.Log(GetComponentInChildren<MeshFilter>().sharedMesh.bounds.size.y);
    }
}
