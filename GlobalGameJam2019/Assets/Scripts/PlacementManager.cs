using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlacementManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Placement> walls;
    public GameObject player;
    private float least=0;
    private Placement closest;
    [SerializeField] private float mindistance=2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            least = walls[0].GetDistance(player.transform.position);
            closest = walls[0];
            for (int i = 1; i < walls.Count; i++)
            {
                if (!walls[i].placed)
                {
                    float temp = walls[i].GetDistance(player.transform.position);
                    if (temp < least)
                    {
                        least = temp;
                        closest = walls[i];
                    }
                }
            }

            if (least <= mindistance)
            {
                closest.Place();
            }
            Debug.Log(least);
        }

    }
}
