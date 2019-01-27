using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CraftingStation : MonoBehaviour
{
    public Transform materialPrefab;
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
    item itemInInventory;

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
            itemInInventory = other.GetComponent<Inventory>().carried.GetComponent<item>();
            if (itemInInventory!= null && ResourceNameList.Contains(itemInInventory.resourceType) && Input.GetKeyDown(KeyCode.E))
            {
                other.GetComponent<Inventory>().carried.gameObject.SetActive(false);
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
                Destroy(itemInInventory);
                itemInInventory = null;
                yield break;
            } 
        }
        //update task quantity
        int index = ResourceNameList.IndexOf(itemInInventory.resourceType);
        resourceQuantity[index]--;
        Debug.Log("index:" + index);
        Debug.Log("quantity" + resourceQuantity[index]);
        taskBoardSlots[index].Find("text").GetComponent<Text>().text = resourceQuantity[index].ToString();
        Debug.Log("text:" + taskBoardSlots[index].Find("text").GetComponent<Text>().text);
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
            Instantiate(materialPrefab, this.transform.position, Quaternion.identity);
            initializeTaskBoard();
        }
        
        initializeProcessBar();
        Destroy(itemInInventory);
        itemInInventory = null;
        
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
