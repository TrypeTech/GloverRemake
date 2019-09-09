using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simpleInventory : MonoBehaviour {
    // for this inventory to work you need a pannel that contains all of your slots
    // add Button to the slot and be sure to position the text correctly, also add an image to the slot
    // fill the inventory pannel with the slots the way you want your inventory
    // the way this inventory works is it creates and empty item first for each slot so they have 
    // something in them but dose nothing. It is the item 0.
    // you can select wich slot you want to choose by going up or down the inventory item index number
    // using the up, down, left ,right  keys or w a s d
    // you can toggle on or of the inventory by pressing I
    // You can equipt add usable items to the player usables are destroyed when they are no more
    // you can add new stats to the player in the Item class but make sure you update them in the useItem function
    // so they are added to the player.
    // NOTE: when the inventory is full there is no functionality yet to remove items by choice in game so make sure
    // there are enuf slots for all your equipt items and usable items and add remove item player choice option.
    // other than that idk.

    // also I could not add mouse click button functionality into this script will try
    // tommorow so if you click button with mouse it wont do anything.

    public List<Item> ItemsList = new List<Item>();
    public List<Item> playerInventory = new List<Item>();
    public List<GameObject> ItemSlots = new List<GameObject>();

    public int MaxPlayerInventoryCount = 5;

    public int SelectedItem;
    public bool inInventory;
    public GameObject InventoryPannel;
    public simplePlayer player;
    SimpleMove move;
    

    // Use this for initialization
    void Start() {
        player = FindObjectOfType<simplePlayer>();
        SelectedItem = 0;
        ConstructInventory();

        // test adding items you can remove when done
        addItem(1);
        addItem(5);
       
        addItem(3);
        
        addItem(6);
        addItem(7);

    }



    // Update is called once per frame
    void Update() {

        if (Input.GetKeyDown(KeyCode.Q))

        {
            addItem(5);
        }
        // if inventory is on turn it off
        if (Input.GetKeyDown(KeyCode.I))
        {
            move = FindObjectOfType<SimpleMove>();
                       

                if (inInventory == true && move.canMove == false)
                {
                    move.canMove = true;
                    inInventory = false;
                    InventoryPannel.gameObject.SetActive(false);

                }
            
            // if inventory is off turn it on
            else if (inInventory == false && move.canMove == true)
            {
                move.canMove = false;
                inInventory = true;
                InventoryPannel.gameObject.SetActive(true);
                ConstructSlots();
            }
            
            
        }


        // manages the selection of the inventory
        if (inInventory == true)
        {
            SelectSlots();

        }
    }

   // add the slot prefab << you can add this at the top
  //  public  GameObject slotPrefab;

    // constructs the database of items at the begining of the game
    public void ConstructInventory()
    {
        // add the database of items 
        addItems();

        // enable the inventory pannel to be enabled else it will be null
        // when doing the for loop
    //    InventoryPannel.gameObject.SetActive(true);
        // set if inventory is empty at start
        for (int i = 0; i < MaxPlayerInventoryCount; i++)
        {
          //  ItemSlots.Add(Instantiate(slotPrefab, InventoryPannel.transform, false));
            playerInventory.Add(ItemsList[0]);
        }

        // disable the  pannel again because we just needed to add the slots
    ///    InventoryPannel.gameObject.SetActive(false);
    }


    // adds item by id to the player inventory
    public void addItem(int ItemId)
    {

        // player already has items just add another one
        for (int i = 0; i < playerInventory.Count; i++)
        {
            if (playerInventory[i].ID == ItemId)
            {
                playerInventory[i].Count += 1;

                // reconstruct the enventory so the change  can be displayed
                if (inInventory == true)
                {
                    ConstructSlots();
                }
                return;
            }

        }



        if (ItemsList[ItemId] != null)
        {
            for (int i = 0; i < MaxPlayerInventoryCount; i++)
            {
                if (playerInventory[i].ID == 0)
                {
                    playerInventory.Insert(i, ItemsList[ItemId]);

                    // reconstruct the inventory so the change can be displayed
                    if (inInventory == true)
                    {
                        ConstructSlots();
                    }
                    Debug.Log("Added Item " + playerInventory[i].Name);
                    break;
                }

                if (i == MaxPlayerInventoryCount)
                {
                    // Player inventory is full
                    Debug.Log("Player Inventory is full");
                }
            }

        }


    }

    // removes item at index number
    public void RemoveItem(int index)
    {

        playerInventory.RemoveAt(index);
        playerInventory.Add(ItemsList[0]);
        ConstructSlots();

    }


    // this function reconstructs the slot so it updated visually for the player
    public void ConstructSlots()
    {
        for (int i = 0; i < MaxPlayerInventoryCount; i++)
        {

            if (playerInventory[i].Equipted == true)
            {
                ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = playerInventory[i].Name + ":" + "EPTD" + " " + playerInventory[i].Count.ToString();
            }
            else if (playerInventory[i].Equipted == false)
            {
                ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = playerInventory[i].Name + " " + playerInventory[i].Count.ToString();
            }
            // add icon that should be in your resources folder that should be indexed in you database where it is with itemLoc
            if (ItemSlots[i].gameObject.GetComponentInChildren<Image>().sprite != null)
            {
                ItemSlots[i].gameObject.GetComponentInChildren<Image>().sprite = playerInventory[i].Icon as Sprite;
            }

            // this add function to button so when it is click use the useItem function << There are some issue with this causeing double items 
            // selected and I need to create a whole new function for this because it handles selecting and choseing items diffrently then other method of using item.
            // but will add work around later
            ItemSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            int index = i;
            
            ItemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => useItem(index));  
        }
        ItemSlots[0].gameObject.GetComponent<Button>().Select();


    }

    public Text ItemInfoText;             // <<<< added new code here
    // this function is placed in update to select between the slots
    public void SelectSlots()
    {

        // go up and right selecting the items in the inventory 
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow )|| Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
        {
            SelectedItem += 1;
                if(MaxPlayerInventoryCount == 0)
            {
                SelectedItem = 0;
            }
            if(SelectedItem > MaxPlayerInventoryCount - 1)
            {
                SelectedItem = 0;
            }
            ItemSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

            // add the item description here                                           
            ItemInfoText.text = playerInventory[SelectedItem].Description;       // <<<<<<< added new code here
        }

        // go down and left selecting the items in the inventory
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            SelectedItem -= 1;

            if(MaxPlayerInventoryCount == 0)
            {
                SelectedItem = 0;
            }
            if(SelectedItem < 0)
            {
                SelectedItem = MaxPlayerInventoryCount -1;
            }
              ItemSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

            // add the item description here                                             
            ItemInfoText.text = playerInventory[SelectedItem].Description;    //   <<<<<<< added new code here
        }


        // press space bar to use the item that is selected
        if (Input.GetKeyDown(KeyCode.B))
        {
            // it selects it twise becuse it prsses button space
            // wich triggers selected ui elementa and also uses function
         useItem(SelectedItem );

        }
    }

    public void useItem(int index)
    {

     

        //if item is equiptable equipt the item

        if (playerInventory[index].Type == Item.ItemType.StatItem && playerInventory[index].Equipted == true)
        {
            Debug.Log("equipted item named " + playerInventory[index].Name);
            // remove stat changes
            ItemSlots[index].gameObject.GetComponentInChildren<Text>().text = playerInventory[index].Name + " " + playerInventory[index].Count.ToString();
            playerInventory[index].Equipted = false;
            player.AttackStat -= playerInventory[index].AttackStat;
            player.DefenceStat -= playerInventory[index].DefenceStat;
            player.HealthStat -= playerInventory[index].StaminaStat;
        }

        else if (playerInventory[index].Type == Item.ItemType.StatItem && playerInventory[index].Equipted == false) {

            Debug.Log("Unequipted item named " + playerInventory[index].Name);
            // apply state changes
            // display changes
            ItemSlots[index].gameObject.GetComponentInChildren<Text>().text = playerInventory[index].Name + ":EPTD" + " " + playerInventory[index].Count.ToString();
            playerInventory[index].Equipted = true;
            player.AttackStat += playerInventory[index].AttackStat;
            player.DefenceStat += playerInventory[index].DefenceStat;
            player.HealthStat += playerInventory[index].StaminaStat;

            

        }


            // if item is usable use the item
            if (playerInventory[index].Type == Item.ItemType.UsableItem)
        {
            // apply the state changes
            Debug.Log("Usable item used named " + playerInventory[index].Name);
            player.AttackStat += playerInventory[index].AttackStat;
            player.DefenceStat += playerInventory[index].DefenceStat;
           // player.HealthStat += playerInventory[index].StaminaStat;
            player.CurrentHealth += playerInventory[index].StaminaStat;

            if(player.CurrentHealth > player.HealthStat)
            {
                player.CurrentHealth = player.HealthStat;
            }
            // remove item if count is less than one
            playerInventory[index].Count -= 1;

            // display that the number decreased
            ItemSlots[index].gameObject.GetComponentInChildren<Text>().text = playerInventory[index].Name + " " + playerInventory[index].Count.ToString();
            if (playerInventory[index].Count <= 0)
            {
                Debug.Log("Removed" + playerInventory[index].Name);
                RemoveItem(index);
                ItemSlots[0].gameObject.GetComponent<Button>().Select();
                SelectedItem = 0;
            }

            
       
        }

        // if item is quiest item so unusable
        if (playerInventory[index].Type == Item.ItemType.QuestItem)
        {

            Debug.Log("this is Quest item");
        }


        if (playerInventory[index].ID == 0)
        {
            Debug.Log("Emtpy Item");
        }
        else
        {
         
        }
        ItemSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

        ItemInfoText.text = playerInventory[0].Description;
    }

    // creats the base classs for items and all there properties
    public class Item
    {
        public  int ID { get; set; }
          public  string Name { get; set; }
         public  string Description { get; set; }
        public  int AttackStat { get; set; }
         public int DefenceStat { get; set; }
        public   int StaminaStat { get; set; }

        public    string IconLoc { get; set; }
        public    Sprite Icon { get; set; }

        public enum EquiptType
        {
            MainAttacker,
            SubAttacker,
            MinAttacker,
            MainShield,
            SubShield,
            MinShield,
            MainEmblem,
            SubEmblem,
            MinEmblem,
            none

        }

        public EquiptType Etype;
      public  enum ItemType
        {
            StatItem,
            QuestItem,
            UsableItem,
            Empty
        }
      public  ItemType Type { get; set; }
        public bool Equipted { get; set; }
        public int Count { get; set; }
        public int QuestObstical { get; set; }
        public bool  Completed { get; set; }
        public int Cost { get; set; }

      public  Item (int id,string name,string desc,int attack,int defence, int stamina, ItemType type,string loc,bool equipt,int count, EquiptType etype,int quest,bool comp,int cost)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.AttackStat = attack;
            this.DefenceStat = defence;
            this.StaminaStat = stamina;
            this.Type = type;
            this.Icon = Resources.Load(loc) as Sprite;
            this.Equipted = equipt;
            this.Count = count;
            this.Etype = etype;
            this.QuestObstical = quest;
            this.Completed = comp;
            this.Cost = cost;
        }

        

    }

    // item database
    public void addItems()
    {
        // each items should had a diffrent ID  example 1,2,3 so you know which one is wich
        ItemsList.Add(new Item(0, "Empty", "EmptyItem", 0, 0, 0, Item.ItemType.Empty, "",false,0, Item.EquiptType.none,0,false,10));
        ItemsList.Add(new Item(1, "SimpleSword", "A Sword of Simple Power", 10, 0, 0, Item.ItemType.StatItem, "",false,1,Item.EquiptType.MainAttacker,0,false,10));
        ItemsList.Add(new Item(2, "SimpleShield", "A Shield of Honor", 0, 10, 0, Item.ItemType.StatItem, "",false,1, Item.EquiptType.MainShield,0,false,10));
        ItemsList.Add(new Item(3, "SimpleGarment", "A garment wich Promotes health", 0, 0, 3, Item.ItemType.StatItem, "",false,1, Item.EquiptType.MainEmblem,0,false,10));
        ItemsList.Add(new Item(4, "Potion", "potion that Promotes health", 0, 0,10, Item.ItemType.UsableItem, "", false, 1, Item.EquiptType.none,0,false,10));
        ItemsList.Add(new Item(5, "MaxPotion", "potion that Promotes health", 0, 0, 20, Item.ItemType.UsableItem, "", false, 1, Item.EquiptType.none,0,false,33));

        ItemsList.Add(new Item(6, "GobbleEmblem", "potion that Promotes health", 0, 0, 20, Item.ItemType.QuestItem, "", false, 1, Item.EquiptType.none,0,false,33));
        ItemsList.Add(new Item(7, "GreatKeyOfMordor", "potion that Promotes health", 0, 0, 20, Item.ItemType.QuestItem, "", false, 1, Item.EquiptType.none,1,false,33));


    }

}
