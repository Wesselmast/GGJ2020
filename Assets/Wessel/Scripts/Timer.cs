using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField]
    private float startSeconds;

    [SerializeField]
    private Animator panelFade;

    private float time = float.MaxValue;

    private Text text;

    void Start() {
        WorldGenerator.OnLoadWorld += OnLoad;
        text = GetComponent<Text>();
        ResetTime();
    }

    void Update() {
        time -= Time.deltaTime;
        string min = Mathf.Floor(time / 60).ToString("00");
        string sec = (Mathf.Floor(time) % 60).ToString("00");
        text.text = min + ":" + sec;

        if (time <= 0.0f) {
            StartCoroutine(ExitLevel());
        }
    }

    private void OnLoad() {
        panelFade.speed = 0.1f;
        panelFade.Play("FadeOut");
    }

    IEnumerator ExitLevel() {
        panelFade.speed = 1.0f;
        panelFade.Play("FadeIn");
        yield return new WaitForSeconds(0.75f);
    }

    public void ResetTime() {
        time = startSeconds;
    }

    public void AddTime() {
        time += startSeconds;
    }
}
