using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class simplePlayer : MonoBehaviour {

    // set from Start data
    public int GameID;
    public int playerID;

    // base player stats
    public string PlayerName;
    public string Description;
    public int AttackStat;
    public int DefenceStat;
    public int HealthStat;
    public int CurrentHealth ;
    public int SpeedStat;
    public int EvasionStat;
    public int AccuracyStat;
    public int SpAttackStat;
    public int SpDefenceStat;
    public Vector2 PlayerLocation;
    


    // stats 
    public int playerLevel;
    public int PointsToLevel;
    public int CurrentPointsToLevel;
    public int Money;
    public int Credits;

    // char
    public GameObject PlayerPrefab;
    public GameObject Player;
    public float PlayTime = 0.0f;
    // base info
    public Transform StartPointMarker;
    private int StartPointToLevel = 10;

    // save info
    // max saves are 5 for now


    // get the refrences in the game 
    simpleInventory Inventory;
    SimpleClasses classes;

	// Use this for initialization
	void Start () {
        // PlayerPrefs.DeleteAll();
        // get the refrences

        // undo later
       // PlayerPrefs.DeleteAll();
        Inventory = FindObjectOfType<simpleInventory>();
        classes = FindObjectOfType<SimpleClasses>();
        // set the SaveGame Data in the main menu that was selected Here << should be player prefs enabled
        // Data GameSlot,PlayerSelected << class should be enabled later on
        // this should be chaged to player pref data later and set in main menu
        GameID = 0;
        playerID = 0;
      GameID =  PlayerPrefs.GetInt("MenuLoadGameSlot");
        LoadGame(GameID);
    }
	
	// Update is called once per frame
	void Update () {
        PlayTime += 0.01f;
        // testing Save Game
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveGame(GameID);
        }
        // testing Loading Game
        if (Input.GetKeyDown(KeyCode.P))
        {
            LoadGame(GameID);
        }
        // testing delets all saved data
        if (Input.GetKeyDown(KeyCode.L))
        {
            PlayerPrefs.DeleteAll();
        }
    }

    // how this works is that the gameid is set at start from player prefs
    // then this function is loaded so it loads that game save file if it exists
    public void LoadGame(int gameID)
    {
        Debug.Log("Loading Game...");
        if(!PlayerPrefs.HasKey("SavedGame" + gameID.ToString()))
        {
            NewGame();
        }
        else
        {
            // ...........................LOADING INVENTORY DATA

            
            
            Inventory.playerInventory.Clear();

            for(int i = 0; i < Inventory.ItemSlots.Count; i ++)
            {
                Inventory.playerInventory.Add(Inventory.ItemsList[PlayerPrefs.GetInt("Item" + gameID.ToString() + i.ToString())]);
                Inventory.playerInventory[i].Count = PlayerPrefs.GetInt("ItemCount" + gameID.ToString() + i.ToString());

                // if i equipt item
                if (Inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.StatItem)
                {
                    // check if equipted if so set status
                    int isEquipted = PlayerPrefs.GetInt("Equipted" + gameID.ToString() + i.ToString());

                    if (isEquipted == 1)
                    {
                        Inventory.playerInventory[i].Equipted = true;
                    }
                    else if (isEquipted == 0)
                    {
                        Inventory.playerInventory[i].Equipted = false;
                    }
                }
        }
            // ...........................LOADING PLAYER POSITION

            if (Player != null)
            {
                Player = null;
            }
                StartPointMarker.position =  new Vector2(PlayerPrefs.GetFloat("PositionX" + gameID.ToString()), PlayerPrefs.GetFloat("PositionY" + gameID.ToString()));

            float xPos = PlayerPrefs.GetFloat("PositionX" + gameID.ToString());
            float yPos = PlayerPrefs.GetFloat("PositionY" + gameID.ToString());
            Vector2 playerPos = new Vector2(xPos, yPos);
            

            Player = Instantiate(PlayerPrefab, playerPos, StartPointMarker.rotation);
            // ...........................LOADING STAT DATA
               GameID = PlayerPrefs.GetInt("SavedGame" + gameID.ToString());
               CurrentHealth = PlayerPrefs.GetInt("CurrentHealth" + gameID.ToString());
               playerID = PlayerPrefs.GetInt("PlayerID" + gameID.ToString());
               PlayerName = PlayerPrefs.GetString("Name" + gameID.ToString());
               Description =  PlayerPrefs.GetString("Description" + gameID.ToString());
               Money = PlayerPrefs.GetInt("Money" + gameID.ToString());
               Credits = PlayerPrefs.GetInt("Credits" + gameID.ToString());
               AttackStat = PlayerPrefs.GetInt("Attack" + gameID.ToString());
               DefenceStat =  PlayerPrefs.GetInt("Defence" + gameID.ToString());
               HealthStat =  PlayerPrefs.GetInt("Health" + gameID.ToString());
               SpeedStat =  PlayerPrefs.GetInt("Speed" + gameID.ToString());
               playerLevel =  PlayerPrefs.GetInt("Level" + gameID.ToString());
               PointsToLevel =   PlayerPrefs.GetInt("PointsToLevel" + gameID.ToString());
               CurrentPointsToLevel =  PlayerPrefs.GetInt("CurrentPointsToLevel" + gameID.ToString());
            PlayTime = PlayerPrefs.GetFloat("PlayTime" + gameID.ToString());

        }
    }
    public void SaveGame(int gameID)
    {
        Debug.Log("Im Saveing Now...");
        //................................ SAVEING INVENTORY DATA HERE

        for (int i = 0; i < Inventory.ItemSlots.Count; i++)
        {
            PlayerPrefs.SetInt("Item" + gameID.ToString() + i.ToString(), Inventory.playerInventory[i].ID);
            PlayerPrefs.SetInt("ItemCount" + gameID.ToString() + i.ToString(), Inventory.playerInventory[i].Count);

            // ............... Check to see if it is a equipting stat item if so set its status
            if (Inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.StatItem)
            {
                // check t o see if item is equipted or not
                if (Inventory.playerInventory[i].Equipted == true)
                {
                    PlayerPrefs.SetInt("Equipted" + gameID.ToString() + i.ToString(), 1);
                }
                else
                {
                    PlayerPrefs.SetInt("Equipted" + gameID.ToString() + i.ToString(), 0);
                }
            }
        }

        //................................. SAVEING PLAYER STAT DATA
        PlayerPrefs.SetInt("SavedGame" + gameID.ToString(), gameID);
        PlayerPrefs.SetInt("CurrentHealth" + gameID.ToString(), CurrentHealth);
        PlayerPrefs.SetInt("PlayerID" + gameID.ToString(), playerID);
        PlayerPrefs.SetString("Name" + gameID.ToString(), PlayerName);
        PlayerPrefs.SetString("Description" + gameID.ToString(), Description);
        PlayerPrefs.SetInt("Money" + gameID.ToString(), Money);
        PlayerPrefs.SetInt("Credits" + gameID.ToString(), Credits);
        PlayerPrefs.SetInt("Attack" + gameID.ToString(), AttackStat);
        PlayerPrefs.SetInt("Defence" + gameID.ToString(), DefenceStat);
        PlayerPrefs.SetInt("Health" + gameID.ToString(), HealthStat);
        PlayerPrefs.SetInt("Speed" + gameID.ToString(), SpeedStat);
        PlayerPrefs.SetInt("Level" + gameID.ToString(), playerLevel);
        PlayerPrefs.SetInt("PointsToLevel" + gameID.ToString(), PointsToLevel);
        PlayerPrefs.SetInt("CurrentPointsToLevel" + gameID.ToString(), CurrentPointsToLevel);

        //................................. SAVEING PLAYER POSITION
        PlayerPrefs.SetFloat("PositionX" + gameID.ToString(), Player.transform.position.x);
        PlayerPrefs.SetFloat("PositionY" + gameID.ToString(), Player.transform.position.y);
        PlayerPrefs.SetFloat("PlayTime" + gameID.ToString(), PlayTime);

    }

    public void NewGame()
    {
       if(Player != null)
        {
            Destroy(Player.gameObject);
        }
        // instatiates the player into the scene at the set begining start point
        Player = Instantiate(PlayerPrefab, StartPointMarker.position, StartPointMarker.rotation);
      
   
        // add the base player stats to the new player
        // for now set a single base player so we set it to 0 for first player

      playerLevel = classes.players[playerID].Level;
        PointsToLevel = StartPointToLevel;
        CurrentPointsToLevel = 0;

        // stats
        PlayerName = classes.players[playerID].PlayerName;
        Description = classes.players[playerID].PlayerDescription;
        AttackStat = classes.players[playerID].Attack;
        DefenceStat = classes.players[playerID].Defence;
        SpeedStat = classes.players[playerID].Speed;
        HealthStat = classes.players[playerID].Health;
        CurrentHealth = HealthStat;

        // general data
        Money = 0;
        Credits = 0;
    }



    // seting up save
    // each save game state will have a number
    // each number will have a set sat data 
}
