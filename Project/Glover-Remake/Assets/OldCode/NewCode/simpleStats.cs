using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simpleStats : MonoBehaviour {


    public GameObject StatsCanvasPannel;
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
    public List<simpleInventory.Item> EquiptedItems = new List<simpleInventory.Item>();


    public simpleInventory inventory;
    public simplePlayer player;
    public SimpleMove move;
    public bool CanvasOpen;

    //new
    public int SelectedItem;
    public int ItemCount;
   // public SimpleBattleHandler gManager;

    // Use this for initialization
    void Start()
    {
        CanvasOpen = false;
        StatsCanvasPannel.gameObject.SetActive(false);
        inventory = FindObjectOfType<simpleInventory>();
      
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && CanvasOpen == false )
        {

            move = FindObjectOfType<SimpleMove>();
            // if player cannot move like if he is in event or battle stat 
            // no movment should talk place
            if (move.canMove == true) {
                SelectedItem = 0;
                move.canMove = false;
                Debug.Log("Opend Stats");
                CanvasOpen = true;
                StatsCanvasPannel.gameObject.SetActive(true);
                player = FindObjectOfType<simplePlayer>();
                AddInventoryItemsToList();
                    }
        }
        else if (Input.GetKeyDown(KeyCode.Q) && CanvasOpen == true && move.canMove == false)
        {
            move.canMove = true;
            CanvasOpen = false;
            StatsCanvasPannel.gameObject.SetActive(false);
          //  AddInventoryItemsToList();
        }

        if(CanvasOpen == true)
        {
            SelectSlots();
        }
    }

    public void AddInventoryItemsToList()
    {
        ItemCount = 0;
        EquiptedItems.Clear();
        for (int i = 0; i < inventory.playerInventory.Count; i++)
        {
            if (inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.StatItem)
            {
                ItemCount += 1;
                EquiptedItems.Add(inventory.playerInventory[i]);
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
            int itemNumber = EquiptedItems[i].ID;
            EquiptedSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            EquiptedSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => EquiptOptions(itemNumber));
        }
        EquiptedSlots[0].gameObject.GetComponent<Button>().Select();

        DisplayStats();
    }

    public void DisplayStats()
    {

        StatsInfo.text = " Lv:" + player.playerLevel + " Hp:" + player.HealthStat + " Attack:" + player.AttackStat + " Defence:" + player.DefenceStat
                                 + "/n" + " Speed:" + player.SpeedStat + " Evastion:" + player.EvasionStat + " Accuracy:" + player.AccuracyStat + " SpAttack:" + player.SpAttackStat
                                 + "/n" + " SpDefence:" + player.SpDefenceStat + " Money:" + player.Money + " CREDITS:" + player.Credits;
        // add points to level and levling points

    }
    public void UseItems()
    {
        int id = currentItem;

        for (int i = 0; i < inventory.ItemSlots.Count; i++)
        {
            if (inventory.playerInventory[i].ID == id)
            {
                if (inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.StatItem && inventory.playerInventory[i].Equipted == true)
                {
                    //  Debug.Log("did unequipt");
                    // do stuff then remove this item
                    player.HealthStat -= inventory.playerInventory[i].StaminaStat;
                    player.AttackStat -= inventory.playerInventory[i].AttackStat;
                    player.DefenceStat -= inventory.playerInventory[i].DefenceStat;
                  //  player.SpeedStat -= inventory.playerInventory[i].;
                  //  player.EvasionStat -= inventory.playerInventory[i].Evasion;
                 //   player.AccuracyStat = inventory.playerInventory[i].Accuracy;
                 //player.SpAttackStat -= inventory.playerInventory[i].SpAttack;
            //        player.SpDefenceStat -= inventory.playerInventory[i].SpDefence;

                    // disavle item as equiptable
                    inventory.playerInventory[i].Equipted = false;
                    break;
                }
                // item is not attached so attack it and aplay stats
                else if (inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.StatItem && inventory.playerInventory[i].Equipted == false)
                {
                    //   Debug.Log("did equipt");
                    // do stuff then remove this item
                    player.HealthStat += inventory.playerInventory[i].StaminaStat;
                    player.AttackStat += inventory.playerInventory[i].AttackStat;
                    player.DefenceStat += inventory.playerInventory[i].DefenceStat;
                 //   player.Speed += inventory.inventory[i].Speed;
                 //   player.Evasion += inventory.inventory[i].Evasion;
                //    player.Accuracy += inventory.inventory[i].Accuracy;
                //    player.SpAttack += inventory.inventory[i].SpAttack;
               //     player.SpDef += inventory.inventory[i].SpDefence;

                    // enable item as eqipted
                    inventory.playerInventory[i].Equipted = true;
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

        for (int i = 0; i < inventory.ItemSlots.Count; i++)
        {
            {
                if (inventory.playerInventory[i].ID == id && inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.StatItem && inventory.playerInventory[i].Equipted == true)
                {
                    OptionsPannel.gameObject.SetActive(true);
                    OptionText.text = "UnEquipt this Item?";
                    ItemDescriptionText.text = inventory.playerInventory[i].Description;
                }
                // if item is equipted
                else if (inventory.playerInventory[i].ID == id && inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.StatItem && inventory.playerInventory[i].Equipted == false)
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

                    for (int a = 0; a < inventory.playerInventory.Count; a++)
                    {
                        // check to see how many of each item there is
                        if (inventory.playerInventory[a].Etype == simpleInventory.Item.EquiptType.MainAttacker && inventory.playerInventory[a].Equipted == true)
                        {
                            MainWeaponCount += 1;
                        }
                        if (inventory.playerInventory[a].Etype == simpleInventory.Item.EquiptType.SubAttacker && inventory.playerInventory[a].Equipted == true)
                        {
                            SubWeaponCount += 1;
                        }
                        if (inventory.playerInventory[a].Etype == simpleInventory.Item.EquiptType.MainShield && inventory.playerInventory[a].Equipted == true)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory.playerInventory[a].Etype == simpleInventory.Item.EquiptType.SubShield && inventory.playerInventory[a].Equipted == true)
                        {
                            SubArmorCount += 1;
                        }
                        if (inventory.playerInventory[a].Etype == simpleInventory.Item.EquiptType.MainEmblem && inventory.playerInventory[a].Equipted == true)
                        {
                            MainEmblemCount += 1;
                        }
                        if (inventory.playerInventory[a].Etype == simpleInventory.Item.EquiptType.SubEmblem && inventory.playerInventory[a].Equipted == true)
                        {
                            SubEmblemCount += 1;
                        }


                    }

                    // check the nubers to c if they went over if so stat have to remove a sertain item to att this one else use it
                    ////////////////////////////////////////////////////// weapon
                    if (MainWeaponCount > 0 && inventory.playerInventory[i].Etype == simpleInventory.Item.EquiptType.MainAttacker)
                    {
                        CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only One Main Weapon Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubWeaponCount > 1 && inventory.playerInventory[i].Etype == simpleInventory.Item.EquiptType.SubAttacker)
                    {
                        CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only 2  Weapon Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    ////////////////////////////////////////////////////// armor
                    else if (MainArmorCount > 0 && inventory.playerInventory[i].Etype == simpleInventory.Item.EquiptType.MainShield)
                    {
                        CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only One Main Armor Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubArmorCount > 1 && inventory.playerInventory[i].Etype == simpleInventory.Item.EquiptType.SubShield)
                    {
                        CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only 2  Armor Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    ////////////////////////////////////////////////////// emblem
                    else if (MainEmblemCount > 0 && inventory.playerInventory[i].Etype == simpleInventory.Item.EquiptType.MainEmblem)
                    {

                        CancelOption.gameObject.SetActive(true);
                        CancelText.text = "Only One Main Emblem Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubEmblemCount > 1 && inventory.playerInventory[i].Etype == simpleInventory.Item.EquiptType.SubEmblem)
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

                    ItemDescriptionText.text = inventory.playerInventory[i].Description;
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
        ItemDescriptionText.text = EquiptedItems[0].Description;
        EquiptedSlots[SelectedItem - 1].gameObject.GetComponent<Button>().Select();

    }




    public void SelectSlots()
    {

        // go up and right selecting the items in the inventory 
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
        {
            SelectedItem += 1;
            if (ItemCount == 0)
            {
                SelectedItem = 0;
            }
            if (SelectedItem > ItemCount - 1)
            {
                SelectedItem = 0;
            }
            EquiptedSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

            // add the item description here                                           
            ItemDescriptionText.text = EquiptedItems[SelectedItem].Description;       // <<<<<<< added new code here

            
         
        }

        // go down and left selecting the items in the inventory
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            SelectedItem -= 1;
            if (ItemCount == 0)
            {
                SelectedItem = 0;
            }
            if (SelectedItem < 0)
            {
                SelectedItem = ItemCount - 1;
            }
            EquiptedSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

            // add the item description here                                             
            ItemDescriptionText.text = EquiptedItems[SelectedItem].Description;    //   <<<<<<< added new code 
          
       
            Debug.Log("SelectedItem:" + SelectedItem.ToString());

        }


        // press space bar to use the item that is selected
        if (Input.GetKeyDown(KeyCode.B))
        {

            EquiptOptions(SelectedItem - 1);

        }
    }
}
