using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIIcons : MonoBehaviour {

    [SerializeField]
    private Image img = null;
    [SerializeField]
    private Transform target = null;

    private Camera camera = null;

    private void Start() {
        camera = GetComponent<Camera>();
    }

    void Update() {
        float minX = img.GetPixelAdjustedRect().width / 2;
        float maxX = Screen.width - minX;

        float minY = img.GetPixelAdjustedRect().height / 2;
        float maxY = Screen.height  - minY;
        Vector2 pos = camera.WorldToScreenPoint(target.position);

        if (Vector3.Dot((target.position - transform.position), transform.forward) < 0) {
            pos.x = pos.x < Screen.width / 2 ? maxX : minX;
        }

        pos.x = Mathf.Clamp(pos.x, minX, maxX);
        pos.y = Mathf.Clamp(pos.y, minY, maxY);

        img.transform.position = pos;


    }
}
