using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Placement : MonoBehaviour
{
    private Renderer r;
    public Color c;
    public Color hlcol;
    private Color def;
    public bool placed = false;
    private Collider cl;
    public string construct = "Wall";
    

    private void Start()
    {
        
        r = this.GetComponent<Renderer>();
        cl = GetComponent<Collider>();
        cl.enabled=false;
        def = r.material.color;
    }
    private void Update()
    {
        if (placed)
        {
            cl.enabled = true;
            r.material.color = c;
        }    
    }

    public void DefaultColor()
    {
        r.material.color = def;
    }
    public void Highlight()
    {
        r.material.color = hlcol;
    }

    public void Place()
    {
        
        r.material.color = c;
        placed = true;
        cl.enabled = true;
        cl.isTrigger = false;
        
    }

    public float GetDistance(Vector3 playerpos)
    {
        return Mathf.Sqrt(Mathf.Pow((playerpos.x - transform.position.x),2) + Mathf.Sqrt(Mathf.Pow((playerpos.z - transform.position.z),2)));
    }
}
