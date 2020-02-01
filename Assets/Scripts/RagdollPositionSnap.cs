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
    void Update()
    {
        //targetTransform.position = snapPosition.position;
       //targetTransform.eulerAngles = new Vector3(90, 180, 90);
       if (targetTransform == null) {
            return;
        }
        if (Vector3.Distance(targetTransform.position, snapPosition.position) > 7f) {
            targetTransform.parent = null;
            targetTransform = null;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        Transform newTargetTransform = collision.transform.root.GetChild(0);
        if (newTargetTransform.tag == "Patient") {
            Debug.Log("Hit Patient");
            //newTargetTransform.GetComponent<Rigidbody>().isKinematic = true;
            if (targetTransform == null) {
                targetTransform = newTargetTransform;
                targetTransform.position = snapPosition.position;
                targetTransform.eulerAngles = new Vector3(90, 180, 90);
                targetTransform.root.SetParent(transform.root);
            }
                return;
            }
            Debug.Log("patient");
    }

    IEnumerator MoveToTarget() {
        yield return new WaitForSeconds(3);
    }
}
