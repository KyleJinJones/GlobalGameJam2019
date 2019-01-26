using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public GameObject carried;
    public float scale;

    void Update()
    {
        if (carried != null)
        {
            Vector3 velocity = this.GetComponent<Rigidbody>().velocity;
            if (velocity.magnitude != 0)
            {
                Vector3 displacement = Quaternion.AngleAxis(90, new Vector3(0, 1, 0)) * (scale * velocity);
                carried.transform.position = this.transform.position + displacement / (displacement.magnitude/1.25f);
            }
            
        }
        
    }
}
