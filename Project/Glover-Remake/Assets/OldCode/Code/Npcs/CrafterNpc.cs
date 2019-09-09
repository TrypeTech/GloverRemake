using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrafterNpc : MonoBehaviour {


    [Header("NPC Dialogue")]
    public string NpcName;


    public string[] Dialague;

  


    TalkBox talkBox;
    public bool canBeginTalking;
    // the player
 //   GameObject player;
  //  Items item;
   // Inventory inventory;




    // method for colors
    //   StoreMessage = "<color=silver>" + "\n"  +  MessageText.text  +" " + "[" +PlayerName + "]" + "</color>" ;
    // change sixe "<size= 20> </size>
    // color method: "<color=darkblue>" + "Player" + "</color>" put in text string
    // colors for text brown cyan gray lightblue silver teal  white yellow red black

    // Use this for initialization
    void Start()
    {

     //   item = FindObjectOfType<Items>();
     //   inventory = FindObjectOfType<Inventory>();
        talkBox = FindObjectOfType<TalkBox>();

        
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canBeginTalking == true)
        {
            canBeginTalking = false;
          
            talkBox.StartDialogue(Dialague, 0, false, 0, false, 0,true);


        }
    }


    public void OnTriggerStay2D(Collider2D collision)
    {

        if (collision.tag == "Player")
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
