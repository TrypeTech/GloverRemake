using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SavePoint : MonoBehaviour {

    public GameObject SaveOptions;


    Player playerStat;
    GameObject player;
	// Use this for initialization
	void Start () {
        SaveOptions.gameObject.SetActive(false);
        player = GameObject.FindGameObjectWithTag("Player");
        playerStat = FindObjectOfType<Player>();
	}
	

	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.E))
        {
            SaveOptions.gameObject.SetActive(false);
        }
	}


    public void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {

            enableSaveingOptions();
        }
    }

   

    public void enableSaveingOptions()
    {
        player.gameObject.GetComponent<Movement>().CanMove = false ;
        SaveOptions.gameObject.SetActive(true);
    }

    public void SaveData()
    {
        exitSave();
        playerStat.gameObject.GetComponent<Player>().SaveGame();
  
    }
    public void exitSave()
    {
        SaveOptions.gameObject.SetActive(false);
        player.gameObject.GetComponent<Movement>().CanMove = true;
    }
}
