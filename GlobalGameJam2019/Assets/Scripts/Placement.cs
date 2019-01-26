using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Placement : MonoBehaviour
{
    private Renderer r;
    public Color c;
    public bool placed = false;

    private void Start()
    {
        r = this.GetComponent<Renderer>();    
    }

    public void Place()
    {
           
        r.material.color = c;
        placed = true;
        
    }

    public float GetDistance(Vector3 playerpos)
    {
        return Mathf.Sqrt(Mathf.Pow((playerpos.x - transform.position.x),2) + Mathf.Sqrt(Mathf.Pow((playerpos.z - transform.position.z),2)));
    }
}
