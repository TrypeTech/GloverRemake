using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleSave : MonoBehaviour {


    simplePlayer player;
    SimpleMove move;

    public GameObject PopUpPannel;
    public GameObject SaveDataPannel;
    public Text SaveInfoText;
    public List<GameObject> Options = new List<GameObject>();
    public float saveWaitTime;
    public bool insave;
    public bool JustStarted;
    
	// Use this for initialization
	void Start () {
        player = FindObjectOfType<simplePlayer>();
        PopUpPannel.gameObject.SetActive(false);
        SaveDataPannel.gameObject.SetActive(false);
        insave = false;
        JustStarted = true;
        Invoke("saveEnableCountDown", 2f);
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E) && insave == true)
        {
            Cancel();
        }
	}
   
    public void saveEnableCountDown()
    {
        JustStarted = false;

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (JustStarted == false)
        {

            Debug.Log("Enterd Collision");
            if (collision.tag == "Player")
            {
                collision.gameObject.GetComponentInChildren<SimpleMove>().canMove = false;
                EnablePopUp();
                move = collision.gameObject.GetComponent<SimpleMove>();
            }
        }
    }


    public void EnablePopUp()
    {
        PopUpPannel.gameObject.SetActive(true);
    }

    public void SaveGame()
    {
        StartCoroutine(save());
    }
    public void Cancel()
    {
        SaveDataPannel.gameObject.SetActive(false);
        PopUpPannel.gameObject.SetActive(false);
        move.canMove = true;
    }

    public IEnumerator save()
    {
        PopUpPannel.gameObject.SetActive(false);
        SaveDataPannel.gameObject.SetActive(true);
        SaveInfoText.text = "GAME SAVE SLOT:" + player.GameID + "\n" + "LEVEL:" + player.playerLevel + "\n" + "MONEY:" + player.Money
            + "\n" + "CREDITS:" + player.Credits + "\n" + "GAMEPLAYTIME: " + player.PlayTime + "\n" + "\n" + "SAVEING GAME DATA ...";

        yield return new WaitForSeconds(saveWaitTime);
       
        SaveInfoText.text = "GAME HAS SAVED SUCCESSFULLY!!";
        yield return new WaitForSeconds(1f);
        Cancel();
        player.SaveGame(player.GameID);
    }
  
}
