using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Store : MonoBehaviour {


    public GameObject StorePannel;
    public Text DescriptionText;
    public GameObject OptionPannel;
    public GameObject CancelPannel;

    public List<simpleInventory.Item> StoreItems = new List<simpleInventory.Item>();
    public List<GameObject> ItemSlots = new List<GameObject>();
    simpleInventory items;
    simpleInventory inventory;
    simplePlayer player;
    public int SelectedItem;

    int currentChosenItem;
    public int CostRange;
    // Use this for initialization
    void Start () {
        SelectedItem = 0;
        items = FindObjectOfType<simpleInventory>();
        inventory = FindObjectOfType<simpleInventory>();
        player = FindObjectOfType<simplePlayer>();
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            StorePannel.gameObject.SetActive(false);
        }
	}

    
    public void MakeStore(int PriceRange)
    {
        StorePannel.gameObject.SetActive(true);
        StoreItems.Clear();
        for (int i = 0; i < items.ItemsList.Count; i++)
        {
            if (items.ItemsList[i].Cost <= PriceRange + 1)
            {
                StoreItems.Add(items.ItemsList[i]);

            }
        }

        for (int i = 0; i < StoreItems.Count; i++)
        {

            ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = StoreItems[i].Name + " " + StoreItems[i].Cost + " :Cost";
            // add function to button when selected
            int itemNumber = StoreItems[i].ID;
            ItemSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            ItemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => AddItem(itemNumber));
        }
    }

    public void AddItem(int id)
    {

        currentChosenItem = id;

        for (int i = 0; i < StoreItems.Count; i++)
        {

            if (StoreItems[i].ID == id)
            {
                // if player has enuf
                if (player.Money >= StoreItems[i].Cost)
                {
                    // open pannel choice
                    DescriptionText.text = "Item Info:" + StoreItems[i].Description;
                  OptionPannel.gameObject.SetActive(true);
                }
                else if (player.Money < StoreItems[i].Cost)
                {
                    DescriptionText.text = "Item Info:" + StoreItems[i].Description;
                  CancelPannel.gameObject.SetActive(true);
                }

            }
        }
    }




    public void Options(int id)
    {

        inventory.addItem(currentChosenItem);
        for (int i = 0; i < StoreItems.Count; i++)
        {
            if (StoreItems[i].ID == id)
            {
                player.Money -= StoreItems[i].Cost;
            }
        }
        ExitCancelChoice();
    }

    public void Cancel(int id)
    {
        ExitCancelChoice();
    }

    public void ExitCancelChoice()
    {
        DescriptionText.text = "Item Info:";
        CancelPannel.gameObject.SetActive(false);
        OptionPannel.gameObject.SetActive(false);

    }
}
