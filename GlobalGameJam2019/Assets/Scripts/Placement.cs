using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Placement : MonoBehaviour
{
    private Renderer r;
    public Color c;
    private void Start()
    {
        r = this.GetComponent<Renderer>();    
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            r.material.color = c;
        }
    }
}
