﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisposeInventory : MonoBehaviour
{
    // Start is called before the first frame update

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerStay(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if(Input.GetKeyDown(KeyCode.E)) {
            if(inventory.carried == null) {
                Debug.Log("CANNOT THROW ITEM; NO ITEM IN INVENTORY");
            } else {
                inventory.carried = null;
            }
        }
    }

}
