using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamManager : MonoBehaviour {

    public GameObject MenuCanvas;
    public GameObject InventoryCanvas;
    public GameObject StateCanvas;
    public GameObject QuestCanvas;
    public GameObject OptionCanvas;
  

    public enum ActiveCanvas
    {
     inGame,
     menu,
     inventory,
     stats,
     quest,
     options,
     exit
   
    }
    public ActiveCanvas canvas;

     Inventory inventory;
     StatsMenu stats;
   QuestMenu quest;

    private void Awake()
    {
        inventory = FindObjectOfType<Inventory>();
        stats = FindObjectOfType<StatsMenu>();
        quest = FindObjectOfType<QuestMenu>();
    }
    // Use this for initialization
    void Start () {
        

        doStart();
      
	}

    public void doStart()
    {
        SwitchCanvases(ActiveCanvas.inGame);
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.I))
        {
            SwitchCanvases(ActiveCanvas.menu);
        }
	}

    // MainMenu Button options


        public void LoadSavedInfo()
    {
        SwitchCanvases(ActiveCanvas.inventory);
        inventory.ReconstructInventory();
        SwitchCanvases(ActiveCanvas.quest);
        quest.AddInventoryItemsToList();
        SwitchCanvases(ActiveCanvas.stats);
        stats.AddInventoryItemsToList();
    }
    public void EnableOptions()
    {
        SwitchCanvases(ActiveCanvas.menu);
        
    }

    // sub menu options
    public void OnSelectInventory()
    {
        SwitchCanvases(ActiveCanvas.inventory);
        inventory.ReconstructInventory();

    }
    public void OnSelectQuest()
    {
        SwitchCanvases(ActiveCanvas.quest);
        quest.AddInventoryItemsToList();
       
    }
    public void OnSelectStats()
    {
        SwitchCanvases(ActiveCanvas.stats);
        stats.AddInventoryItemsToList();
    }
    public void OnSelectOptions()
    {
        SwitchCanvases(ActiveCanvas.options);
    }
    public void OnSelectExit()
    {
        // start main menu
    }

   public void SwitchCanvases(ActiveCanvas act)
    {
        canvas = act;
        switch (act)
        {
            case ActiveCanvas.inGame:
                MenuCanvas.gameObject.SetActive(false);
                InventoryCanvas.gameObject.SetActive(false);
                StateCanvas.gameObject.SetActive(false);
                QuestCanvas.gameObject.SetActive(false);
                OptionCanvas.gameObject.SetActive(false);
                if(inventory != null) {
                inventory.inInventory = false;
                }

                break;
            case ActiveCanvas.menu:
                MenuCanvas.gameObject.SetActive(true);
                break;
            case ActiveCanvas.inventory:
                InventoryCanvas.gameObject.SetActive(true);
                StateCanvas.gameObject.SetActive(false);
                QuestCanvas.gameObject.SetActive(false);
                OptionCanvas.gameObject.SetActive(false);
                break;
            case ActiveCanvas.stats:
                InventoryCanvas.gameObject.SetActive(false);
                StateCanvas.gameObject.SetActive(true);
                QuestCanvas.gameObject.SetActive(false);
                OptionCanvas.gameObject.SetActive(false);
                break;
            case ActiveCanvas.quest:
                InventoryCanvas.gameObject.SetActive(false);
                StateCanvas.gameObject.SetActive(false);
                QuestCanvas.gameObject.SetActive(true);
                OptionCanvas.gameObject.SetActive(false);
                break;
            case ActiveCanvas.options:
                InventoryCanvas.gameObject.SetActive(false);
                StateCanvas.gameObject.SetActive(false);
                QuestCanvas.gameObject.SetActive(false);
                OptionCanvas.gameObject.SetActive(true);
                break;
            case ActiveCanvas.exit:
                // go back to main menu
                break;
        }
    }
     
}
