using UnityEngine;

public class BuildingGenerator : MonoBehaviour {
    public int minPieces = 5;
    public int maxPieces = 20;
    public GameObject[] baseParts;
    public GameObject[] middleParts;
    public GameObject[] topParts;

    void Start() {
        Build();
    }

    private void OnDrawGizmos() {
        Gizmos.matrix = Matrix4x4.TRS(transform.position, transform.rotation, transform.lossyScale);
        Gizmos.color = new Color(.5f, .5f, .25f);
        Gizmos.DrawCube(new Vector3(-0.5f, 0.1f, -0.5f), new Vector3(1, 0.1f, 1));
        Gizmos.color = new Color(1.0f, 0.0f, 0.0f);
        Gizmos.DrawLine(new Vector3(-0.5f, 0.1f, -0.5f), new Vector3(-0.5f, 0.1f, -0.5f) + Vector3.right);
        Gizmos.DrawCube(new Vector3(-0.5f, 0.1f, -0.5f) + Vector3.right, Vector3.one * 0.1f);
    }

    void Build() {
        int targetPieces = Random.Range(minPieces, maxPieces);
        float heightOffset = 0;
        heightOffset += SpawnPieceLayer(baseParts, heightOffset);


        for (int i = 2; i < targetPieces; i++) {
            heightOffset += SpawnPieceLayer(middleParts, heightOffset);
        }

        heightOffset += SpawnPieceLayer(topParts, heightOffset);

        GetComponent<BoxCollider>().center = new Vector3(-0.5f, heightOffset / 20, -0.5f);
        GetComponent<BoxCollider>().size   = new Vector3(1, heightOffset / 10, 1);
    }

    float SpawnPieceLayer(GameObject[] pieceArray, float inputHeight) {
        Transform randomTransform = pieceArray[Random.Range(0, pieceArray.Length)].transform;
        GameObject clone = Instantiate(randomTransform.gameObject, this.transform.position
            + new Vector3(0, inputHeight, 0), transform.rotation) as GameObject;
        
        Mesh cloneMesh = clone.GetComponentInChildren<MeshFilter>().sharedMesh;
        Bounds bounds = cloneMesh.bounds;

        clone.transform.SetParent(this.transform);
        clone.transform.localScale = transform.localScale;

        float heightOffset = bounds.size.y * transform.localScale.y * 10;
        return heightOffset;
    }

}
