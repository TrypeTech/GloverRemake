using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CommonNpc : MonoBehaviour {


    [Header("NPC Dialogue")]
    public string NpcName;

  
    public string[] Dialague;

    [Header("Item Info")]
    public bool hasItem;
    public int ItemNumber;


    TalkBox talkBox;
    public bool canBeginTalking;
    // the player
   // GameObject player;
   // Items item;
   // Inventory inventory;




    // method for colors
 //   StoreMessage = "<color=silver>" + "\n"  +  MessageText.text  +" " + "[" +PlayerName + "]" + "</color>" ;
 // change sixe "<size= 20> </size>
    // color method: "<color=darkblue>" + "Player" + "</color>" put in text string
    // colors for text brown cyan gray lightblue silver teal  white yellow red black

    // Use this for initialization
    void Start () {


      talkBox = FindObjectOfType<TalkBox>();

           
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && canBeginTalking == true)
        {
            canBeginTalking = false;
            if (hasItem == false)
            {
                ItemNumber = 0;
            }
            talkBox.StartDialogue(Dialague, ItemNumber,false,0,false,0,false);
          
           
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        
    }

    public void OnTriggerStay2D(Collider2D collision)
    {
        
        if(collision.tag == "Player")
        {
            canBeginTalking = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            canBeginTalking = false;
        }
    }





}
