using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hospital : MonoBehaviour {
    private bool enabled = false;

    public void setEnabled(bool enabled) {
        this.enabled = enabled;
        if (enabled) {
            Camera.main.GetComponent<UIIcons>().SetTarget(transform);
        }
    }

    private  void OnTriggerEnter(Collider other) {
        if (!enabled) return;
        if (other.tag == "Pawn") {
            PlayerQuestManager.Instance.OnComplete();
            setEnabled(false);
        }
    }
}
