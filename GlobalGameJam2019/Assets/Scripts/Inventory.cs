using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject carried;
    public float scale;
    public bool nothing = true;

    private AudioSource a;
    private Animator anim;

    private void Start()
    {
        a = GetComponent<AudioSource>();
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (carried != null)
        {
            anim.SetBool("Hold", true);

            Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
            if (velocity.magnitude != 0)
            {  
                Vector3 displacement = Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * (scale * velocity);
                carried.transform.position = this.transform.position + displacement / (displacement.magnitude/1.25f) + Vector3.up*2;
            }
            if (nothing)
            {
               
                a.Play();
                nothing = false;
            }
            carried.GetComponent<Collider>().enabled = false;
            
        }
        else
        {
            anim.SetBool("Hold", false);

            nothing = true;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log("test:" + other.tag);
        if ((other.tag == "material" || other.tag == "resource" )&& carried == null)
        {
            carried = other.gameObject;
            a.Play();
        }
    }
}
