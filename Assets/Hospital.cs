using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hospital : MonoBehaviour {

    [SerializeField] private Sprite img;

    private bool enabled;

    private void Start() {
        enabled = false;
    }

    public void setEnabled(bool enabled) {
        this.enabled = enabled;
        if (enabled) {
            Camera.main.GetComponent<UIIcons>().SetTarget(transform).SetTexture(img);
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
