using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacementManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Placement> walls;
    public GameObject player;
    private float least=0;
    private Placement closest=null;
    [SerializeField] private float mindistance=2;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check if there's no more walls to be placed
        
        
        //Temporarily set the last highlighted wall off
        if (closest != null)
        {
            closest.DefaultColor();
        }
            
        //Loop through walls to find which one the player is closest to
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

        //Once the closest wall has been found, check player distance from it
        //If it is close enough, highlight it, or if they press e, place the wall
        if (least <= mindistance)
        {

            if (Input.GetKeyDown(KeyCode.E))
            {
                closest.Place();
                if (walls.Count == 1)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                }
                walls.Remove(closest);
                closest = null;

            }
            else
            {
                closest.Highlight();
            }
           
        }
            }
           
        


    }

