using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Timer : MonoBehaviour {

    [SerializeField]
    private float startSeconds;

    [SerializeField]
    private Animator panelFade;

    [SerializeField]
    private AudioClip beep, beeeeeeeep;

    private float time = float.MaxValue;
    private float testast = 1.0f;

    private Text text;
    private AudioSource source;

    void Start() {
        WorldGenerator.OnLoadWorld += OnLoad;
        text = GetComponent<Text>();
        source = GetComponent<AudioSource>();
        ResetTime();
        source.clip = beep;
    }

    void Update() {
        testast -= Time.deltaTime;
        time -= Time.deltaTime;
        string min = Mathf.Floor(time / 60).ToString("00");
        string sec = (Mathf.Floor(time) % 60).ToString("00");
        text.text = min + ":" + sec;

        if (testast <= 0.0f) {
            source.Play();
            testast = 1.0f;
        }

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
        source.Stop();
        source.clip = beeeeeeeep;
        source.Play();
        yield return new WaitForSeconds(1.25f);
        panelFade.Play("FadeIn");
        yield return new WaitForSeconds(0.75f);
        UnityEngine.SceneManagement.SceneManager.LoadScene(0);
    }

    public void ResetTime() {
        time = startSeconds;
    }

    public void AddTime(float times = 1) {
        time += startSeconds * times;
    }
}
