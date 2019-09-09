using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour {

    // handle menu selections
    public int MenuMaxCount;
    public int CurrentInputCount;
    public bool menuEnabled;

    public List<GameObject> EvenPannelSlots = new List<GameObject>();

    // pannels for handling
    // set functions for each to handeling enablein and disable functions within there script

    Inventory inventory;
    SimpleBattleHandler battleHandler;
    SimpleMove movement;
       
    // setup event manager controlls in movement
  

    public enum GameEvents
    {
        ChatBox,
        Inventory,
        Stats,
        Map,
        Options,
        BattleEvent,
        CinimaticEvent,
        StoryEvent,
        Store,
        Crafter,
        EventStore,
        inGame,
    }

    public GameEvents gameEvent;
	// Use this for initialization
	void Start () {
        inventory = FindObjectOfType<Inventory>();
        battleHandler = FindObjectOfType<SimpleBattleHandler>();
        movement = FindObjectOfType<SimpleMove>();
	}
	
	// Update is called once per frame
	void Update () {

        ChooseMenuByInput();
	}



    public void EnableMenu()
    {
        menuEnabled = true;
        EvenPannelSlots[0].gameObject.GetComponentInChildren<Button>().Select();
    }

    public void DisableMenu()
    {
        menuEnabled = false;
    }

    public void ChooseMenuByInput()
    {

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            CurrentInputCount += 1;
            if(CurrentInputCount > EvenPannelSlots.Count)
            {
                CurrentInputCount = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            CurrentInputCount -= 1;
            if (CurrentInputCount > 0)
            {
                CurrentInputCount = EvenPannelSlots.Count;
            }
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
        {
            CurrentInputCount += 1;
            if (CurrentInputCount > EvenPannelSlots.Count)
            {
                CurrentInputCount = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
        {
            CurrentInputCount -= 1;
            if (CurrentInputCount > 0)
            {
                CurrentInputCount = EvenPannelSlots.Count;
            }
        }
    }
    public void SwitchEvents(GameEvents EventState)
    {

        gameEvent = EventState;
        switch (EventState)
        {
            case GameEvents.ChatBox:
              
                break;

            case GameEvents.BattleEvent:
                break;
            case GameEvents.CinimaticEvent:
                break;
            case GameEvents.StoryEvent:
                break;
            case GameEvents.Inventory:
                break;
            case GameEvents.Stats:
                break;
            case GameEvents.Map:
                break;
            case GameEvents.Options:
                break;
            case GameEvents.Store:
                break;
            case GameEvents.Crafter:
                break;
            case GameEvents.EventStore:
                break;
            case GameEvents.inGame:
                break;
                
        }
    }



    
    }