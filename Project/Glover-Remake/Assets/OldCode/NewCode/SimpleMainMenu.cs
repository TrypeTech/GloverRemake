using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class SimpleMainMenu : MonoBehaviour {


    public List<GameObject> SaveSlots = new List<GameObject>();
    public List<GameObject> MenuObjects = new List<GameObject>();

    public GameObject LoadingPannel;
    public GameObject SaveSlotPannel;
    public GameObject CreditPannel;
    public GameObject MainMenuPannel;

    public int SelectedItem;
    public float LoadMenuTime = 5f;

    public enum SelectMenu
    {
        inMain,
        inSaves,
        inLoading,
        none
    }

    SelectMenu menus;
	// Use this for initialization
	void Start () {
        LoadMainMenu();
    }
	
	// Update is called once per frame
	void Update () {

        if(menus != SelectMenu.inMain || menus != SelectMenu.inLoading)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                LoadMainMenu();
            }
        }
        SelectSlots();
	}


    public void LoadMainMenu()
    {
        menus = SelectMenu.inMain;
        CreditPannel.gameObject.SetActive(false);
        MainMenuPannel.gameObject.SetActive(true);
        LoadingPannel.gameObject.SetActive(false);
        SaveSlotPannel.gameObject.SetActive(false);
        SelectedItem = 0;
        MenuObjects[SelectedItem].gameObject.GetComponent<Button>().Select();
    }
    public void LoadGame(int SaveSlot)
    {
        menus = SelectMenu.inLoading;
        PlayerPrefs.SetInt("MenuLoadGameSlot", SaveSlot);

        // enable load menu
        CreditPannel.gameObject.SetActive(false);
        MainMenuPannel.gameObject.SetActive(false);
        LoadingPannel.gameObject.SetActive(true);
        SaveSlotPannel.gameObject.SetActive(false);
        Invoke("WaitLoading", LoadMenuTime);
    }
    public void LoadCredits()
    {
        menus = SelectMenu.none;
        CreditPannel.gameObject.SetActive(true);
        MainMenuPannel.gameObject.SetActive(false);
        LoadingPannel.gameObject.SetActive(false);
        SaveSlotPannel.gameObject.SetActive(false);

    }
    public void ExitGame()
    {
        Application.Quit();
    }
    public void ContenuGame()
    {
        menus = SelectMenu.inSaves;
       
        CreditPannel.gameObject.SetActive(false);
        MainMenuPannel.gameObject.SetActive(false);
        LoadingPannel.gameObject.SetActive(false);
        SaveSlotPannel.gameObject.SetActive(true);
        ConstructSaveSlots();
    }
    public void StartNewGame()
    {
      
        for (int i = 0; i < SaveSlots.Count; i++)
        {
            if (!PlayerPrefs.HasKey("SavedGame" + i.ToString()))
            {
                LoadGame(i);
                break;
            }
        }
    }

    public void ConstructSaveSlots()
    {
        for (int i = 0; i < SaveSlots.Count; i++)
        {

            if (!PlayerPrefs.HasKey("SavedGame" + i.ToString()))
            {
                SaveSlots[i].gameObject.GetComponentInChildren<Text>().text = "[NewGame]" + "\n" + "GAMETIME:0000" + "\n" + "LOCATION:NONE";
            }

            else
            {
                SaveSlots[i].gameObject.GetComponentInChildren<Text>().text = "[GAME:" + i.ToString() + "\n" + "GAMETIME:" + PlayerPrefs.GetFloat("PlayTime" + i.ToString()).ToString() + "\n" + "LOCATION:NONE";
            }
            // but will add work around later
            SaveSlots[i].GetComponent<Button>().onClick.RemoveAllListeners();
            int index = i;

            SaveSlots[i].GetComponent<Button>().onClick.AddListener(() => LoadGame(index));
        }
        SelectedItem = 0;
        SaveSlots[SelectedItem].gameObject.GetComponent<Button>().Select();
    }



  public void WaitLoading()
    {
        // play load menu music
        SceneManager.LoadScene("BasicRPGMap");
    }



    // slot movement


    public void SelectSlots()
    {

        if (menus == SelectMenu.inMain)
        {
            // go up and right selecting the items in the inventory 
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                SelectedItem += 1;
                if (MenuObjects.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem > MenuObjects.Count - 1)
                {
                    SelectedItem = 0;
                }
                MenuObjects[SelectedItem].gameObject.GetComponent<Button>().Select();


            }

            // go down and left selecting the items in the inventory
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
            {
                SelectedItem -= 1;
                if (MenuObjects.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem < 0)
                {
                    SelectedItem = MenuObjects.Count - 1;
                }
                MenuObjects[SelectedItem].gameObject.GetComponent<Button>().Select();

            }


            // press space bar to use the item that is selected
            if (Input.GetKeyDown(KeyCode.B))
            {
                //  useItem(SelectedItem);

            }
        }

        //.......................................................................................... In Save Slots

        if (menus == SelectMenu.inSaves)
        {
            // go up and right selecting the items in the inventory 
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                SelectedItem += 1;
                if (SaveSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem > SaveSlots.Count - 1)
                {
                    SelectedItem = 0;
                }
                SaveSlots[SelectedItem].gameObject.GetComponent<Button>().Select();


            }

            // go down and left selecting the items in the inventory
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
            {
                SelectedItem -= 1;
                if (SaveSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem < 0)
                {
                    SelectedItem = SaveSlots.Count - 1;
                }
                SaveSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

            }


            // press space bar to use the item that is selected
            if (Input.GetKeyDown(KeyCode.B))
            {
                //  useItem(SelectedItem);

            }
        }
    }

}
