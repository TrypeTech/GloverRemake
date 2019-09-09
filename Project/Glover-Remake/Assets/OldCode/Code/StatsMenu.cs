using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsMenu : MonoBehaviour {

    public GameObject CharacterImage;
    public GameObject MainWeaponImage;
    public GameObject MainArmourImage;
    public GameObject MainEmblemImage;

    public GameObject SubeWeapon1Image;
    public GameObject SubeWeapon2Image;

    public GameObject SubeArmour1Image;
    public GameObject SubeArmour2Image;

    public GameObject SubeEmblem1Image;
    public GameObject SubeEmblem2Image;
    // more in the future

    public Text ItemName;
    public Text StatsInfo;
    public int currentItem;

    // Options Pannel
    public GameObject OptionsPannel;
    public GameObject CancelOption;
    public Text CancelText;
    public Text OptionText;
    // item info text
    public Text ItemDescriptionText;
  
    public List<GameObject> EquiptedSlots = new List<GameObject>();
    public List<Items.Item> EquiptedItems = new List<Items.Item>();


    public Inventory inventory;
    public Player player;
    public GamManager gManager;
    
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<Inventory>();
        gManager = FindObjectOfType<GamManager>();
        gManager.SwitchCanvases(GamManager.ActiveCanvas.stats);
      //  AddInventoryItemsToList();
        gManager.SwitchCanvases(GamManager.ActiveCanvas.inGame);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            AddInventoryItemsToList();
        }
	}

    public void AddInventoryItemsToList()
    {
        EquiptedItems.Clear();
        for (int i = 0;i < inventory.inventory.Count; i++)
        {
            if(inventory.inventory[i].type == Items.Item.ItemType.Eqiptable )
            {
                EquiptedItems.Add(inventory.inventory[i]);
            }
            
        }
        ReconstructEquipted();
    }


    public void ReconstructEquipted()
    {
      
        for (int i = 0; i < EquiptedItems.Count; i++)
        {
            if (EquiptedItems[i].Equipted == true)
            {
                EquiptedSlots[i].gameObject.GetComponentInChildren<Text>().text = EquiptedItems[i].Name + " " + "Equipted " + "   " + EquiptedItems[i].Count;
            }
            if (EquiptedItems[i].Equipted == false)
            {
                EquiptedSlots[i].gameObject.GetComponentInChildren<Text>().text = EquiptedItems[i].Name + " " + "UnEquipted " + "   " + EquiptedItems[i].Count;
            }
            int itemNumber = EquiptedItems[i].itemNumber;
            EquiptedSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            EquiptedSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => EquiptOptions(itemNumber));
        }
        DisplayStats();
        }

    public void DisplayStats()
    {

        StatsInfo.text = " Lv:" + player.Level + " Hp:" + player.Hp + " Attack:" + player.Attack + " Defence:" + player.Defence
                                 + "/n" + " Speed:" + player.Speed + " Evastion:" + player.Evasion + " Accuracy:" + player.Accuracy + " SpAttack:" + player.SpAttack
                                 + "/n" + " SpDefence:" + player.SpDef + " Money:" + player.Money ;
        // add points to level and levling points

    }
    public void UseItems()
    {
        int id = currentItem;

        for (int i = 0; i < inventory.itemSlots.Count; i++) {
            if (inventory.inventory[i].itemNumber == id)
            {
                if (inventory.inventory[i].type == Items.Item.ItemType.Eqiptable && inventory.inventory[i].Equipted == true)
                {
                    //  Debug.Log("did unequipt");
                    // do stuff then remove this item
                    player.Hp -= inventory.inventory[i].Hp;
                    player.Attack -= inventory.inventory[i].Attack;
                    player.Defence -= inventory.inventory[i].Defence;
                    player.Speed -= inventory.inventory[i].Speed;
                    player.Evasion -= inventory.inventory[i].Evasion;
                    player.Accuracy -= inventory.inventory[i].Accuracy;
                    player.SpAttack -= inventory.inventory[i].SpAttack;
                    player.SpDef -= inventory.inventory[i].SpDefence;

                    // disavle item as equiptable
                    inventory.inventory[i].Equipted = false;
                    break;
                }
                // item is not attached so attack it and aplay stats
                else if (inventory.inventory[i].type == Items.Item.ItemType.Eqiptable && inventory.inventory[i].Equipted == false)
                {
                    //   Debug.Log("did equipt");
                    // do stuff then remove this item
                    player.Hp += inventory.inventory[i].Hp;
                    player.Attack += inventory.inventory[i].Attack;
                    player.Defence += inventory.inventory[i].Defence;
                    player.Speed += inventory.inventory[i].Speed;
                    player.Evasion += inventory.inventory[i].Evasion;
                    player.Accuracy += inventory.inventory[i].Accuracy;
                    player.SpAttack += inventory.inventory[i].SpAttack;
                    player.SpDef += inventory.inventory[i].SpDefence;

                    // enable item as eqipted
                    inventory.inventory[i].Equipted = true;
                    break;
                }
            }
        }
        AddInventoryItemsToList();
        CancelSelection();
    }


    // equipt options
    public void EquiptOptions(int id)
    {
        currentItem = id;

        for (int i = 0; i < inventory.itemSlots.Count; i++)
        {
            {
                if (inventory.inventory[i].itemNumber == id && inventory.inventory[i].type == Items.Item.ItemType.Eqiptable && inventory.inventory[i].Equipted == true)
                {
                    OptionsPannel.gameObject.SetActive(true);
                 OptionText.text = "UnEquipt this Item?";
                    ItemDescriptionText.text = inventory.inventory[i].Description;
                }
                // if item is equipted
                else if (inventory.inventory[i].itemNumber == id && inventory.inventory[i].type == Items.Item.ItemType.Eqiptable && inventory.inventory[i].Equipted == false)
                {
                    // check if the maximum number of subs and main r used
                    // weapon
                    int MainWeaponCount = 0;
                    int SubWeaponCount = 0;
                    //  int MinorWeaponCount = 0;
                    // armor
                    int MainArmorCount = 0;
                    int SubArmorCount = 0;
                    //  int MinorArmorCount = 0;
                    // emblem
                    int MainEmblemCount = 0;
                    int SubEmblemCount = 0;
                    //  int MinormblemCount = 0;

                    for (int a = 0; a < inventory.inventory.Count; a++)
                    {
                        // check to see how many of each item there is
                        if (inventory.inventory[a].equiptType == Items.Item.EquiptType.MainAttacker)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory.inventory[a].equiptType == Items.Item.EquiptType.SubAttacker)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory.inventory[a].equiptType == Items.Item.EquiptType.MainArmor)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory.inventory[a].equiptType == Items.Item.EquiptType.SubArmor)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory.inventory[a].equiptType == Items.Item.EquiptType.MainEmblem)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory.inventory[a].equiptType == Items.Item.EquiptType.SubeEmblem)
                        {
                            MainArmorCount += 1;
                        }


                    }

                    // check the nubers to c if they went over if so stat have to remove a sertain item to att this one else use it
                    ////////////////////////////////////////////////////// weapon
                    if (MainWeaponCount > 0 && inventory.inventory[i].equiptType == Items.Item.EquiptType.MainAttacker)
                    {
                        CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only One Main Weapon Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubWeaponCount > 1 && inventory.inventory[i].equiptType == Items.Item.EquiptType.SubAttacker)
                    {
                          CancelOption.gameObject.SetActive(true);
                       CancelText.text = "Only 2  Weapon Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    ////////////////////////////////////////////////////// armor
                    else if (MainArmorCount > 0 && inventory.inventory[i].equiptType == Items.Item.EquiptType.MainArmor)
                    {
                  CancelOption.gameObject.SetActive(true);
                       CancelText.text = "Only One Main Armor Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubArmorCount > 1 && inventory.inventory[i].equiptType == Items.Item.EquiptType.SubArmor)
                    {
                        CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only 2  Armor Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    ////////////////////////////////////////////////////// emblem
                    else if (MainEmblemCount > 0 && inventory.inventory[i].equiptType == Items.Item.EquiptType.MainEmblem)
                    {

                         CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only One Main Emblem Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubEmblemCount > 1 && inventory.inventory[i].equiptType == Items.Item.EquiptType.SubeEmblem)
                    {
                          CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only 2  Emblem Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    else
                    {
                        // enable equipt pannel option
                        CancelOption.gameObject.SetActive(false);
                        OptionsPannel.gameObject.SetActive(true);
                      OptionText.text = "Equipt this Item?";
                    }
                    //////////////////////////////////////////////////////

                  ItemDescriptionText.text = inventory.inventory[i].Description;
                }
            }
        }
        }
    public void CancelSelection()
    {
        // disable options
        CancelText.text = "";
        CancelOption.gameObject.SetActive(false);
       OptionsPannel.gameObject.SetActive(false);
        ItemDescriptionText.text = "ITEM INFO:";
    }
}
