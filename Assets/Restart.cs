using UnityEngine;

public class Restart : MonoBehaviour {
    void Update() {
        if(Input.GetKeyUp(KeyCode.Escape)) {
            Application.Quit();
        }
    }
}
