using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NpcType : MonoBehaviour {

    private float currentTime;
    // battle stuff

    // chat stuff
    [Header("ChatPannel Info")]
    // new chat stuff
    int stringIndex = 0;
    int characterIndex = 0;
    bool canTalk = true;


    //
    public GameObject ChatPannel;
    public string[] talkLines;
    public string[] talkLines2;

    public int Scrollspeed = 50 ;
    private bool talking;
    public Text talkTextInfo;
 

    // TextInfo
    [Header("Quest Giver Also Common Info")]
    public int QuestGiverItemId;

    [Header("Store Info")]
    public int CostRangeForClerks = 50;
    public List<Items.Item> StoreItems = new List<Items.Item>();
    public List<GameObject> ItemSlots = new List<GameObject>();
    public GameObject ChatText;
    public GameObject StoreMenuPannel;
    public GameObject OptionPannel;
    public GameObject CancelPannel;
    public Text ItemDescription;

    [Header("Boss Info")]
    public int BossEnamyIndexId;



    [Header("Crafter Info")]
    // npc Crafting Stuff
    public int CraftItemsLimit = 2;
    public GameObject CraftPannel;
    public Text CraftInfoText;
    public GameObject CraftOptions;
    public GameObject CraftUnable;
    // lists
    public List<CraftItems.CraftItem> craftItems = new List<CraftItems.CraftItem>();
    public List<GameObject> craftSlots = new List<GameObject>();


    private int currentChosenItem;
    private bool inCloseView;
     
    public enum Npctype
    {
        commonNpc,
        StoreClerk,
        Crafter,
        BossEvent,
        QuestGiver

    }
    public Npctype npc;

    Player player;
    Inventory inventory;
    Movement move;
    Items items;
    BattleEvent battleM;
    Classes clas;
    CraftItems craft;
    private void Awake()
    {
        items = FindObjectOfType<Items>();
        player = FindObjectOfType<Player>();
        inventory = FindObjectOfType<Inventory>();
        move = FindObjectOfType<Movement>();
        battleM = FindObjectOfType<BattleEvent>();
        clas = FindObjectOfType<Classes>();
        craft = FindObjectOfType<CraftItems>();
    }
    // Use this for initialization
    void Start () {

      //  ChatPannel.gameObject.SetActive(true);
       // StartCoroutine(DisplayTimerTextScroll());
    }

    // Update is called once per frame
    void Update() {

        if (!inCloseView)
            return;
        //talkText();
        ChatCheckPressed();
        if (npc == Npctype.BossEvent || npc == Npctype.QuestGiver)
            return;
        // if event do function and return

        if (Input.GetKeyDown(KeyCode.E))
        {
            // exit store
            if (npc == Npctype.Crafter || npc == Npctype.StoreClerk)
            {
                EndNpcEvent();
            }
        }
     
            if (Input.GetKeyDown(KeyCode.N) && canTalk == true )
        {
            
          //  if (inventory.inventory.Contains(items.items[QuestGiverItemId]))
         //   {
          //      talkLines = talkLines2;
          //  }
          //  talkTriggered();
          
            ChatPannel.gameObject.SetActive(true);


        
            // exit store
            // do talk text first
            move.CanMove = false;
            Invoke("doThing", 1);

            // do this stuff in another funtions

        }
    }

    public void doThing()
    {
        // button sound
     
        StartCoroutine(DisplayTimerTextScroll());
    }
    // .....................................................................           choose type
    // happens after the talk text
    public void DoType()
    {
     
        if (npc == Npctype.StoreClerk)
        {
            ChatPannel.gameObject.SetActive(false);
            StoreMenuPannel.gameObject.SetActive(true);
            ConstructStoreSlots();
        }
        else if (npc == Npctype.Crafter)
        {
            ChatPannel.gameObject.SetActive(false);
            CraftPannel.gameObject.SetActive(true);
            ConstructCraftSlots();
        }
       else if (npc == Npctype.commonNpc)
        {
           
            if(currentChosenItem > 0)
            {
                GiveQuestItem();
            }
           // talkLines = talkLines2;
            EndNpcEvent();
            Debug.Log("did the event to turn it off");

        }
      else  if (npc == Npctype.QuestGiver)
        {
            if (inventory.inventory.Contains(items.items[QuestGiverItemId]))

                return;
           // ChatPannel.gameObject.SetActive(false);
            GiveQuestItem();
          //  talkLines = talkLines2;
            EndNpcEvent();
        }
     else   if (npc == Npctype.BossEvent)
        {
            ChatPannel.gameObject.SetActive(false);
            battleM.StartBattle(clas.Enamies[BossEnamyIndexId]);
            GiveQuestItem();
            EndNpcEvent();

        }
    }

    public void EndNpcEvent()
    {
        Debug.Log("Trund it off");
   
        ChatPannel.gameObject.SetActive(false);
   
        StoreMenuPannel.gameObject.SetActive(false);
        CraftPannel.gameObject.SetActive(false);
        currentChosenItem = 0;
        move.CanMove = true;
        stringIndex = 0;
    }
    public void ConstructStoreSlots()
    {
        StoreItems.Clear();
        for (int i = 0; i < items.items.Count; i++)
        {
            if(items.items[i].Cost <= CostRangeForClerks)
            {
                StoreItems.Add(items.items[i]);
                
            }
        }
            for (int i = 0; i < StoreItems.Count; i++)
        {

            ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = StoreItems[i].Name + " " + StoreItems[i].Cost + " :Cost";
            // add function to button when selected
            int itemNumber = StoreItems[i].itemNumber;
            ItemSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            ItemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => AddItem(itemNumber));
        }
    }

    public void AddItem(int id)
    {
      
        currentChosenItem = id;
     
        for (int i = 0; i < StoreItems.Count; i++)
        {

            if (StoreItems[i].itemNumber == id) {
                // if player has enuf
                if (player.Money >= StoreItems[i].Cost)
                {
                    // open pannel choice
                    ItemDescription.text = "Item Info:" + StoreItems[i].Description;
                 OptionPannel.gameObject.SetActive(true);
                }
                else if(player.Money < StoreItems[i].Cost)
                {
                    ItemDescription.text = "Item Info:" + StoreItems[i].Description;
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
            if(StoreItems[i].itemNumber == id)
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
        ItemDescription.text = "Item Info:";
        CancelPannel.gameObject.SetActive(false);
        OptionPannel.gameObject.SetActive(false);

    }

    // ........................................... trigger here
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
         
                 inCloseView = true;
            
            if(npc == Npctype.QuestGiver || npc == Npctype.BossEvent)
            {
                ChatPannel.gameObject.SetActive(true);



                // exit store
                // do talk text first
               
                move.CanMove = false;
                Invoke("doThing", 1);
            }
          

        }
    }

    public void GiveQuestItem()
    {

        inventory.addItem(QuestGiverItemId);
    }

    // ............................................................................... CRAFT STUFF
    public void ConstructCraftSlots()
    {
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
           craftSlots[i].gameObject.GetComponentInChildren<Text>().text =  craftItems[i].ItemName;
            // add function to button when selected
            int itemNumber = craftItems[i].ID;
            craftSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            craftSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => CraftItem(itemNumber));
        }
    }

    public void CraftItem(int id)
    {
       
        for (int i = 0; i < craftItems.Count; i++)
        {
           if(craftItems[i].ID == id)
            {
                currentChosenItem = craftItems[i].CraftedItemID;
               CraftInfoText.text = craftItems[i].Description;
                // craft takes 2 items
                if (craftItems[i].ItemIDs.Length == 2) {
                    int item1 = craftItems[i].ItemIDs[0];
                    int item2 = craftItems[i].ItemIDs[1];
               if (inventory.inventory.Contains(items.items[item1]) && inventory.inventory.Contains(items.items[item2]))
                    {
                        CraftChoicePannel();
                        // show pannel
                    }
                }
                // craft takes 3 items
                else if(craftItems[i].ItemIDs.Length == 3)
                {
                    int item1 = craftItems[i].ItemIDs[0];
                    int item2 = craftItems[i].ItemIDs[1];
                    int item3 = craftItems[i].ItemIDs[2];
                    if (inventory.inventory.Contains(items.items[item1]) && inventory.inventory.Contains(items.items[item2]) && inventory.inventory.Contains(items.items[item3]))
                    {
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


    //...................................................................................Text Talk stuff

 

    // new scroll text

    IEnumerator DisplayTimerTextScroll()
    {

        canTalk = false;
     

        while(stringIndex < talkLines.Length)
        {
           
            yield return new WaitForSeconds(0.1f);
        

                if (characterIndex > talkLines[stringIndex].Length - 1)
                {

                    continue;
                }
                talkTextInfo.text = talkLines[stringIndex].Substring(0, characterIndex);
                characterIndex++;
            
        }

    }
  
    public  void ChatCheckPressed()
    {
        if (Input.GetKeyDown(KeyCode.Space) && canTalk == false  )
        {
            if( stringIndex >= talkLines.Length  - 1)
            {
           
           
                
                DoType();
                stringIndex = 0;
                characterIndex = 0;
                canTalk = true;
        
            }
          else  if (characterIndex < talkLines[stringIndex].Length)
            {
                characterIndex = talkLines[stringIndex].Length;
            }
            else if (stringIndex < talkLines.Length)
            {
                stringIndex++;
               
                characterIndex = 0;
           
            }
          
        }
    }

}
