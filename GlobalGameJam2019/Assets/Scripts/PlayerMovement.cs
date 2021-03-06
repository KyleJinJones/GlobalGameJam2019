﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody rb;
    [SerializeField] private float speed = 5.0f;
    [SerializeField] private float Maxspeed = 10.0f;
    [SerializeField] private float Minspeed = 0f;
    [SerializeField] private float accerleration = 1.5f;
 
    private float x_dir = 0;
    private float z_dir = 0;
    private float pre_x_dir = 0;
    private float pre_z_dir = 0;

    private float angle=0;
    private float lastangle=0;
    public string HorzAxis = "P1_H";
    public string VerticalAxis = "P1_V";
    public string InteractAxis;

    void Start()
    {
        rb=GetComponent<Rigidbody>();
    }
    
    // Update is called once per frame
    void Update()
    {
        //transform.Rotate(new Vector3(0, -angle, 0));
        x_dir = Input.GetAxisRaw(HorzAxis);
        z_dir = Input.GetAxisRaw(VerticalAxis);
        if (x_dir != 0 || z_dir != 0)
        {
            angle = Vector3.Angle(Vector3.Normalize(new Vector3(Input.GetAxis(HorzAxis), 0, Input.GetAxis(VerticalAxis))), Vector3.forward);
            if (x_dir < 0) { angle = -angle; }


            transform.Rotate(new Vector3(0, angle - lastangle, 0));
            lastangle = angle;
        }
    }

    private void FixedUpdate()
    {
        if (x_dir != 0 || z_dir != 0)
        {
            Minspeed = Approach(Minspeed, Maxspeed, accerleration);
        }
        else
        {
            Minspeed = 0;
        }

        speed = Minspeed;
        rb.velocity = Vector3.Normalize(new Vector3(x_dir, 0, z_dir)) * speed;
        
    }

    public float Approach(float a, float b, float c)
    {
        if (a < b)
        {
            a += c;
            if (a > b)
                return b;
        }
        else
        {
            a -= c;
            if (a < b)
                return b;
        }
        return a;

    }
    
    public float Sign(float a)
    {
        if(a != 0)
        {
            if(a > 0)
            {
                return 1;
            }

            if (a < 0)
            {
                return -1;
            }

        }
        
        return  0;
    }
}

