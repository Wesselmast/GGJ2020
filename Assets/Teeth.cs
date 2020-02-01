using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teeth : MonoBehaviour
{   
    public Transform teethParent;
    
    public Color heathyTooth;
    public Color rotTooth;

    private Renderer[] teethRenderers;
    private float[] t;

    void Start()
    {

        teethRenderers = new Renderer[teethParent.childCount];
        t = new float[teethParent.childCount];

        for (int i = 0; i < teethRenderers.Length; i++)
        {
            teethRenderers[i] = teethParent.GetChild(i).GetComponent<Renderer>();
        }
    }

    // Update is called once per frame
    void Update()
    {

        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit) && Input.GetMouseButton(0))
        {
            RepairTooth(hit.transform.gameObject);
        }

        t[Random.Range(0,teethRenderers.Length)] += Time.deltaTime;


        for (int i = 0; i < teethRenderers.Length; i++)
        {
            teethRenderers[i].material.color = Color.Lerp(heathyTooth, rotTooth, t[i]);
            t[i] += getNeighbours(i) * Time.deltaTime * 0.01f;
            t[i] = Mathf.Clamp01(t[i]);

            //if(t[i] > 0.98f)
            //{
            //    teethRenderers[i].gameObject.SetActive(false);
            //}
        }

    }

    private float getNeighbours(int index)
    {
        int i = index + 1;

        if(i > t.Length - 1)
        {
            i = 0;
        }

        int i2 = index - 1;

        if (i2 < 0)
        {
            i2 = t.Length - 1;
        }

        return t[i] + t[i2];
    }

    void RepairTooth(GameObject gameObject)
    {

        for (int i = 0; i < teethRenderers.Length; i++)
        {
            if(teethRenderers[i].gameObject == gameObject)
            {
                t[i] -= Time.deltaTime;
                t[i] = Mathf.Clamp01(t[i]);
            }
        }

    }

}
