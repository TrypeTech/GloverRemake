using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleBattleHandler : MonoBehaviour {

    public int cointMinLimit = 10;
    public int ExpLevelLimit = 10;
    public int LevelUpMaxRange = 10;
    public int pointToLevelMultiplayer = 5;

    public GameObject BattlePannel;

    public SimpleClasses.Enamy TempEnamy;
    public List<Attacks.attacks> EnamyAttacks = new List<Attacks.attacks>();
    public List<Attacks.attacks> PlayerAttacks = new List<Attacks.attacks>();
    public List<Attacks.attacks> PlayerSkills = new List<Attacks.attacks>();
    public List<simpleInventory.Item> PlayerItems = new List<simpleInventory.Item>();
 

    public int enamyHpStore;
    public int EnamyCurrentHealth;

    simplePlayer player;
    SimpleMove movement;
    simpleInventory inventory;
    Attacks attack;

    public GameObject StartBattlePannel;
    public GameObject BattleChoicePannel;
    public GameObject PlayerAttackPannel;
    public GameObject PlayerSkillPannel;
    public GameObject BattleInfoPannel;
    public GameObject ItemChoicePannel;
    public GameObject StatsPannel;

    // slots
    public List<GameObject> BattleOptionsSlots = new List<GameObject>();
    public List<GameObject> AttackOptionsSLots = new List<GameObject>();
    public List<GameObject> SkillOptionsSlots = new List<GameObject>();
    public List<GameObject> ItemOptionsSlots = new List<GameObject>();
    public bool inBattle;

    // stats
    public Text playerStats;
    public Text enamyStats;

    public Text GameInfoText;
    public Text StatInfoText;

    // bars
    public Slider enamyHealthBar;
    public Slider playerHealthBar;
        


    // slotObjects
    public List<GameObject> AttackSlots = new List<GameObject>();
    public List<GameObject> SkillSlots = new List<GameObject>();
    public List<GameObject> ItemSlots = new List<GameObject>();

    public enum BattleStats
    {
        BattleIntro,
        BattleChoice,
        BattleAttack,
        BattleSkillAttack,
        BattleItem,
        PlayerAttacking,
        EnamyAttacking,
        CalculateDamage,
        PlayerDies,
        EnamyDies,
        ExitBattle,
        BattleOver
    }
    public BattleStats battleState;

    // prefrences Options
    public string MonsterEnterBattleDesc = "Has Attacked";
    public float BattleInfoTime = 2f;
    public float BattleTurnTime = 1f;
    public float StatDelatTime = 1f;
    public float PlayerDiedWaitTime = 1f;

    // new 
    public int SelectedItem;



    private void Awake()
    {

    }
    // Use this for initialization
    void Start () {


        player = FindObjectOfType<simplePlayer>();
        movement = FindObjectOfType<SimpleMove>();
        inventory = FindObjectOfType<simpleInventory>();
        attack = FindObjectOfType<Attacks>();
        BattlePannel.gameObject.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
		if(inBattle == true)
        {
            SelectSlots();
        }
        if (Input.GetKeyDown(KeyCode.E) )
        {
            if (battleState == BattleStats.BattleAttack || battleState == BattleStats.BattleItem || battleState == BattleStats.BattleSkillAttack)
            {
                SwitchBattleStates(BattleStats.BattleChoice);
            }
        }
	}


    public void StartBattle(SimpleClasses.Enamy enamy,SimpleMove move)
    {
        inBattle = true;
        // temp
      //  player.playerLevel = 10;
       // player.AttackStat = 10;
       // player.SpeedStat = 200;
       
        // temporary monster stuff
       enamyHpStore = enamy.Health;
        enamyHealthBar.maxValue = enamy.Health;
        EnamyCurrentHealth = enamy.Health ;
        enamyHealthBar.value = EnamyCurrentHealth ;

        movement = move;
        // disable movement and other input for character
        // enable battle pannel
        BattlePannel.gameObject.SetActive(true);
     movement.canMove = false;
        TempEnamy = enamy;

        Debug.Log("tempMonHP:" + TempEnamy.Health);
        Debug.Log("TemMonAttack:" + TempEnamy.Attack);
        // remove all information from former battle
        PlayerAttacks.Clear();
        EnamyAttacks.Clear();
        PlayerSkills.Clear();
        PlayerItems.Clear();
        playerHealthBar.maxValue = player.HealthStat;
        playerHealthBar.value = player.CurrentHealth;

        // updateDamageBars();

        // setup attacks

        for(int i = 0; i < attack.AttackList.Count; i++)
        {
            // check all player attacks
            // set it to the player at begining soon
            if(attack.AttackList[i].type == Attacks.attacks.AttackType.hero1 && attack.AttackList[i].RecLevel <= player.playerLevel && attack.AttackList[i].isItemAttack == false)
            {
                PlayerAttacks.Add(attack.AttackList[i]);
            }
            // add skills list
            if(attack.AttackList[i].type == Attacks.attacks.AttackType.hero1 && attack.AttackList[i].isItemAttack == true)
            {
                for(int a = 0; a < inventory.playerInventory.Count; a++)
                {
                    if(inventory.playerInventory[a] == attack.AttackList[i].AttackItemSimple)
                    {
                        PlayerSkills.Add(attack.AttackList[i]);
                    }
                }
            }

            if(attack.AttackList[i].type == Attacks.attacks.AttackType.normal && attack.AttackList[i].RecLevel <= TempEnamy.Level)
            {
                EnamyAttacks.Add(attack.AttackList[i]);
            }
        }

        SwitchBattleStates(BattleStats.BattleIntro);
    }



    public void SwitchBattleStates(BattleStats state)
    {

        battleState = state;
        switch (state)
        {
            case BattleStats.BattleIntro:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
              //  BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                StartCoroutine(BattleIntro());
                
                
                break;
            case BattleStats.BattleChoice:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(true);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                SelectedItem = 0;
                BattleOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                



                break;
            case BattleStats.BattleAttack:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(true);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                ConstructAttackSlots();
                SelectedItem = 0;
                AttackOptionsSLots[SelectedItem].gameObject.GetComponent<Button>().Select();



                break;
            case BattleStats.BattleSkillAttack:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(true);
                ItemChoicePannel.gameObject.SetActive(false);
                ConstructSkillSlots();
                SelectedItem = 0;
                SkillOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();



                break;
            case BattleStats.BattleItem:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(true);

                ConstructItemSlots();
                SelectedItem = 0;
                ItemOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                break;
            case BattleStats.ExitBattle:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
              //  TempEnamy.Health = enamyHpStore;

               movement.canMove = true;
                inBattle = false;
                BattlePannel.gameObject.SetActive(false);


                break;
            case BattleStats.CalculateDamage:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);


                break;
            case BattleStats.PlayerDies:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                StartCoroutine(Playerdies());
               

                break;
            case BattleStats.EnamyDies:
                BattleInfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(false);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);
                StartCoroutine(EnamyDies());

                break;
            case BattleStats.BattleOver:
                BattleInfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttackPannel.gameObject.SetActive(false);
                StatsPannel.gameObject.SetActive(true);
                BattleInfoPannel.gameObject.SetActive(false);
                PlayerSkillPannel.gameObject.SetActive(false);
                ItemChoicePannel.gameObject.SetActive(false);

                break;
        }
    }



    public void ConstructAttackSlots()
    {
        for(int i = 0; i < PlayerAttacks.Count; i++)
        {
            AttackSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerAttacks[i].Name + " " + PlayerAttacks[i].Amount + ":AM";
            int itemNumber = PlayerAttacks[i].AttackNumber;
            AttackSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            AttackSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAttacks(itemNumber, true));
        }
    }

   
    // basic functions
    //....................................................................................... DO ITEM ATTACK
    public void doItem(int id)
    {

        inventory.inInventory = false;
        for (int i = 0; i < inventory.playerInventory.Count; i++)
        {
            if(inventory.playerInventory[i].ID == id)
            {
                inventory.SelectedItem = i;
                inventory.useItem(i);

                
                    break;
            }
        }
      //  ConstructItemSlots();
       StartCoroutine(updateDamageBars());

        // finish attack

        int crit = Random.Range(1, 8);
        if (crit == 7)
        {
            crit = 2;
        }
        else
        {
            crit = 1;
        }
        // damage = enamyHp + enamyHp * Defence/100 - Attack + Attack * AttackPower/100 * crit;

        int RandomEnamyAttack = Random.Range(0, EnamyAttacks.Count);
        int EnamyDamage = ((2 * TempEnamy.Level / 5 + 2) * TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower) / player.DefenceStat / 50 + 2 * Random.Range(217, 255) / 255;

    //    int EnamyDamage = player.HealthStat + player.HealthStat * player.DefenceStat / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
        //  player.CurrentHealth -= EnamyDamage;
        // restore after algorithem is fixed
        player.CurrentHealth -= EnamyDamage;
        Debug.Log("PlayerDamageItem" + EnamyDamage);
        Debug.Log("Player" + player.HealthStat);
        if(player.CurrentHealth <= 0)
        {
            player.CurrentHealth = 0;
           StartCoroutine(updateDamageBars());
            SwitchBattleStates(BattleStats.PlayerDies);
        }
     else
        {
           StartCoroutine(updateDamageBars());
            // go back to battle choices
            SwitchBattleStates(BattleStats.BattleChoice);
        }
 
   
    }

  

    //.................................................................................................... ITEM SLOT CONSTRUCTION
    public void ConstructItemSlots()
    {
        // update items
        PlayerItems.Clear();
        
        for(int i = 0; i < ItemSlots.Count; i++)
        {
            PlayerItems.Add(inventory.ItemsList[0]);
        }

        int count = 0;
        
            for (int i = 0; i < inventory.playerInventory.Count; i++)
            {
                if (inventory.playerInventory[i].Type == simpleInventory.Item.ItemType.UsableItem)
                {
                    PlayerItems.Insert(count, inventory.playerInventory[i]);
                //  PlayerItems.Add(inventory.playerInventory[i]);
                count += 1;
                PlayerItems.RemoveAt(PlayerItems.Count - 1);
                }
            

        }
        // setup buttons

        for(int i = 0; i < PlayerItems.Count; i++)
        {

            ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerItems[i].Name + " " + PlayerItems[i].Count.ToString() + ":AM";
            // add function to the button when selected
            int itemNumber = PlayerItems[i].ID;
            ItemSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            if(PlayerItems[i].ID != 0)
            ItemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAttacks(itemNumber, false));
        }
    }

    //.................................................................................................... SKILL SLOT CONSTRUCTION
    // update items
    public void ConstructSkillSlots()
    {
        for(int i = 0; i < PlayerSkills.Count; i++)
        {

            SkillSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerSkills[i].Name + " " + PlayerSkills[i].Amount.ToString() + ":AM";
            // add function to button when selected
            int itemNumber = PlayerSkills[i].AttackNumber;
            SkillSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            SkillSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoSkillAttack(itemNumber));
        }
    }


    /*
    // pokemon battle calculation
    ((2A/5+2)*B* C)/D)/50)+2)*X)*Y/10)*Z)/255

A = attacker's Level
B = attacker's Attack or Special
C = attack Power
D = defender's Defense or Special
X = same-Type attack bonus(1 or 1.5)
Y = Type modifiers(40, 20, 10, 5, 2.5, or 0)
Z = a random number between 217 and 255

((2*LV/5+2)*ATKSTAT*ATKPOWER)/DEFENCESTAT)/50)+2)*SAMESTAT)*-SAMESTAT/10)*RANDOM)/255
((2*LV/5+2)*ATKSTAT*ATKPOWER)/DEFENCESTAT)/50)+2)*RANDOM)/255
*/

    // Attack Function when pressed the button ........................................................... ATTACKS
    public void DoAttacks(int id,bool isAttack)
    {

        // player gose first
        if(player.SpeedStat >= TempEnamy.Speed || isAttack == false)
        {
            if (!isAttack)
            {
                doItem(id);
            }
            else
            {
                for(int i = 0; i < PlayerAttacks.Count; i++)
                {
                    if(PlayerAttacks[i].AttackNumber == id)
                    {
                        // ............. Damage phase player stars here
                        // do effect here
                        // battle calculation
                        int crit = Random.Range(1, 8);
                        if (crit == 7)
                        {
                            crit = 2;
                        }
                        else
                        {
                            crit = 1;
                        }
                        int Damage = ((2 * player.playerLevel / 5 + 2) * player.AttackStat * PlayerAttacks[i].AtkPower) / TempEnamy.Defence / 50+2 *Random.Range(217, 255) / 255;

              //  int Damage = TempEnamy.Health + TempEnamy.Health * TempEnamy.Defence / 100 - player.AttackStat + player.AttackStat * PlayerAttacks[i].AtkPower / 100 * crit;
                        // check if enamy died
                        //tempenamy.current -= damage;

                        // alterd << i think auto kill do ubove change
                        EnamyCurrentHealth -= Damage ;
                        Debug.Log("Damage" + Damage);
                        Debug.Log("enamyHP" + EnamyCurrentHealth);
                        Debug.Log("PlayerAttackStat" + player.AttackStat);
                        Debug.Log("PlayerAttackPower" + PlayerAttacks[i].AtkPower);
                      
                        if (EnamyCurrentHealth <= 0)
                        {
                            EnamyCurrentHealth = 0;
                            StartCoroutine(updateDamageBars());
                            // other stuf

                            SwitchBattleStates(BattleStats.EnamyDies);
                        }
                        else
                        {
                            StartCoroutine(updateDamageBars());
                            // ............................Dabage Phase Enamy Starts Here
                            // do enamy Battle Scene event here
                            // enamy dose attack
                            crit = Random.Range(1, 8);
                            if(crit == 7)
                            {
                                crit = 1;
                            }
                            else
                            {
                                crit = 1;
                            }

                            int RandomEnamyAttack = Random.Range(0, EnamyAttacks.Count);
                            int EnamyDamage = ((2 * TempEnamy.Level / 5 + 2) * TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower )/ player.DefenceStat / 50 + 2 * Random.Range(217, 255) / 255;

                            //   int EnamyDamage = player.HealthStat + player.HealthStat * player.DefenceStat / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                            player.CurrentHealth -= EnamyDamage;

                            Debug.Log("PlayerDamage " + EnamyDamage);
                            Debug.Log("Player" + player.CurrentHealth);
                            Debug.Log("EnamyAttackStat" + TempEnamy.Attack);
                            Debug.Log("EnamyAttackPower" + EnamyAttacks[RandomEnamyAttack].AtkPower);
                            if (player.CurrentHealth <= 0)
                            {
                                player.CurrentHealth = 0;
                             StartCoroutine(updateDamageBars());
                                SwitchBattleStates(BattleStats.PlayerDies);
                                Debug.Log("PLAYERHAS DIED");
                            }
                            else
                            {
                                StartCoroutine(updateDamageBars());
                                // go back to battle choices
                                SwitchBattleStates(BattleStats.BattleChoice);

                            }
                            
                            // check if enamy died
                        }
                    }

                }
            }

        }

        else if (player.SpeedStat < TempEnamy.Speed)
        {

            for (int i = 0; i < PlayerAttacks.Count; i++)
            {
                //.............................................. Damage phase enamy starts here
                // do enamy battle cene event here
                // enamy dose attack
                int crit = Random.RandomRange(1, 8);
                if(crit == 7)
                {
                    crit = 2;
                }
                else
                {
                    crit = 1;
                }
                int RandomEnamyAttack = Random.Range(0, EnamyAttacks.Count);
                int EnamyDamage = ((2 * TempEnamy.Level / 5 + 2) * TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower) / player.DefenceStat / 50 + 2 * Random.Range(217, 255) / 255;

             //   int EnamyDamage = player.HealthStat + player.HealthStat * player.DefenceStat / 100 - TempEnamy.Attack  + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                player.CurrentHealth -= EnamyDamage;

                if(player.CurrentHealth <= 0)
                {
                    player.CurrentHealth = 0;
                    StartCoroutine(updateDamageBars());
                    SwitchBattleStates(BattleStats.PlayerDies);
                }
                else
                {

                    //.......................................... Damage Phase player Starts Here
                    // do efect here
                    // battle calculations
                    crit = Random.Range(1, 8);
                    if (crit == 7)
                    {
                        crit = 2;
                    }
                    else
                    {
                        crit = 1;
                    }
                    int Damage = ((2 * player.playerLevel / 5 + 2) * player.AttackStat * PlayerAttacks[i].AtkPower) / TempEnamy.Defence / 50 + 2 * Random.Range(217, 255) / 255;

               //     int Damage = TempEnamy.Health + TempEnamy.Health * TempEnamy.Defence / 100 - player.AttackStat  + player.AttackStat * PlayerAttacks[i].AtkPower / 100 * crit;
                    // check if enamy died
                   EnamyCurrentHealth -= Damage;
                    if (EnamyCurrentHealth <= 0)
                    {
                        EnamyCurrentHealth = 0;
                        StartCoroutine(updateDamageBars());
                        // other stuff
                        SwitchBattleStates(BattleStats.EnamyDies);
                    }
                    else
                    {
                        StartCoroutine(updateDamageBars());
                        SwitchBattleStates(BattleStats.BattleChoice);
                    }
                }
            }

        }
    }


    //............................................................................................SKILL ATTACK PHASE

    public void DoSkillAttack(int id)
    {
        Debug.Log("EnterdAttackButton");

        // player gose first
        if (player.SpeedStat >= TempEnamy.Speed)
        {
            for (int i = 0; i < PlayerSkills.Count; i++)
            {
                if (PlayerSkills[i].AttackNumber == id)
                {
                    // ........................................................... Damage Phase Player Starts Here
                    // do effect here
                    //battle calculations
                    int crit = Random.Range(1, 8);
                    if (crit == 7)
                    {
                        crit = 2;
                    }
                    else
                    {
                        crit = 1;
                    }
                    int Damage = ((2 * player.playerLevel / 5 + 2) * player.AttackStat * PlayerAttacks[i].AtkPower) / TempEnamy.Defence / 50 + 2 * Random.Range(217, 255) / 255;

                  //  int Damage = TempEnamy.Health + TempEnamy.Health * TempEnamy.Defence / 100 - player.AttackStat  + player.AttackStat * PlayerSkills[i].AtkPower / 100 * crit;
                    // check if enamy died
                     EnamyCurrentHealth -= Damage ;

                    // alterd 
                   // TempEnamy.Health -= 100;
                    Debug.Log("Damage" + Damage);
                    Debug.Log("enamHp" + EnamyCurrentHealth);
                 
                    if (EnamyCurrentHealth <= 0)
                    {
                        EnamyCurrentHealth = 0;
                        StartCoroutine(updateDamageBars());
                        // other stuf 
                        SwitchBattleStates(BattleStats.EnamyDies);
                    }
                    else
                    {
                        StartCoroutine(updateDamageBars());
                        // ........................................................... Damage Phase Enamy Starts Here
                        // do enamy battle scene event here
                        // enamy dose attack
                        crit = Random.Range(1, 8);
                        if (crit == 7)
                        {
                            crit = 2;
                        }
                        else
                        {
                            crit = 1;
                        }

                        int RandomEnamyAttack = Random.Range(0, EnamyAttacks.Count);
                        int EnamyDamage = ((2 * TempEnamy.Level / 5 + 2) * TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower) / player.DefenceStat / 50 + 2 * Random.Range(217, 255) / 255;

                   //     int EnamyDamage = player.HealthStat + player.HealthStat * player.DefenceStat / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                        player.CurrentHealth -= EnamyDamage;

                        Debug.Log("PlayerDamage" + EnamyDamage);
                        Debug.Log("Player" + player.CurrentHealth);
                        if (player.CurrentHealth <= 0)
                        {
                            player.CurrentHealth = 0;
                            StartCoroutine(updateDamageBars());
                            SwitchBattleStates(BattleStats.PlayerDies);
                        }
                        else
                        {
                            StartCoroutine(updateDamageBars());
                            // go back to battle choices
                            SwitchBattleStates(BattleStats.BattleChoice);
                        }

                        // check if enamy died


                    }
                }


            }
        }
        // enamy gose first
        else if (player.SpeedStat < TempEnamy.Speed)
        {
            for (int i = 0; i < PlayerSkills.Count; i++)
            {
                // ........................................................... Damage Phase Enamy Starts Here
                // do enamy battle scene event here
                // enamy dose attack
                int crit = Random.Range(1, 8);
                if (crit == 7)
                {
                    crit = 2;
                }
                else
                {
                    crit = 1;
                }

                int RandomEnamyAttack = Random.Range(0, EnamyAttacks.Count);
                int EnamyDamage = ((2 * TempEnamy.Level / 5 + 2) * TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower) / player.DefenceStat / 50 + 2 * Random.Range(217, 255) / 255;

             //   int EnamyDamage = player.HealthStat + player.HealthStat * player.DefenceStat / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                player.CurrentHealth -= EnamyDamage;
                if (player.CurrentHealth <= 0)
                {
                    player.CurrentHealth = 0;
                    StartCoroutine(updateDamageBars());
                    SwitchBattleStates(BattleStats.PlayerDies);
                }
                else
                {
                  
                    // ........................................................... Damage Phase Player Starts Here
                    // do effect here
                    //battle calculations
                    crit = Random.Range(1, 8);
                    if (crit == 7)
                    {
                        crit = 2;
                    }
                    else
                    {
                        crit = 1;
                    }
                    int Damage = ((2 * player.playerLevel / 5 + 2) * player.AttackStat * PlayerAttacks[i].AtkPower) / TempEnamy.Defence / 50 + 2 * Random.Range(217, 255) / 255;

                 //   int Damage = TempEnamy.Health + TempEnamy.Health * TempEnamy.Defence / 100 - player.AttackStat + player.AttackStat * PlayerSkills[i].AtkPower / 100 * crit;
                    // check if enamy died
                    EnamyCurrentHealth -= Damage;
                    if (EnamyCurrentHealth <= 0)
                    {
                        EnamyCurrentHealth = 0;
                        StartCoroutine(updateDamageBars());
                        // other stuf 
                        SwitchBattleStates(BattleStats.EnamyDies);
                    }
                    else
                    {
                        StartCoroutine(updateDamageBars());
                        SwitchBattleStates(BattleStats.BattleChoice);
                    }





                }


            }
        }
    }
















    // functions ............................................................ selection functions
    public void ChooseDoAttack()
    {
        SwitchBattleStates(BattleStats.BattleAttack);
    }
     public void ChooseDoItem()
    {
        SwitchBattleStates(BattleStats.BattleItem);
    }
    public void Run()
    {
        StartCoroutine(RunMessage());
        //SwitchBattleStates(BattleStats.ExitBattle);
    }
    public void AttackButton()
    {
        SwitchBattleStates(BattleStats.BattleAttack);
    }
    public void SkillButton()
    {
        SwitchBattleStates(BattleStats.BattleSkillAttack);
    }
    public void ItemButton()
    {
        SwitchBattleStates(BattleStats.BattleItem);
    }

    // the Courutines
    public IEnumerator BattleIntro()
    {
        GameInfoText.text = TempEnamy.EnamyName  + MonsterEnterBattleDesc;
        yield return new WaitForSeconds(BattleInfoTime);

        SwitchBattleStates(BattleStats.BattleChoice);
    }

    public IEnumerator updateDamageBars()
    {
        
        playerStats.text = player.name + " Lv:" + player.playerLevel + " HP:" + player.CurrentHealth;
        playerHealthBar.value = player.CurrentHealth;
        enamyStats.text = TempEnamy.EnamyName + " Lv:" + TempEnamy.Level + " HP:" + EnamyCurrentHealth;
        enamyHealthBar.value = EnamyCurrentHealth;
        Debug.Log("EnamyHP" + EnamyCurrentHealth);
        yield return new WaitForSeconds(BattleTurnTime);

    }

    // functions .......................................................................... DIE FUNCTIONS

    public IEnumerator EnamyDies()
    {
        StatsPannel.gameObject.SetActive(true);
        player.CurrentPointsToLevel += ExpLevelLimit * TempEnamy.Level;
        player.Money += cointMinLimit * TempEnamy.Level;
        StatInfoText.text = "Player has defeated" + TempEnamy.EnamyName;
        yield return new WaitForSeconds(StatDelatTime);

        if(player.CurrentPointsToLevel >= player.PointsToLevel)
        {
            // update stats lv ect;
            UpdateStats();
            int exp = ExpLevelLimit * TempEnamy.Level;
            StatInfoText.text = "PLAYER HAS LEVELD UP:" + player.playerLevel.ToString() + "GAINED EXP:" + exp.ToString() + " CURRENT EXP:" + player.CurrentPointsToLevel.ToString() + "/" + player.PointsToLevel;

        }
        else
        {
            int exp = 7 * TempEnamy.Level;
            StatInfoText.text = "GAINED EXP:" + exp.ToString() + " CURRENT EXP:" + player.CurrentPointsToLevel + "/" + player.PointsToLevel;
        }
        int coin = cointMinLimit * TempEnamy.Level;
        GameInfoText.text = TempEnamy.EnamyName + " has fainted he can no longer contenuy" + " Player Has Gained" + coin.ToString();
        yield return new WaitForSeconds(2f);

        
        SwitchBattleStates(BattleStats.ExitBattle);
        enamyHealthBar.value = TempEnamy.Health;
        EnamyCurrentHealth = TempEnamy.Health;
        Debug.Log("TempEnmyHEalth" + TempEnamy.Health);
        Debug.Log("currentval" + EnamyCurrentHealth);

    }
  

    public IEnumerator Playerdies()
    {
        GameInfoText.text = "PLAYER HAS FAINTED";
        // DO ANIMS HERE
        yield return new WaitForSeconds(PlayerDiedWaitTime);
        BattlePannel.gameObject.SetActive(false);
        player.LoadGame(player.GameID);
    }
    public void UpdateStats()
    {
        int randomNumber = Random.Range(1, LevelUpMaxRange);
        player.HealthStat += randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.AttackStat += randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.DefenceStat += randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.SpeedStat += randomNumber;

        //NOTE ADD SPDEFENCE , SPATTACK
        player.playerLevel += 1;
        player.PointsToLevel = player.PointsToLevel * pointToLevelMultiplayer;
    }

    public IEnumerator RunMessage()
    {
        BattleChoicePannel.gameObject.SetActive(false);
        BattleInfoPannel.gameObject.SetActive(true);
        GameInfoText.text = "Player Has Fled";
        yield return new WaitForSeconds(1f);

        BattleInfoPannel.gameObject.SetActive(false);

        SwitchBattleStates(BattleStats.ExitBattle);

    }

    //.......................................................................................... pannels 
    public void SelectSlots()
    {
        //............................................................................. BATTLE CHOICE NAV
        if (battleState == BattleStats.BattleChoice)
        {
            // go up and right selecting the items in the inventory 
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                SelectedItem += 1;
                if (BattleOptionsSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem > BattleOptionsSlots.Count - 1)
                {
                    SelectedItem = 0;
                }
                BattleOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                           
             //   ItemInfoText.text = playerInventory[SelectedItem].Description;       // <<<<<<< added new code here
            }

            // go down and left selecting the items in the inventory
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
            {
                SelectedItem -= 1;

                if (BattleOptionsSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem < 0)
                {
                    SelectedItem = BattleOptionsSlots.Count - 1;
                }
                BattleOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                             
               // ItemInfoText.text = playerInventory[SelectedItem].Description;    //   <<<<<<< added new code here
            }


            // press space bar to use the item that is selected
            if (Input.GetKeyDown(KeyCode.B))
            {
                // it selects it twise becuse it prsses button space
                // wich triggers selected ui elementa and also uses function
              //  useItem(SelectedItem);

            }
        }
       //................................................................................................ATTACK CHOICE NAV
        if (battleState == BattleStats.BattleAttack)
        {
            // go up and right selecting the items in the inventory 
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                SelectedItem += 1;
                if (AttackOptionsSLots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem > AttackOptionsSLots.Count - 1)
                {
                    SelectedItem = 0;
                }
                AttackOptionsSLots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                           
                //   ItemInfoText.text = playerInventory[SelectedItem].Description;       // <<<<<<< added new code here
            }

            // go down and left selecting the items in the inventory
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
            {
                SelectedItem -= 1;

                if (AttackOptionsSLots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem < 0)
                {
                    SelectedItem = AttackOptionsSLots.Count - 1;
                }
                AttackOptionsSLots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                             
                // ItemInfoText.text = playerInventory[SelectedItem].Description;    //   <<<<<<< added new code here
            }


            // press space bar to use the item that is selected
            if (Input.GetKeyDown(KeyCode.B))
            {
                // it selects it twise becuse it prsses button space
                // wich triggers selected ui elementa and also uses function
                //  useItem(SelectedItem);

            }
        }


        //................................................................................................ATTACK CHOICE NAV
        if (battleState == BattleStats.BattleSkillAttack)
        {
            // go up and right selecting the items in the inventory 
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                SelectedItem += 1;
                if (SkillOptionsSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem > SkillOptionsSlots.Count - 1)
                {
                    SelectedItem = 0;
                }
                SkillOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                           
                //   ItemInfoText.text = playerInventory[SelectedItem].Description;       // <<<<<<< added new code here
            }

            // go down and left selecting the items in the inventory
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
            {
                SelectedItem -= 1;

                if (SkillOptionsSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem < 0)
                {
                    SelectedItem = SkillOptionsSlots.Count - 1;
                }
                SkillOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                             
                // ItemInfoText.text = playerInventory[SelectedItem].Description;    //   <<<<<<< added new code here
            }


            // press space bar to use the item that is selected
            if (Input.GetKeyDown(KeyCode.B))
            {
                // it selects it twise becuse it prsses button space
                // wich triggers selected ui elementa and also uses function
                //  useItem(SelectedItem);

            }
        }

        //................................................................................................ATTACK CHOICE NAV
        if (battleState == BattleStats.BattleItem)
        {
            // go up and right selecting the items in the inventory 
            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.D))
            {
                SelectedItem += 1;
                if (ItemOptionsSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem > ItemOptionsSlots.Count - 1)
                {
                    SelectedItem = 0;
                }
                ItemOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                           
                //   ItemInfoText.text = playerInventory[SelectedItem].Description;       // <<<<<<< added new code here
            }

            // go down and left selecting the items in the inventory
            if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.A))
            {
                SelectedItem -= 1;

                if (ItemOptionsSlots.Count == 0)
                {
                    SelectedItem = 0;
                }
                if (SelectedItem < 0)
                {
                    SelectedItem = ItemOptionsSlots.Count - 1;
                }
                ItemOptionsSlots[SelectedItem].gameObject.GetComponent<Button>().Select();

                // add the item description here                                             
                // ItemInfoText.text = playerInventory[SelectedItem].Description;    //   <<<<<<< added new code here
            }


            // press space bar to use the item that is selected
            if (Input.GetKeyDown(KeyCode.B))
            {
                // it selects it twise becuse it prsses button space
                // wich triggers selected ui elementa and also uses function
                //  useItem(SelectedItem);

            }
        }
    }
}
