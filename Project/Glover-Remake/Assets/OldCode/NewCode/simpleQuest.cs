using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class simpleQuest : MonoBehaviour {


    public GameObject QuestPannel;
    public Text QuestInfo;
    public List<GameObject> Obsticals = new List<GameObject>();
        [TextArea(3,20)]
    public List<string> Descriptions = new List<string>();
    public List<string> QuestNames = new List<string>();
    public List<GameObject> CheckPoints = new List<GameObject>();
    public List<GameObject> QuestSlots = new List<GameObject>();
    public int SelectedItem;
    public int QuestCount;
    simpleInventory inventory;
    SimpleMove move;
    public bool inQuest;

   
    // Use this for initialization
    void Start() {
        QuestCount = 0;
        SelectedItem = 0;
        inQuest = false;
        inventory = FindObjectOfType<simpleInventory>();

        Invoke("SetOpsticals", 2f);
        Invoke("setMovement", 3f);

    }

    // Update is called once per frame
    void Update()
    {
        if (inQuest == false && Input.GetKeyDown(KeyCode.U) && move.canMove == true)
        {
            inQuest = true;
            QuestPannel.gameObject.SetActive(true);
            SetQuestSlots();
            SelectedItem = 0;
            
            QuestSlots[SelectedItem].gameObject.GetComponent<Button>().Select();
            move.canMove = false;
        }else if(inQuest == true && Input.GetKeyDown(KeyCode.U) && move.canMove == false)
        {
            inQuest = false;
            QuestPannel.gameObject.SetActive(false);
            move.canMove = true;

        }

        if (inQuest)
        {
            SelectSlots();
        }
    }

    public void SetOpsticals()
    {
      
       foreach(GameObject obstical in Obsticals)
        {
            obstical.gameObject.SetActive(true);
        }
       
       for(int i = 0; i < inventory.ItemSlots.Count; i++)
        {
            for(int a = 0; a < Obsticals.Count; a++)
            {
                if(inventory.playerInventory[i].QuestObstical == a)
                {
                    // if it is we will disable the obstical
                    Obsticals[a].gameObject.SetActive(false);
                // also check if is completed we will disable quest
             
                }
            }
        }
    }

    public void SetQuestSlots()
    {
        QuestCount = 0;
        for (int i = 0; i < QuestSlots.Count; i++)
        {
            for(int a = 0; a < inventory.playerInventory.Count; a++)
            {
                if(inventory.playerInventory[a].QuestObstical == i && inventory.playerInventory[a].Type == simpleInventory.Item.ItemType.QuestItem)
                {
                    QuestCount += 1;

                    if (inventory.playerInventory[a].Completed == true) {
                        QuestSlots[i].gameObject.GetComponentInChildren<Text>().text = QuestNames[i] + " :COMPLETED";
                        

                    }
                    else
                    {
                        QuestSlots[i].gameObject.GetComponentInChildren<Text>().text = QuestNames[i] + " :QUEST";

                    }

                }
            }
        }
        Debug.Log("SettQuestSlots");
        Debug.Log("QuestCount:" + QuestCount.ToString());
    }

    public void SelectSlots()
    {

        // go up and right selecting the items in the inventory 
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
        {
            SelectedItem += 1;
                if(QuestCount == 0)
            {
                SelectedItem = 0;
            }
            if (SelectedItem > QuestCount - 1)
            {
                SelectedItem = 0;
            }
            QuestSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

            // add the item description here                                           
            QuestInfo.text = Descriptions[SelectedItem];       // <<<<<<< added new code here

            // set the checkpoints
            foreach (GameObject checkpoint in CheckPoints)
            {
                checkpoint.gameObject.SetActive(false);
                if (checkpoint == CheckPoints[SelectedItem])
                {
                    checkpoint.gameObject.SetActive(true);
                }
            }
            Debug.Log("SelectedItem:" + SelectedItem.ToString());
        }

        // go down and left selecting the items in the inventory
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
        {
            SelectedItem -= 1;
            if (QuestCount == 0)
            {
                SelectedItem = 0;
            }
            if (SelectedItem < 0)
            {
                SelectedItem = QuestCount - 1;
            }
            QuestSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

            // add the item description here                                             
            QuestInfo.text = Descriptions[SelectedItem];    //   <<<<<<< added new code 
            // set the check point to the quest
            foreach(GameObject checkpoint in CheckPoints)
            {
                checkpoint.gameObject.SetActive(false);
                if(checkpoint == CheckPoints[SelectedItem])
                {
                    checkpoint.gameObject.SetActive(true);
                }
            }
            Debug.Log("SelectedItem:" + SelectedItem.ToString());

        }


        // press space bar to use the item that is selected
        if (Input.GetKeyDown(KeyCode.B))
        {
            //  useItem(SelectedItem);

        }
    }

    public void setMovement()
    {
        move = FindObjectOfType<SimpleMove>();
    }
    
   
}
