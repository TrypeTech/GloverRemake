using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class CraftStore : MonoBehaviour {


    public GameObject CraftPannel;
    public GameObject CraftOptions;
    public GameObject CraftUnable;
    public Text CraftInfoText;
    public List<CraftItems.CraftItem> craftItems = new List<CraftItems.CraftItem>();
    public List<GameObject> craftSlots = new List<GameObject>();
    public List<int> itemsToRemove = new List<int>();

    simpleInventory inventory;
    CraftItems craft;
    simpleInventory  items;
    int currentChosenItem;
    // Use this for initialization
    void Start () {
        craft = FindObjectOfType<CraftItems>();
        inventory = FindObjectOfType<simpleInventory>();
        items = FindObjectOfType<simpleInventory>();
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.E))
        {
            CraftPannel.gameObject.SetActive(false);
        }
	}








    public void MakeCraftStore()
    {
        CraftPannel.gameObject.SetActive(true);
        craftItems.Clear();
        for (int i = 0; i < craft.CraftItemsList.Count; i++)
        {
            //  if(craft.CraftItemsList[i].ItemIDs.Length == CraftItemsLimit)
            //   {
            craftItems.Add(craft.CraftItemsList[i]);
            //  }
        }
        for (int i = 0; i < craftItems.Count; i++)
        {
            // item name description
            craftSlots[i].gameObject.GetComponentInChildren<Text>().text = craftItems[i].ItemName;
            // add function to button when selected
            int itemNumber = craftItems[i].ID;
            craftSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            craftSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => CraftItem(itemNumber));
        }
    }

    public void CraftItem(int id)
    {
        itemsToRemove.Clear();
        for (int i = 0; i < craftItems.Count; i++)
        {
            if (craftItems[i].ID == id)
            {
                currentChosenItem = craftItems[i].CraftedItemID;
                CraftInfoText.text = craftItems[i].Description;
                // craft takes 2 items
                if (craftItems[i].ItemIDs.Length == 2)
                {
                    int item1 = craftItems[i].ItemIDs[0];
                    int item2 = craftItems[i].ItemIDs[1];
                    if (inventory.playerInventory.Contains(items.ItemsList[item1]) && inventory.playerInventory.Contains(items.ItemsList[item2]))
                    {

                        itemsToRemove.Add(item1);
                        itemsToRemove.Add(item2);
                        // show pannel
                        CraftChoicePannel();
                     
                       
                    }
                }
                // craft takes 3 items
                else if (craftItems[i].ItemIDs.Length == 3)
                {
                    int item1 = craftItems[i].ItemIDs[0];
                    int item2 = craftItems[i].ItemIDs[1];
                    int item3 = craftItems[i].ItemIDs[2];
                    if (inventory.playerInventory.Contains(items.ItemsList[item1]) && inventory.playerInventory.Contains(items.ItemsList[item2]) && inventory.playerInventory.Contains(items.ItemsList[item3]))
                    {

                        itemsToRemove.Add(item1);
                        itemsToRemove.Add(item2);
                        itemsToRemove.Add(item3);

                        // show pannel
                        CraftChoicePannel();
                    }
                    else
                    {
                        CraftUnablePannel();
                    }

                }

            }


        }


    }

    public void CraftChoicePannel()
    {
        CraftOptions.gameObject.SetActive(true);


    }

    // button options
    public void ChooseToCraft()
    {
        inventory.addItem(currentChosenItem);

        for(int i = 0; i < itemsToRemove.Count; i++)
        {
            inventory.RemoveItem(itemsToRemove[i]);
        }
        itemsToRemove.Clear();
        CraftOptions.gameObject.SetActive(false);
        CraftUnable.gameObject.SetActive(false);
    }
    // button option
    public void CancelCraft()
    {
        CraftOptions.gameObject.SetActive(false);
        CraftUnable.gameObject.SetActive(false);
        CraftInfoText.text = "CRAFT INFO:";
    }

    public void CraftUnablePannel()
    {
        CraftUnable.gameObject.SetActive(true);
    }
}
