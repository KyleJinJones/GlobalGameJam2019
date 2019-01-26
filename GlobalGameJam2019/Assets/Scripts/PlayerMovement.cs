using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5.0f;
    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        rb.velocity = Vector3.Normalize(new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"))) * speed;
    }
   

    private void OnTriggerStay(Collider other)
    {
        
    }
}
