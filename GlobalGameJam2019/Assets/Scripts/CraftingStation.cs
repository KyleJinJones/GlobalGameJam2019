using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : MonoBehaviour
{
    public GameObject materialPrefab;
    public Transform[] taskBoardSlots;
    public Image processBar;
    //public Text taskBoard;
    public enum matetialTypeList {wall};
    public matetialTypeList materialType;
    public recipe[] Recipe;
    public float processSpeed;
    List<recipe.resourceNameList> ResourceNameList;
    List<int> resourceQuantity;
    //public int wall = 4;
    float fillAmount;
    Inventory inventory;
    GameObject carriedObject;

    void Start()
    {
        foreach (Transform slot in taskBoardSlots)
        {
            slot.gameObject.SetActive(false);
        }
        ResourceNameList = new List<recipe.resourceNameList>();
        resourceQuantity = new List<int>();
        foreach (recipe resource in Recipe)
        {
            ResourceNameList.Add(resource.resourceName);
            resourceQuantity.Add(resource.quantity);
        }
        initializeTaskBoard();
        initializeProcessBar();

    }

    void initializeTaskBoard()
    {
        for (int i = 0; i < Recipe.Length; i++)
        {
            taskBoardSlots[i].gameObject.SetActive(true);
            taskBoardSlots[i].Find("image").GetComponent<Image>().sprite = Recipe[i].sprite;
            taskBoardSlots[i].Find("text").GetComponent<Text>().text = Recipe[i].quantity.ToString();
        }
    }

    void initializeProcessBar()
    {
        fillAmount = 0;
        Debug.Log("name:" + this.name);
        Debug.Log("process bar:" + processBar);
        processBar.fillAmount = fillAmount;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            carriedObject = other.GetComponent<Inventory>().carried;
            inventory = other.GetComponent<Inventory>();
            //itemInInventory = other.GetComponent<Inventory>().carried.GetComponent<item>();
            if (inventory.carried != null && ResourceNameList.Contains(carriedObject.GetComponent<item>().resourceType) && Input.GetKeyDown(KeyCode.E))
            {
                carriedObject.SetActive(false);
                StartCoroutine(processMaterial());
            }
        }
    }
    IEnumerator processMaterial()
    {
        while (fillAmount < 1f)
        {
            if (Input.GetKey(KeyCode.E))
            {
                fillAmount = Mathf.Min(1f, fillAmount + Time.deltaTime * processSpeed);
                processBar.fillAmount = fillAmount;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                initializeProcessBar();
                Destroy(carriedObject);
                inventory.carried = null;
                yield break;
            } 
        }
        //update task quantity
        int index = ResourceNameList.IndexOf(carriedObject.GetComponent<item>().resourceType);
        resourceQuantity[index]--;
        taskBoardSlots[index].Find("text").GetComponent<Text>().text = resourceQuantity[index].ToString();
        //check if produce new material
        bool signal = true;
        foreach (int quantity in resourceQuantity)
        {
            if (quantity != 0)
            {
                signal = false;
                break;
            }
        }
        if (signal == true)
        {
            Instantiate(materialPrefab, this.transform.position + 7 * Vector3.left, Quaternion.identity);
            initializeTaskBoard();
        }
        
        initializeProcessBar();
        Destroy(carriedObject);
        inventory.carried = null;
        
    }
}

[System.Serializable]
public class recipe
{
    public enum resourceNameList { wood, stone, metal};
    public resourceNameList resourceName;
    public Sprite sprite;
    public int quantity;
}
