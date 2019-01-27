using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlacementManager : MonoBehaviour
{
    // Start is called before the first frame update
    public List<Placement> walls;
    public List<GameObject> player;
    private float least=0;
    private Placement[] closest = null;
    [SerializeField] private float mindistance=2;
    void Start()
    {
        closest = new Placement[4];
        closest[0] = walls[0];
        closest[1] = walls[0];
        closest[2] = walls[0];
        closest[3] = walls[0];
    }

    // Update is called once per frame
    void Update()
    {
        if (closest != null)
        {
            for (int i = 0; i < closest.Length; i++)
            {
                if (closest[i] != null)
                {
                    closest[i].DefaultColor();
                }
            }
        }
   
        
        //Check if there's no more walls to be placed
        for (int p = 0; p < player.Count; p++)
        {

            //Temporarily set the last highlighted wall off

            if(player[p].GetComponent<Inventory>().carried != null&& player[p].GetComponent<Inventory>().carried.name.Contains("RM")==false) { 
            //Loop through walls to find which one the player is closest to
            least = walls[0].GetDistance(player[p].transform.position);
            closest[p] = walls[0];
            for (int i = 1; i < walls.Count; i++)
            {
                if (!walls[i].placed&&player[p].GetComponent<Inventory>().carried.GetComponent<BuildType>().building==walls[i].construct)
                {
                    float temp = walls[i].GetDistance(player[p].transform.position);
                    if (temp < least)
                    {
                        least = temp;
                        closest[p] = walls[i];
                    }
                }
            }

                //Once the closest wall has been found, check player distance from it
                //If it is close enough, highlight it, or if they press e, place the wall
                if (least <= mindistance)
                {

                    if (Input.GetAxisRaw(player[p].GetComponent<PlayerMovement>().InteractAxis) == 1)
                    {
                        closest[p].Place();
                        if (walls.Count == 1)
                        {
                            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
                        }
                        walls.Remove(closest[p]);
                        GameObject temp = player[p].GetComponent<Inventory>().carried;
                        player[p].GetComponent<Inventory>().carried = null;
                        Destroy(temp);
                        closest[p] = null;
                    }
                    else
                    {
                        closest[p].Highlight();
                    }
                }

            }
        }
    }
           
        


    }

