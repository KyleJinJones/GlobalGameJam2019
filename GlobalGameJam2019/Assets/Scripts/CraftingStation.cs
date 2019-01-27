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
    public float processingSpeed;
    List<recipe.resourceNameList> ResourceNameList;
    List<int> resourceQuantity;
    //public int wall = 4;
    float fillAmount;
    Inventory inventory;
    GameObject carriedObject;
    bool isWorking;
    //test
    public float test = 1;

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
        }
        resetTask();
        resetProcess();

    }

    void resetTask()
    {
        resourceQuantity.RemoveRange(0, resourceQuantity.Count);
        foreach (recipe resource in Recipe)
        {
            resourceQuantity.Add(resource.quantity);
        }
        for (int i = 0; i < Recipe.Length; i++)
        {
            taskBoardSlots[i].gameObject.SetActive(true);
            taskBoardSlots[i].Find("image").GetComponent<Image>().sprite = Recipe[i].sprite;
            taskBoardSlots[i].Find("text").GetComponent<Text>().text = Recipe[i].quantity.ToString();
        }
    }

    void resetProcess()
    {
        fillAmount = 0;
        processBar.fillAmount = fillAmount;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag("Player"))
        {
            recipe.resourceNameList resourceType;
            carriedObject = other.GetComponent<Inventory>().carried;
            inventory = other.GetComponent<Inventory>();
            if (isWorking == false && carriedObject != null && carriedObject.CompareTag("resource") &&ResourceNameList.Contains(carriedObject.GetComponent<item>().resourceType) && Input.GetAxisRaw(other.GetComponent<PlayerMovement>().InteractAxis) == 1)
            {
                resourceType = carriedObject.GetComponent<item>().resourceType;
                isWorking = true;
                carriedObject.SetActive(false);
                inventory.carried = null;
                StartCoroutine(processMaterial(resourceType, carriedObject));
            }
        }
    }
    IEnumerator processMaterial(recipe.resourceNameList resourceType, GameObject carriedObjectPass)
    {
        while (fillAmount < 1f)
        {
            if (Input.GetKey(KeyCode.E))
            {
                fillAmount = Mathf.Min(1f, fillAmount + Time.deltaTime * processingSpeed);
                processBar.fillAmount = fillAmount;
                yield return new WaitForEndOfFrame();
            }
            else
            {
                resetProcess();
                carriedObjectPass.SetActive(true);
                carriedObjectPass.transform.position = this.transform.position + 5 * Vector3.left;
                isWorking = false;
                yield break;
            } 
        }
        //update task quantity
        int index = ResourceNameList.IndexOf(resourceType);
        
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
            resetTask();
        }
        resetProcess();
        Destroy(carriedObject);
        isWorking = false;
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
