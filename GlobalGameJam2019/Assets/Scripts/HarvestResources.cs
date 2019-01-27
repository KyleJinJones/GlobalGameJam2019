using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarvestResources : MonoBehaviour
{
    // Start is called before the first frame update
    const float HARVEST_TIME = 1.0f;
    float timer = HARVEST_TIME;
    public GameObject rawMaterial; 
    public GameObject particles;
    private GameObject spawnParticles;
    private int playerCounter;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter(Collider other) {
        if(other.gameObject.GetComponent<Inventory>()) {
            playerCounter += 1;
            if(playerCounter == 1)
                spawnParticles = Instantiate(particles, transform.position, Quaternion.identity);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        Inventory inventory = other.GetComponent<Inventory>();

        if(Input.GetKey(KeyCode.E)) {
            if(inventory.carried != null) {
                Debug.Log("CANNOT HARVEST ITEM; CURRENTLY CARRYING ITEM");
            } else {
                timer = timer - Time.deltaTime;
                Debug.Log(timer);
                if(timer <= 0) {
                    Debug.Log("ITEM HARVESTED");
                    inventory.carried = Instantiate(rawMaterial, other.transform.position, Quaternion.identity);
                    timer = HARVEST_TIME;
                }
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        if(other.gameObject.GetComponent<Inventory>()) {
            playerCounter -= 1;
            if(playerCounter == 0)
                Destroy(spawnParticles);
        }
    }

}

