using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(0, 0, Random.Range(-50.0f, 50.0f));
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Mathf.Abs(rb.velocity.x) <= 5.0f && Mathf.Abs(rb.velocity.z) <= 5.0f)
            rb.velocity = new Vector3(0, 0, Random.Range(-50.0f, 50.0f));
    }
}
