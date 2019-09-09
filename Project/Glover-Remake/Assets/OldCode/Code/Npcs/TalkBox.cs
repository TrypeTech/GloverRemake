using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class TalkBox : MonoBehaviour {


    public Text NameText;
    public Text TalkText;
    public Animator anim;
   // GameObject player;
    simpleInventory inventory;
    SimpleBattleHandler battle;
    Store store;
    SimpleClasses clas;
    CraftStore craftStore;
   simpleInventory item;
    SimpleMove move;
    private Queue<string> sentences;

    // text Button
    [Header("SkipSpeed")]
    public float EnableNewTextSpeed = 1f;
    public bool NpcTalking;
    public float textSpeed;

    int itemGiven;
    bool isbattle;
    int MonsterNumber;
    bool isAStore;
    int PriceRange;
    bool IsCrafter;
    public bool canDo;
    // Use this for initialization
    void Start () {

        craftStore = FindObjectOfType<CraftStore>();
       store = FindObjectOfType<Store>();
        clas = GameObject.FindObjectOfType<SimpleClasses>();
        battle = GameObject.FindObjectOfType<SimpleBattleHandler>();
       // player = GameObject.FindGameObjectWithTag("Player");
        item = GameObject.FindObjectOfType<simpleInventory>();
        inventory = FindObjectOfType<simpleInventory>();
        sentences = new Queue<string>();
        anim.SetBool("TalkBoxOpen", false);
        Invoke("getMovement", 2f);
        canDo = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Space) && NpcTalking == true)
        {
            if (canDo == true)
            {
                canDo = false;
                Invoke("Wait", 0.3f);
                DisplayNextSentece();
            }
        }
	}

 
    public void Wait()
    {
        canDo = true;
    }

    public void StartDialogue(string[] dialogue, int item,bool isBattle,int Monster,bool isStore,int priceRange,bool isCrafter)
    {
       
        IsCrafter = isCrafter;
        isAStore = isStore;
        PriceRange = priceRange;
        isbattle = isBattle;
        MonsterNumber = Monster;
        //  player.GetComponent<Movement>().CanMove = false;
        move.canMove = false;
        anim.SetBool("TalkBoxOpen", true);

       // NameText.text = name;
        sentences.Clear();

        foreach(string sentence in dialogue)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentece();
        NpcTalking = true;

        itemGiven = item;
    }


    public void DisplayNextSentece()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }
 
    IEnumerator TypeSentence(string sentence)
    {
        TalkText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            TalkText.text += letter;
            // yield return null;
            yield return new WaitForSeconds(textSpeed);
        }
    }
    void EndDialogue()
    {
      
       
        
        if(itemGiven != 0)
        {
            string giveItemText = " [Player] receaved " + item.ItemsList[itemGiven].Name ;
            inventory.addItem(itemGiven);
            StartCoroutine(TypeSentence(giveItemText));
            itemGiven = 0;
        }
        else if(isbattle == true)
        {
            isbattle = false;
           
                  battle.StartBattle(clas.enamies[MonsterNumber],move);
            EndDialogue();
        }
        else if(isAStore == true)
        {
            isAStore = false;
            Debug.Log("Price" + PriceRange);
            store.MakeStore(PriceRange);
            EndDialogue();
        }
        else if(IsCrafter == true)
        {
            IsCrafter = false;
            craftStore.MakeCraftStore();

            EndDialogue();
        }
        else
        {
            move.canMove = true;
            NpcTalking = false;
            anim.SetBool("TalkBoxOpen", false);
        }
    }   


    public void getMovement()
    {
        move = FindObjectOfType<SimpleMove>();
    }
}
