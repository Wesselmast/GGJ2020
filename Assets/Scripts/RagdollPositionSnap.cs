using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RagdollPositionSnap : MonoBehaviour
{

    public Transform snapPosition;
    public Transform targetTransform;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        targetTransform.position = snapPosition.position;
    }

    private void OnCollisionEnter(Collision collision) {
        Transform newTargetTransform = collision.transform.root.GetChild(0);
        if (newTargetTransform.tag == "Patient") {
            //newTargetTransform.GetComponent<Rigidbody>().isKinematic = true;
            if (targetTransform == null) {
                targetTransform = newTargetTransform;
                targetTransform.position = snapPosition.position;
                targetTransform.eulerAngles = new Vector3(90, 180, 90);
            } else if (newTargetTransform != targetTransform) {
                targetTransform = newTargetTransform;
                targetTransform.position = snapPosition.position;
            } else {
                return;
            }
            Debug.Log("patient");
        }
    }
}
