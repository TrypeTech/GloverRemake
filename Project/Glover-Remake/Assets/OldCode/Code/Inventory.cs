using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class Inventory : MonoBehaviour {

    public List<Items.Item> inventory = new List<Items.Item>();
    public List<GameObject> itemSlots = new List<GameObject>();

    public int SlotCount;
    public GameObject SlotCanvas;
    public GameObject SlotPrefab;
    public Text ItemDescription;

    // option pannels
    public int CurrentItem;
    public GameObject UseItemOptions;
    public GameObject CancelOptions;
    public Text OptionsInfo;
    public Text CancelInfo;

    // battle data
    public bool inInventory;
    // refrences
     Items item;
      GamManager manager;
      Player player;

    // wait bool
    public bool waited;

    // Use this for initialization
    private void Awake()
    {
        item = FindObjectOfType<Items>();
        manager = FindObjectOfType<GamManager>();
        player = FindObjectOfType<Player>();
    }
    void Start () {
       
        waited = true;

       Invoke("StartInventory", 0.2f);


    }
	

    public void StartInventory()
    {
        manager.SwitchCanvases(GamManager.ActiveCanvas.inventory);
       
        for (int i = 0; i < itemSlots.Count; i++)
        {
            inventory.Add(item.items[0]);
        }
        // load saved data here 
      //  manager.SwitchCanvases(GamManager.ActiveCanvas.inventory);
       
        ReconstructInventory();
        manager.SwitchCanvases(GamManager.ActiveCanvas.inGame);
        inInventory = false;

    }
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.A))
        {
            //   inventory.Add(item.items[0]);
            //  inventory.Add(item.items[2]);
            //  inventory.Add(item.items[3]);
            //   inventory.Add(item.items[4]);
            //   inventory.Add(item.items[1]);

        
                addItem(1);
                addItem(2);
                addItem(3);
                addItem(5);
                addItem(4);
            addItem(6);
            //addItem(1);
            Debug.Log("adda item");
       
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            manager.SwitchCanvases(GamManager.ActiveCanvas.inGame);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            ReconstructInventory();
        }
        }
  

    // resets the inventory and sets data to the slots
    public void ReconstructInventory()
    {
      
        // enable the slot menu pannel to do this function or there will be errors
        manager.SwitchCanvases(GamManager.ActiveCanvas.inventory);

      
            for (int i = 0; i < itemSlots.Count; i++)
            {
                if (inventory.Count < 1)
                {
                    Debug.Log("is less then zero in reconstruct");
                    return;

                }
                if (inventory[i].Equipted == true && inventory[i].type == Items.Item.ItemType.Eqiptable)
                {
                    itemSlots[i].gameObject.GetComponentInChildren<Text>().text = inventory[i].Name + " " + "Equipted " + "   " + inventory[i].Count.ToString();
                }
                else if (inventory[i].Equipted == false && inventory[i].type == Items.Item.ItemType.Eqiptable)
                {
                    // dont need this text so remove in alittle wile
                    itemSlots[i].gameObject.GetComponentInChildren<Text>().text = inventory[i].Name + " " + "UnEquipted" + "   " + inventory[i].Count.ToString();
                }
                else
                {
                    itemSlots[i].gameObject.GetComponentInChildren<Text>().text = inventory[i].Name + "   " + inventory[i].Count.ToString();
                }

                // add function to button when selected
                int itemNumber = inventory[i].itemNumber;
                itemSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
                itemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => OptionPannel(itemNumber));

            
            itemSlots[0].gameObject.GetComponentInChildren<Button>().Select();
        }
      
    }

    
    // add item to inventory
    public void addItem(int id)
    {
        // check if there is one already in the inventory

     
      
            if(inventory.Contains (item.items [id]) == true && inventory[id].itemNumber != 0)
            {
            for (int i = 0; i < inventory.Count ; i++)
            {
              if(inventory [i].itemNumber == id)
                {
                    inventory[i].Count += 1;
                    break;
                }
            }
            }

        // check to see if there is a impty slot
        else if (inventory.Contains (item.items [id]) == false )
       
        {
            for (int i = 0; i < inventory.Count ; i++)
            {
               if(inventory [i].itemNumber == 0)
                {
                    inventory.Remove(inventory[i]);
                    // int indexLoc = i;
                    //inventory.Add(item.items[id]);
                    inventory.Insert(i, item.items[id]);
                    inventory[i].Count = 0;
                    inventory[i].Count += 1;
                    break;
                }
            }
        // <<<<<<<<<<<<< make option to delete item
        }
       
            ReconstructInventory();
        


        //then go back to in game or place cuz adding items usualy dont happen in the inventory 
       manager.SwitchCanvases(GamManager.ActiveCanvas.inGame);
    }


    // delete item\

    public void removeItem(int id)
    {
        // removes item from inventory
        for (int i = 0; i < itemSlots.Count; i++)
        {
            if (inventory[i].itemNumber == id)
            {
                for (int a = 0; a < itemSlots.Count; a++)
                {
                    inventory[a].Index = a;
                }

                inventory.RemoveAt(i);
                inventory.Add(item.items[0]);
                inventory[itemSlots.Count - 1].Index = itemSlots.Count;
                //   inventory.Insert(itemSlots.Count - 1, item.items[0]);
                //   inventory.Insert(i, item.items[0]);

                // remeber this for leader board :D 
                inventory =  inventory.OrderBy(w => w.Index).ToList();
                itemSlots[0].gameObject.GetComponentInChildren<Button>().Select();
                break;
            }

           
        }
        for (int i = 0; i < itemSlots.Count; i++)
        {

         //   Debug.Log(inventory[i].itemNumber + "itemNub");
         //   Debug.Log(inventory[i].Index + "index");
        }
        if (inInventory)
        {
            ReconstructInventory();
        }
       
    }

    public void useItem()
    {
        int id = CurrentItem;
        if (waited)
        {
            Debug.Log("added item :D");
            waited = false;
         //   Debug.Log("did button");
            for (int i = 0; i < inventory.Count; i++)
            {

        //        Debug.Log("lv 1");
                if (inventory[i].itemNumber == id)
                {
                 //   Debug.Log(inventory[i].type.ToString());
                 //   Debug.Log("lv 2");
                    // use item then delete it
                    if (inventory[i].type == Items.Item.ItemType.Useable)
                    {
                    //    Debug.Log("did usable");
                        // do stuff then remove this item
                        player.Hp += inventory[i].Hp;
                        player.Attack += inventory[i].Attack;
                        player.Defence += inventory[i].Defence;
                        player.Speed += inventory[i].Speed;
                        player.Evasion += inventory[i].Evasion;
                        player.Accuracy += inventory[i].Accuracy;
                        player.SpAttack += inventory[i].SpAttack;
                        player.SpDef += inventory[i].SpDefence;

                        // check how many left
                        if (inventory[i].Count <= 1)
                        {
                      //      Debug.Log("used and deleted");
                            removeItem(id);
                        }
                        else if (inventory[i].Count > 1)
                        {
                       //     Debug.Log("used and minus");
                            inventory[i].Count -= 1;

                        }
                        break;
                    }

                    // check if item is equipt if so undo what was done in stats and unequipt it
                 else  if (inventory[i].type == Items.Item.ItemType.Eqiptable && inventory[i].Equipted == true)
                    {
                      //  Debug.Log("did unequipt");
                        // do stuff then remove this item
                        player.Hp -= inventory[i].Hp;
                        player.Attack -= inventory[i].Attack;
                        player.Defence -= inventory[i].Defence;
                        player.Speed -= inventory[i].Speed;
                        player.Evasion -= inventory[i].Evasion;
                        player.Accuracy -= inventory[i].Accuracy;
                        player.SpAttack -= inventory[i].SpAttack;
                        player.SpDef -= inventory[i].SpDefence;

                        // disavle item as equiptable
                        inventory[i].Equipted = false;
                        break;
                    }
                    // item is not attached so attack it and aplay stats
                 else   if (inventory[i].type == Items.Item.ItemType.Eqiptable && inventory[i].Equipted == false)
                    {
                     //   Debug.Log("did equipt");
                        // do stuff then remove this item
                        player.Hp += inventory[i].Hp;
                        player.Attack += inventory[i].Attack;
                        player.Defence += inventory[i].Defence;
                        player.Speed += inventory[i].Speed;
                        player.Evasion += inventory[i].Evasion;
                        player.Accuracy += inventory[i].Accuracy;
                        player.SpAttack += inventory[i].SpAttack;
                        player.SpDef += inventory[i].SpDefence;

                        // enable item as eqipted
                        inventory[i].Equipted = true;
                        break;
                    }
              
                }
            }
            ReconstructInventory();
            OptionsInfo.text = "";
            ItemDescription.text = "ITEM INFO:";
            CancelOptions.gameObject.SetActive(false);
            UseItemOptions.gameObject.SetActive(false);
            // if add too many remove
            waited = true;
        }
        else
        {
            StartCoroutine(WaitASec());
        }
    }

    public IEnumerator WaitASec()
    {
        yield return new WaitForSeconds(0.01f);
        waited = true;
    }



   public void OptionPannel(int id)
    {
       
           
            CurrentItem = id;
            // if is usable
            for (int i = 0; i < inventory.Count; i++)
            {
                // if item is a usable
                if (inventory[i].itemNumber == id && inventory[i].type == Items.Item.ItemType.Useable)
                {
                    UseItemOptions.gameObject.SetActive(true);
                    OptionsInfo.text = "Use this Item?";
                ItemDescription.text = inventory[i].Description;
                }
                // if is unequipted
              else  if (inventory[i].itemNumber == id && inventory[i].type == Items.Item.ItemType.Eqiptable && inventory[i].Equipted == true)
                {
                    UseItemOptions.gameObject.SetActive(true);
                    OptionsInfo.text = "UnEquipt this Item?";
                ItemDescription.text = inventory[i].Description;
            }
                // if item is equipted
            else    if (inventory[i].itemNumber == id && inventory[i].type == Items.Item.ItemType.Eqiptable && inventory[i].Equipted == false)
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

                    for (int a = 0; a < inventory.Count; a++)
                    {
                        // check to see how many of each item there is
                        if (inventory[a].equiptType == Items.Item.EquiptType.MainAttacker)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory[a].equiptType == Items.Item.EquiptType.SubAttacker)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory[a].equiptType == Items.Item.EquiptType.MainArmor)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory[a].equiptType == Items.Item.EquiptType.SubArmor)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory[a].equiptType == Items.Item.EquiptType.MainEmblem)
                        {
                            MainArmorCount += 1;
                        }
                        if (inventory[a].equiptType == Items.Item.EquiptType.SubeEmblem)
                        {
                            MainArmorCount += 1;
                        }


                    }

                    // check the nubers to c if they went over if so stat have to remove a sertain item to att this one else use it
                    ////////////////////////////////////////////////////// weapon
                    if (MainWeaponCount > 0 && inventory[i].equiptType == Items.Item.EquiptType.MainAttacker)
                    {
                        CancelInfo.text = "Only One Main Weapon Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubWeaponCount > 1 && inventory[i].equiptType == Items.Item.EquiptType.SubAttacker)
                    {
                        CancelOptions.gameObject.SetActive(true);
                        CancelInfo.text = "Only 2  Weapon Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    ////////////////////////////////////////////////////// armor
                    else if (MainArmorCount > 0 && inventory[i].equiptType == Items.Item.EquiptType.MainArmor)
                    {
                        CancelOptions.gameObject.SetActive(true);
                        CancelInfo.text = "Only One Main Armor Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubArmorCount > 1 && inventory[i].equiptType == Items.Item.EquiptType.SubArmor)
                    {
                        CancelOptions.gameObject.SetActive(true);
                        CancelInfo.text = "Only 2  Armor Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    ////////////////////////////////////////////////////// emblem
                    else if (MainEmblemCount > 0 && inventory[i].equiptType == Items.Item.EquiptType.MainEmblem)
                    {

                        CancelOptions.gameObject.SetActive(true);
                        CancelInfo.text = "Only One Main Emblem Item can be Equipt At Time /n Unequipt the Active One to Use this one";
                    }

                    else if (SubEmblemCount > 1 && inventory[i].equiptType == Items.Item.EquiptType.SubeEmblem)
                    {
                        CancelOptions.gameObject.SetActive(true);
                        CancelInfo.text = "Only 2  Emblem Upgrade Item can be Equipt At Time /n Unequipt the  One to Use this one";
                    }
                    else
                    {
                        // enable equipt pannel option
                        CancelOptions.gameObject.SetActive(false);
                        UseItemOptions.gameObject.SetActive(true);
                        OptionsInfo.text = "Equipt this Item?";
                    }
                //////////////////////////////////////////////////////

                ItemDescription.text = inventory[i].Description;
            }
                // if item is quest item or craft item
            else    if (inventory[i].itemNumber == id && inventory[i].type == Items.Item.ItemType.Quest )
                {
                  CancelOptions.gameObject.SetActive(true);
                 CancelInfo.text = "Cant Use this item!";
                ItemDescription.text = inventory[i].Description;
            }
                else if (inventory[i].itemNumber == id && inventory[i].type == Items.Item.ItemType.Craft)
                {
                    CancelOptions.gameObject.SetActive(true);
                    CancelInfo.text = "Cant Use this item!";
                ItemDescription.text = inventory[i].Description;
            }

            }
   
        }

    public void CancelSelection()
    {
        // disable options
        CancelInfo.text = "";
        CancelOptions.gameObject.SetActive(false);
        UseItemOptions.gameObject.SetActive(false);
        ItemDescription.text = "ITEM INFO:";
    }
}
