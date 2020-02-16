using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour {

    [SerializeField]
    private Animator panel;

    private void Start() {
        panel.Play("FadeIn");
    }

    void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            StartCoroutine(awdwd());
        }
    }

    IEnumerator awdwd() {
        panel.Play("FadeOut");
        yield return new WaitForSeconds(0.75f);
        
    }
}
