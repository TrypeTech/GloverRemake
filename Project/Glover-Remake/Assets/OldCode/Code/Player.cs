using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;


public class Player : MonoBehaviour {


    public string playerName;
    // player data
    public GameObject player;
    public Transform playerStartPosition;

    public int playerID;
    // playerStatsInGame
    public int Money;
    public int Level;
    public int maxHp;
    public int Hp;
    public int Attack;
    public int Defence;
    public int Speed;
    public int Evasion;
    public int Accuracy;
    public int SpDef;
    public int SpAttack;

    // level Stats
    public int PointsToLevel;
    public int CurrentLevelPoints;

    public GamManager gManager;
    public Inventory inventory;
    public Items item;
    public Classes clas;

	// Use this for initialization
	void Start () {
        gManager = FindObjectOfType<GamManager>();
        inventory = FindObjectOfType<Inventory>();
        item = FindObjectOfType<Items>();
        clas = FindObjectOfType<Classes>();
        Level = 10;
        Hp = 100;
        Attack = 10;
        Defence = 10;
        // PlayerPrefs.DeleteAll();
        Invoke("LoadPlayerGameData" ,0.2f);
       // LoadPlayerGameData();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.O))
        {
            SaveGame();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            //  PlayerPrefs.DeleteAll();
            LoadPlayerGameData();
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            //  PlayerPrefs.DeleteAll();
            PlayerPrefs.DeleteAll();
        }
    }

    public void LoadPlayerGameData()
    {
        inventory.inventory.Clear();
        if (PlayerPrefs.HasKey("NewGame1"))
        {
            Debug.Log("LoadedGame");
            // add data

            // reconstruct inventory and add saved items to it

            // << need to do is add the stored items to the inventory  then create a function in inventory that reconstructs it


            //......................................................... Load Inventory Data Here
            // set player id 
            playerID = PlayerPrefs.GetInt("PlayerID");
            for (int i = 0; i < inventory.itemSlots.Count ; i++)
            {
                inventory.inventory.Add(item.items[PlayerPrefs.GetInt("Item" + i.ToString())]);
                inventory.inventory[i].Count = PlayerPrefs.GetInt("ItemCount" + i.ToString());

                // equipting items based on 1 and zero value
                int isEquipted = PlayerPrefs.GetInt("Equipted" + i.ToString());
                if (isEquipted == 1)
                {
                    inventory.inventory[i].Equipted = true;
                }
                else if(isEquipted == 0)
                {
                    inventory.inventory[i].Equipted = false;
                }
              
            }
            // ..................................................... player position info
            player.transform.position = new Vector2(PlayerPrefs.GetFloat("PlayerXPosition"), PlayerPrefs.GetFloat("PlayerYPosition"));
            //.........................................................    Load Stat Data Here
            Money = PlayerPrefs.GetInt("PlayerMoney");
            Level = PlayerPrefs.GetInt("PlayerLevel");
            Hp = PlayerPrefs.GetInt("PlayerHp");
            Attack = PlayerPrefs.GetInt("PlayerAttack");
            Defence = PlayerPrefs.GetInt("PlayerDefence");
            Speed = PlayerPrefs.GetInt("PlayerSpeed");
            Evasion = PlayerPrefs.GetInt("PlayerEvasion");
            Accuracy = PlayerPrefs.GetInt("PlayerAccuracy");
            SpAttack = PlayerPrefs.GetInt("PlayerSpAttack");
            SpDef = PlayerPrefs.GetInt("PlayerSpDefence");

            PointsToLevel = PlayerPrefs.GetInt("PointsToLevel");
            CurrentLevelPoints = PlayerPrefs.GetInt("CurrentLevelPoints");

        }
        else
        {
            NewGame();
        }
        gManager.SwitchCanvases(GamManager.ActiveCanvas.inGame);
    }

    public void NewGame()
    {

        player.gameObject.transform.position = new Vector2(playerStartPosition.position.x, playerStartPosition.position.y);

        Level = clas.players[playerID].Level;
        Hp = clas.players[playerID].Hp;
        Attack = clas.players[playerID].Attack;
        Defence = clas.players[playerID].Defence;
        Speed = clas.players[playerID].Speed;
        Evasion = clas.players[playerID].Evasion;
        Accuracy = clas.players[playerID].Accuracy;
        SpAttack = clas.players[playerID].SpAttack;
        SpDef = clas.players[playerID].SpDefence;
        // levling stuff
        PointsToLevel = 30;
        CurrentLevelPoints = 0;
        Money = 0;
        

    }


    public void SaveGame()
    {

        //.........................................................  Save Inventory Data Here

        PlayerPrefs.SetInt("PlayerID", playerID);
        // save new game in slot 1
        PlayerPrefs.SetInt("NewGame1", 1);
        Debug.Log("Saved Game");
        // save inventory items and there locations
        for (int i = 0; i < inventory.itemSlots.Count ; i++)
        {
            PlayerPrefs.SetInt("Item" + i.ToString(), inventory.inventory[i].itemNumber);
            PlayerPrefs.SetInt("ItemCount" + i.ToString(), inventory.inventory[i].Count);
            
            // check to see if item is equipted if not set it to 1 or 0
            if(inventory.inventory[i].Equipted == true)
            {
                PlayerPrefs.SetInt("Equipted" + i.ToString(), 1);
            }else
            {
                PlayerPrefs.SetInt("Equipted" + i.ToString(), 0);
            }
         
        }
        // ....................................................... Load Player Location Data
      //  player.gameObject.transform.position = new Vector2(playerStartPosition.position.x, playerStartPosition.position.y);
        PlayerPrefs.SetFloat("PlayerXPosition", player.transform.position.x);
        PlayerPrefs.SetFloat("PlayerYPosition", player.transform.position.y);

        //......................................................... Save Stat Data Here

        PlayerPrefs.SetInt("PlayerMoney", Money);
        PlayerPrefs.SetInt("PlayerLvl", Level);
        PlayerPrefs.SetInt("PlayerHp", Hp);
        PlayerPrefs.SetInt("PlayerAttack", Attack);
        PlayerPrefs.SetInt("PlayerDefence", Defence);
        PlayerPrefs.SetInt("PlayerSpeed", Speed);
        PlayerPrefs.SetInt("PlayerEvasion", Evasion);
        PlayerPrefs.SetInt("PlayerAccuracy", Accuracy);
        PlayerPrefs.SetInt("PlayerSpDefence", SpDef);
        PlayerPrefs.SetInt("PlayerSpAttack", SpAttack);

        PlayerPrefs.SetInt("PointsToLevel", PointsToLevel);
        PlayerPrefs.SetInt("CurrentLevelPoints", CurrentLevelPoints);
   
    }
}
