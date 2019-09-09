using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestMenu : MonoBehaviour {


    // these object must be place where player 
    // cant go unless player has a sertain quest item
    // they will be disable when player has the item
    public GameObject Obstical1;
    public GameObject Obstical2;
    public GameObject Obstical3;
    public GameObject Obstical4;
    public GameObject Obstical5;

    public int QuestItem1;
    public int QuestItem2;
    public int QuestItem3;
    public int QuestItem4;
    public int QuestItem5;


    public string QuestDescription1;
    public string QuestDescription2;
    public string QuestDescription3;
    public string QuestDescription4;
    public string QuestDescription5;

    public GameObject QuestLocImage1;
    public GameObject QuestLocImage2;
    public GameObject QuestLocImage3;
    public GameObject QuestLocImage4;
    public GameObject QuestLocImage5;
    public Text QuestDesciptionText;

    public List<GameObject> Objectives = new List<GameObject>();
    public List<Items.Item> ObjectiveList = new List<Items.Item>();

    // refrences
   Inventory inventory;
    GamManager gManager;
    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        gManager = FindObjectOfType<GamManager>();
        gManager.SwitchCanvases(GamManager.ActiveCanvas.quest);
       
        gManager.SwitchCanvases(GamManager.ActiveCanvas.inGame);
    }
    // Use this for initialization
    void Start () {
        
        EnableQuestItems();
      // CheckQuestItems();
       // AddInventoryItemsToList();
      Invoke("CheckQuestItems", 1f);
      
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.W))
        {
            AddInventoryItemsToList();
        }
    }

    public void EnableQuestItems()
    {
        Obstical1.gameObject.SetActive(true);
        Obstical2.gameObject.SetActive(true);
        Obstical3.gameObject.SetActive(true);
        Obstical4.gameObject.SetActive(true);
        Obstical5.gameObject.SetActive(true);
    }


    public void AddInventoryItemsToList()
    {
        ObjectiveList.Clear();
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            if (inventory.inventory[i].type == Items.Item.ItemType.Quest)
            {
                ObjectiveList.Add(inventory.inventory[i]);
            }

        }
        ReconstructQuestSlots();
    }

    // construct the list of objective onl the lots
    public void ReconstructQuestSlots()
    {
      //  gManager.SwitchCanvases(GamManager.ActiveCanvas.quest);
        for (int i = 0; i < ObjectiveList.Count; i++)
        {
           
                Objectives[i].gameObject.GetComponentInChildren<Text>().text = ObjectiveList[i].Name ;
            int itemNumber = ObjectiveList[i].itemNumber;
            Objectives[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            Objectives[i].GetComponentInChildren<Button>().onClick.AddListener(() => displayQuest(itemNumber));
        }
        
    }

    public void CheckQuestItems()
    {
      
            for (int i = 0; i < inventory.itemSlots.Count; i++)
            {
                // obstical 1
                if (inventory.inventory[i].itemNumber == QuestItem1)
                {
                    Obstical1.gameObject.SetActive(false);
                }
                // obstical 2
                if (inventory.inventory[i].itemNumber == QuestItem2)
                {
                    Obstical2.gameObject.SetActive(false);
                }
                // obstical 3
                if (inventory.inventory[i].itemNumber == QuestItem3)
                {
                    Obstical3.gameObject.SetActive(false);
                }
                // obstical 4
                if (inventory.inventory[i].itemNumber == QuestItem4)
                {
                    Obstical4.gameObject.SetActive(false);
                }
                // obstical 5
                if (inventory.inventory[i].itemNumber == QuestItem5)
                {
                    Obstical5.gameObject.SetActive(false);
                }
            }
        
    }

    // this what happe when button is pressed
    // display the quest and the location
    public void displayQuest(int id)
    {
        foreach (Items.Item quest in ObjectiveList) {
            if (id == quest.itemNumber) {
                // get from item description
              //  QuestDesciptionText.text = quest.Description;
                // other stuff
                //nub 1
                if (quest.itemNumber == QuestItem1)
                {
                    // display  quest location
                    QuestLocImage1.gameObject.SetActive(true);
                    QuestLocImage2.gameObject.SetActive(false);
                    QuestLocImage3.gameObject.SetActive(false);
                 QuestLocImage4.gameObject.SetActive(false);
                  QuestLocImage5.gameObject.SetActive(false);

                    // quest description text
                    QuestDesciptionText.text = QuestDescription1;



                }

                //nub 2
                if (quest.itemNumber == QuestItem2)
                {
                    // display  quest location
                    QuestLocImage1.gameObject.SetActive(false);
                    QuestLocImage2.gameObject.SetActive(true);
                    QuestLocImage3.gameObject.SetActive(false);
                   QuestLocImage4.gameObject.SetActive(false);
                  QuestLocImage5.gameObject.SetActive(false);

                    // quest description text
                    QuestDesciptionText.text = QuestDescription2;

                }

                //nub 3
                if (quest.itemNumber == QuestItem3)
                {
                    // display  quest location
                    QuestLocImage1.gameObject.SetActive(false);
                    QuestLocImage2.gameObject.SetActive(false);
                    QuestLocImage3.gameObject.SetActive(true);
                  QuestLocImage4.gameObject.SetActive(false);
                  QuestLocImage5.gameObject.SetActive(false);

                    // quest description text
                    QuestDesciptionText.text = QuestDescription3;

                }

                //nub 4
                if (quest.itemNumber == QuestItem4)
                {
                    // display  quest location
                    QuestLocImage1.gameObject.SetActive(false);
                    QuestLocImage2.gameObject.SetActive(false);
                    QuestLocImage3.gameObject.SetActive(false);
                QuestLocImage4.gameObject.SetActive(true);
                   QuestLocImage5.gameObject.SetActive(false);

                    // quest description text
                    QuestDesciptionText.text = QuestDescription4;

                }

                //nub 5
                if (quest.itemNumber == QuestItem5)
                {
                    // display  quest location
                    QuestLocImage1.gameObject.SetActive(false);
                    QuestLocImage2.gameObject.SetActive(false);
                    QuestLocImage3.gameObject.SetActive(false);
                   QuestLocImage4.gameObject.SetActive(false);
                 QuestLocImage5.gameObject.SetActive(true);

                    // quest description text
                    QuestDesciptionText.text = QuestDescription5;

                }

                // add image of location and add item inventory and display it in map
            }
                }

      
    }
    // for each quest item it allow the player to go to a new location wich removes obsticals for playe 
    // to go so check weather or not a sertain item the player has if so it disables object that block paths
}
