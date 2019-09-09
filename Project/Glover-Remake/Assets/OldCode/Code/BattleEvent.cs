using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BattleEvent : MonoBehaviour {
    // battle data info
    public int coinMinLimit = 10;
    public int ExpLevelLimit = 10;
    public int LevelUpMaxRange = 3;

    public GameObject BattlePannel;
    
  
    public Classes.Enamy TempEnamy;
    public List<Attacks.attacks> EnamyAttacks = new List<Attacks.attacks>();
    public List<Attacks.attacks> PlayerAttacks = new List<Attacks.attacks>();
    public List<Attacks.attacks> PlayerSkills = new List<Attacks.attacks>();
    public List<Items.Item> PlayerItems = new List<Items.Item>();

    // temporaryEnamyAttack
    // SaveData
    public int enamyHpStore;


  
    Player player;
    GamManager gManager;
    Movement movment;
    Inventory inventory;
    Attacks attack;


    public GameObject InfoPannel;
    public GameObject BattleChoicePannel;
    public GameObject PlayerAttacksPannel;
    public GameObject PlayerItemsPannel;
    public GameObject SkillAttacksPannel;
    public GameObject StatPannel;
    public GameObject InfoBarsPannels;

    //stats
    public Text playerStats;
    public Text enamyStats;

    public Text GameInfoText;
    public Text StatsInfoText;

    // slots
    public List<GameObject> AttackSlots = new List<GameObject>();
    public List<GameObject> SkillSlots = new List<GameObject>();
    public List<GameObject> ItemSlots = new List<GameObject>();

    public enum BattleStates
    {
        BattleIntro,
        BattleChoices,
        BattleAttack,
        SkillAttack,
        BattleItems,
        ExitBattle,
        PlayerAttack,
        EnamyAttack,
        CalculateDamage,
        PlayerDies,
        EnamyDies,
        BattleOver
    }
    public BattleStates battleState;
    // Use this for initialization

    private void Awake()
    {
        player = FindObjectOfType<Player>();
     
        movment = FindObjectOfType<Movement>();
        gManager = FindObjectOfType<GamManager>();
        inventory = FindObjectOfType<Inventory>();
        attack = FindObjectOfType<Attacks>();

    }
    void Start () {
 
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    public void StartBattle(Classes.Enamy enamy) 
    {
     //  Debug.Log("Started Battle............................... Battle");
      //  Debug.Log(player.Level);
      // < temporary remove later
        player.Level = 10;
        player.Attack = 10;
        player.Speed = 200;

        // temp mon stuff
        enamyHpStore = enamy.Hp;

        BattlePannel.gameObject.SetActive(true);
        movment.CanMove = false;
        TempEnamy = enamy;
       
        Debug.Log("TempMonHpBS" + TempEnamy.Hp);
        // remove all information from formaer battle
        PlayerAttacks.Clear();
        EnamyAttacks.Clear();
        PlayerSkills.Clear();
        PlayerItems.Clear();

        updateDamageBars();
        // setup attacks
        for (int i = 0; i < attack.AttackList.Count; i++)
        {
            // check all player Attacks
            // set it to the player att beginin sooon
            if(attack.AttackList[i].type == Attacks.attacks.AttackType.hero1 && attack.AttackList[i].RecLevel <= player.Level && attack.AttackList[i].isItemAttack == false)
            {
                PlayerAttacks.Add(attack.AttackList[i]);
            }
            // add skills list
            if (attack.AttackList[i].type == Attacks.attacks.AttackType.hero1  && attack.AttackList[i].isItemAttack == true)
            {
                for (int a = 0; a < inventory.inventory.Count; a++) {

                    if(inventory.inventory[a] == attack.AttackList[i].AttackItem)
                    {
                        PlayerSkills.Add(attack.AttackList[i]);
                    }
                }
                    
                    
            }
            //  check all enamy attack and add to enamy attack list
            // remeber to add diffrent attacks based on enamy type
            if (attack.AttackList[i].type == Attacks.attacks.AttackType.normal && attack.AttackList[i].RecLevel <= TempEnamy.Level)
            {
                EnamyAttacks.Add(attack.AttackList[i]);
            }
        }
  
            SwitchBattleStates(BattleStates.BattleIntro);
    }




    public void SwitchBattleStates(BattleStates state)
    {
        battleState = state;
        switch (state)
        {
            case BattleStates.BattleIntro:
                InfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);
                StartCoroutine(BattleIntro());


                break;
            case BattleStates.BattleChoices:
                InfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(true);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);

                break;
            case BattleStates.BattleAttack:
                InfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(true);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);
                ConstructAttackSlots();
                break;
            case BattleStates.SkillAttack:
                InfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(true);
                ConstructSkillSlots();
                break;
            case BattleStates.ExitBattle:
                InfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                BattlePannel.gameObject.SetActive(false);
                SkillAttacksPannel.gameObject.SetActive(false);
                TempEnamy.Hp = enamyHpStore;
                // exit battle

                gManager.SwitchCanvases(GamManager.ActiveCanvas.inGame);
                movment.CanMove = true;
                break;
            case BattleStates.BattleItems:
                InfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(true);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);
                ConstructItemSlots();
                break;
            case BattleStates.CalculateDamage:
                InfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);
                break;
            case BattleStates.PlayerDies:
                InfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);
                StartCoroutine(PlayerDies());
                break;
            case BattleStates.EnamyDies:
                InfoPannel.gameObject.SetActive(true);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(false);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);
                StartCoroutine(Enamydies());
                break;
            case BattleStates.BattleOver:
                InfoPannel.gameObject.SetActive(false);
                BattleChoicePannel.gameObject.SetActive(false);
                PlayerAttacksPannel.gameObject.SetActive(false);
                PlayerItemsPannel.gameObject.SetActive(false);
                StatPannel.gameObject.SetActive(true);
                InfoBarsPannels.gameObject.SetActive(true);
                SkillAttacksPannel.gameObject.SetActive(false);
                break;
        }

    }

    public void ConstructAttackSlots()
    {
        for(int i = 0; i < PlayerAttacks.Count; i++)
        {
            
            AttackSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerAttacks[i].Name + " " + PlayerAttacks[i].Amount + ":AM";
            // add function to button when selected
            int itemNumber = PlayerAttacks[i].AttackNumber;
            AttackSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            AttackSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAttacks(itemNumber,true));
        }
    }
    public void ConstructSkillSlots()
    {
        for (int i = 0; i < PlayerSkills.Count; i++)
        {

            SkillSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerSkills[i].Name + " " + PlayerSkills[i].Amount + ":AM";
            // add function to button when selected
            int itemNumber = PlayerSkills[i].AttackNumber;
            SkillSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            SkillSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoSkillAttack(itemNumber));
        }
    }
    public void ConstructItemSlots()
    {
        // update items
        PlayerItems.Clear();
        for (int i = 0; i < inventory.inventory.Count; i++)
        {
            if (inventory.inventory[i].type == Items.Item.ItemType.Useable)
            {
                PlayerItems.Add(inventory.inventory[i]);
            }
        }
        // set up buttons
        for (int i = 0; i < PlayerItems.Count; i++)
        {

            ItemSlots[i].gameObject.GetComponentInChildren<Text>().text = PlayerItems[i].Name + " " + PlayerItems[i].Count + ":AM";
            // add function to button when selected
            int itemNumber = PlayerItems[i].itemNumber;
            ItemSlots[i].GetComponentInChildren<Button>().onClick.RemoveAllListeners();
            ItemSlots[i].GetComponentInChildren<Button>().onClick.AddListener(() => DoAttacks(itemNumber,false));
        }
    }


        // damage = enamyHp + enamyHp * Defence/100 - Attack + Attack * AttackPower/100 * crit;
    public void DoAttacks(int id,bool isAttack)
    {
        Debug.Log("EnterdAttackButton");

        // player gose first
        if (player.Speed >= TempEnamy.Speed || isAttack == false)
        {

            if (!isAttack)
            {
                doItem(id);
            }
            else{


                for (int i = 0; i < PlayerAttacks.Count; i++)
                {
                    if (PlayerAttacks[i].AttackNumber == id)
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

                        int Damage = TempEnamy.Hp + TempEnamy.Hp * TempEnamy.Defence / 100 - player.Attack + player.Attack * PlayerAttacks[i].AtkPower / 100 * crit;
                        // check if enamy died
                        // TempEnamy.Hp -= Damage;

                        // alterd 
                        TempEnamy.Hp -= 100;
                        Debug.Log("Damage" + Damage);
                        Debug.Log("enamHp" + TempEnamy.Hp);
                        if (TempEnamy.Hp <= 0)
                        {
                            TempEnamy.Hp = 0;
                            updateDamageBars();
                            // other stuf 
                            SwitchBattleStates(BattleStates.EnamyDies);
                        }
                        else
                        {
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
                            int EnamyDamage = player.Hp + player.Hp * player.Defence / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                            player.Hp -= EnamyDamage;

                            Debug.Log("PlayerDamage" + EnamyDamage);
                            Debug.Log("Player" + player.Hp);
                            if (player.Hp <= 0)
                            {
                                player.Hp = 0;
                                updateDamageBars();
                                SwitchBattleStates(BattleStates.PlayerDies);
                            }
                            else
                            {
                                updateDamageBars();
                                // go back to battle choices
                                SwitchBattleStates(BattleStates.BattleChoices);
                            }

                            // check if enamy died


                        }
                    }
                }

            }
        }
        // enamy gose first
        else if (player.Speed < TempEnamy.Speed)
        {
            for (int i = 0; i < PlayerAttacks.Count; i++)
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
                int EnamyDamage = player.Hp + player.Hp * player.Defence / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                player.Hp -= EnamyDamage;
                if (player.Hp <= 0)
                {
                    player.Hp = 0;
                    updateDamageBars();
                    SwitchBattleStates(BattleStates.PlayerDies);
                }
                else {

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

                    int Damage = TempEnamy.Hp + TempEnamy.Hp * TempEnamy.Defence / 100 - player.Attack + player.Attack * PlayerAttacks[i].AtkPower / 100 * crit;
                    // check if enamy died
                    TempEnamy.Hp -= Damage;
                    if (TempEnamy.Hp <= 0)
                    {
                        TempEnamy.Hp = 0;
                        updateDamageBars();
                        // other stuf 
                        SwitchBattleStates(BattleStates.EnamyDies);
                    }
                    else
                    {
                        updateDamageBars();
                        SwitchBattleStates(BattleStates.BattleChoices);
                    }





                }


            }
        }
    }

  
    public void ChooseDoAttack()
    {
        SwitchBattleStates(BattleStates.BattleAttack);
    }
    public void ChooseDoItem()
    {
        SwitchBattleStates(BattleStates.BattleItems);
    }
    public void Run()
    {
        SwitchBattleStates(BattleStates.ExitBattle);
    }
    public void updateDamageBars()
    {
        playerStats.text = player.name + " :" + player.Level + " :" + player.Hp;
        enamyStats.text = TempEnamy.Name + " :" + TempEnamy.Level + " :" + TempEnamy.Hp;
        Debug.Log("EnamyHp" + TempEnamy.Hp);
     }

    public IEnumerator Enamydies()
    {
        StatPannel.gameObject.SetActive(true);
        player.CurrentLevelPoints += ExpLevelLimit * TempEnamy.Level;
        player.Money += coinMinLimit * TempEnamy.Level;
        StatsInfoText.text = " Player has defeated " + TempEnamy.Name;
        yield return new WaitForSeconds(1f);
        if (player.CurrentLevelPoints >= player.PointsToLevel)
        {
            // update Stats lv ect;
            UpDateStats();
            int exp = ExpLevelLimit * TempEnamy.Level;

            StatsInfoText.text = "PLAYER HAS LEVELED UP:" + player.Level + "GAINED EXP:" + exp.ToString() + " CURRENT EXP:" + player.CurrentLevelPoints + "/" + player.PointsToLevel;
          
        }
        else
        {
            int exp = 7 * TempEnamy.Level;
            StatsInfoText.text = "GAINED EXP:" + exp.ToString() + " CURRENT EXP:" + player.CurrentLevelPoints +"/" + player.PointsToLevel;
        }
       int coin = coinMinLimit * TempEnamy.Level;
        GameInfoText.text =  TempEnamy.Name + " has fainted he can no longer contenu" + " Player Has Gained" + coin.ToString() ;
        yield return new WaitForSeconds(2f);

        SwitchBattleStates(BattleStates.ExitBattle);
    }
    public IEnumerator PlayerDies()
    {
        GameInfoText.text = "Player has fainted";
        // do anims here
        yield return new WaitForSeconds(2f);

        BattlePannel.gameObject.SetActive(false);
        player.LoadPlayerGameData();
    }

    // intro to battle animations and text
    public IEnumerator BattleIntro()
    {
        GameInfoText.text = TempEnamy.Name + " Has Attacked";
        yield return new WaitForSeconds(2f);

        SwitchBattleStates(BattleStates.BattleChoices);
   
    }

    public void UpDateStats()
    {
        int randomNumber = Random.Range(1, LevelUpMaxRange);
        player.Hp += randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.Attack += randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.Defence = randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.Speed = randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.SpAttack = randomNumber;
        randomNumber = Random.Range(1, LevelUpMaxRange);
        player.SpDef = randomNumber;

        player.Level += 1;

        player.PointsToLevel = player.PointsToLevel * 5;


    }

    public void doItem(int id)
    {
        inventory.CurrentItem = id;
        inventory.inInventory = false;
        inventory.useItem();
        inventory.inInventory = true;
        updateDamageBars();
        // ........................................................... Damage Phase Enamy Starts Here
        // do enamy battle scene event here
        // enamy dose attack
      int  crit = Random.Range(1, 8);
        if (crit == 7)
        {
            crit = 2;
        }
        else
        {
            crit = 1;
        }

        int RandomEnamyAttack = Random.Range(0, EnamyAttacks.Count);
        int EnamyDamage = player.Hp + player.Hp * player.Defence / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
        player.Hp -= EnamyDamage;

        Debug.Log("PlayerDamage" + EnamyDamage);
        Debug.Log("Player" + player.Hp);
        if (player.Hp <= 0)
        {
            player.Hp = 0;
            updateDamageBars();
            SwitchBattleStates(BattleStates.PlayerDies);
        }
        else
        {
            updateDamageBars();
            // go back to battle choices
            SwitchBattleStates(BattleStates.BattleChoices);
        }

        // check if enamy died
}

    public void AttackButton()
    {
        SwitchBattleStates(BattleStates.BattleAttack);
    }

    public void SkillButton()
    {
        SwitchBattleStates(BattleStates.SkillAttack);
    }
    public void ItemButton()
    {
        SwitchBattleStates(BattleStates.BattleItems);
    }




    //   .......................................................................... Skill Attack do
    public void DoSkillAttack(int id)
    {
        Debug.Log("EnterdAttackButton");

        // player gose first
        if (player.Speed >= TempEnamy.Speed )
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

                        int Damage = TempEnamy.Hp + TempEnamy.Hp * TempEnamy.Defence / 100 - player.Attack + player.Attack * PlayerSkills[i].AtkPower / 100 * crit;
                        // check if enamy died
                        // TempEnamy.Hp -= Damage;

                        // alterd 
                        TempEnamy.Hp -= 100;
                        Debug.Log("Damage" + Damage);
                        Debug.Log("enamHp" + TempEnamy.Hp);
                        if (TempEnamy.Hp <= 0)
                        {
                            TempEnamy.Hp = 0;
                            updateDamageBars();
                            // other stuf 
                            SwitchBattleStates(BattleStates.EnamyDies);
                        }
                        else
                        {
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
                            int EnamyDamage = player.Hp + player.Hp * player.Defence / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                            player.Hp -= EnamyDamage;

                            Debug.Log("PlayerDamage" + EnamyDamage);
                            Debug.Log("Player" + player.Hp);
                            if (player.Hp <= 0)
                            {
                                player.Hp = 0;
                                updateDamageBars();
                                SwitchBattleStates(BattleStates.PlayerDies);
                            }
                            else
                            {
                                updateDamageBars();
                                // go back to battle choices
                                SwitchBattleStates(BattleStates.BattleChoices);
                            }

                            // check if enamy died


                        }
                    }
              

            }
        }
        // enamy gose first
        else if (player.Speed < TempEnamy.Speed)
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
                int EnamyDamage = player.Hp + player.Hp * player.Defence / 100 - TempEnamy.Attack + TempEnamy.Attack * EnamyAttacks[RandomEnamyAttack].AtkPower / 100 * crit;
                player.Hp -= EnamyDamage;
                if (player.Hp <= 0)
                {
                    player.Hp = 0;
                    updateDamageBars();
                    SwitchBattleStates(BattleStates.PlayerDies);
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

                    int Damage = TempEnamy.Hp + TempEnamy.Hp * TempEnamy.Defence / 100 - player.Attack + player.Attack * PlayerSkills[i].AtkPower / 100 * crit;
                    // check if enamy died
                    TempEnamy.Hp -= Damage;
                    if (TempEnamy.Hp <= 0)
                    {
                        TempEnamy.Hp = 0;
                        updateDamageBars();
                        // other stuf 
                        SwitchBattleStates(BattleStates.EnamyDies);
                    }
                    else
                    {
                        updateDamageBars();
                        SwitchBattleStates(BattleStates.BattleChoices);
                    }





                }


            }
        }
    }

}
