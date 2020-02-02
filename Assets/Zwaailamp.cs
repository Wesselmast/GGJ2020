using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zwaailamp : MonoBehaviour
{
    public Color On;
    public Color Off;
    public float interval;
    public Material mat;
    public Material mat2;
    private bool on;
    private float t;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        t += Time.deltaTime;

        if(t > interval)
        {
            on = !on;
            t = 0;

            if (on)
            {
                mat.color = On;
                mat.SetColor("_EmissionColor", On);

                mat2.color = Off;
                mat2.SetColor("_EmissionColor", Off);
            }
            else
            {
                mat.color = Off;
                mat.SetColor("_EmissionColor", Off);

                mat2.color = On;
                mat2.SetColor("_EmissionColor", On);
            }

        }
    }
}
