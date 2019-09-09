using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossNpc : MonoBehaviour {
    [Header("NPC Dialogue")]
    public string NpcName;

    [TextArea(3, 10)]
    public string[] Dialague;

    [Header("Monster Info")]
   
    public int MonsterNumber;


    TalkBox talkBox;
    public bool canBeginTalking;
    // the player
   // GameObject player;
 //   simpleInventory item;
  //  simpleInventory inventory;



    // Use this for initialization
    void Start()
    {

      //  item = FindObjectOfType<simpleInventory>();
       // inventory = FindObjectOfType<simpleInventory>();
      talkBox = FindObjectOfType<TalkBox>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canBeginTalking == true)
        {
            canBeginTalking = false;
            talkBox.StartDialogue(Dialague, 0,true,MonsterNumber,false,0,false);


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
