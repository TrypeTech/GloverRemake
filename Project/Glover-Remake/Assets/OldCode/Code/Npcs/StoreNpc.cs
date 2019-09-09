using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class StoreNpc : MonoBehaviour {

    [Header("NPC Dialogue")]
    public string NpcName;

   
    public string[] Dialague;

    [Header("Store Class Price Range 10..")]
    public int PriceRange = 10;


    TalkBox talkBox;
    public bool canBeginTalking;
    // the player

    public bool BeganTalking;


    // Use this for initialization
    void Start()
    {

        talkBox = FindObjectOfType<TalkBox>();
        BeganTalking = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Space) && canBeginTalking == true && BeganTalking == false)
        {
           canBeginTalking = false;
           BeganTalking = true;
           talkBox.StartDialogue(Dialague, 0, false, 0, true, PriceRange, false);
           Invoke("BeginTalking", 2f);
        }
    }

    public void BeginTalking()
    {
        BeganTalking = false;
      
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
     //   canBeginTalking = false;
      //  BeganTalking = true;
      //  talkBox.StartDialogue(Dialague, 0, false, 0, true, PriceRange, false);
       // Invoke("BeginTalking", 1f);
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
            BeganTalking = false;
        }
    }


}
