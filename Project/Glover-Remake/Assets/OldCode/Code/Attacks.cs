using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attacks : MonoBehaviour {


    public List<attacks> AttackList = new List<attacks>();

    public Items items;
    public simpleInventory SItems;
	// Use this for initialization
	void Start () {
        items = FindObjectOfType<Items>();
        SItems = FindObjectOfType<simpleInventory>();
        ConstructAttackList();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public class attacks {

        public int AttackNumber { get; set; }
        public string Name { get; set; }
        public string Descripton { get; set; }
        public int Amount { get; set; }
        public int RecLevel { get; set; }
        public int Accuracy { get; set; }

        // other stats
        public int AtkPower { get; set; }
        public int SpPower { get; set; }

        public enum AttackType
        {
            water,
            fire,
            earth,
            wind,
            nature,
            normal,
            hero1
        }
        public AttackType type { get; set; }
        public enum EffectType
        {
            plusAttack,
            plusDefence,
            plusSpeed,
            plusAccuracy,
            plusEvasion,
            plusHp,
            DoubleAttack,
            DoubleDefence,
            DoubleSpAttack,
            DoubleSpDefence,
            DoubleSpeed,
            BuffFire,
            BuffPoison,
            BuffParalysis,
            BuffSleep,
            none
        }
        public EffectType eType;

        public bool isItemAttack { get; set; }
        public Items.Item AttackItem { get; set; }
        public simpleInventory.Item AttackItemSimple { get; set; }
        
        public attacks(int num,string name,string desc,int amount,int lvRec,int acc,int atkp,int spatkp,AttackType aType,EffectType etype,bool wepon,Items.Item item,simpleInventory.Item simpleItem)
        {
            this.AttackNumber = num;
            this.Name = name;
            this.Descripton = desc;
            this.Amount = amount;
            this.RecLevel = lvRec;
            this.Accuracy = acc;
            this.AtkPower = atkp;
            this.SpPower = spatkp;
            this.type = aType;
            this.eType = etype;
            this.isItemAttack = wepon;
            this.AttackItem = item;
            this.AttackItemSimple = simpleItem;
        }

        } 

    public void ConstructAttackList()
    {
        // normal attacks
        AttackList.Add(new attacks(0, "Rage", "FastSword hit", 10, 1, 100, 20, 20, attacks.AttackType.normal, attacks.EffectType.none,false,items.items[0],SItems.ItemsList[0]));
        AttackList.Add(new attacks(1, "rockToss", "FastSword hit", 10, 1, 100, 20, 20, attacks.AttackType.normal, attacks.EffectType.none, false, items.items[0], SItems.ItemsList[0]));

        // earth Attacks
        AttackList.Add(new attacks(2, "nobleSmash", "FastSword hit", 10, 1, 100, 20, 0, attacks.AttackType.normal, attacks.EffectType.none, false, items.items[0], SItems.ItemsList[0]));
        AttackList.Add(new attacks(3, "shakeshamsh", "FastSword hit", 10, 1, 100, 20, 0, attacks.AttackType.hero1, attacks.EffectType.none, false, items.items[0], SItems.ItemsList[0]));

        // hero 1 attacks
        AttackList.Add(new attacks(4, "HammerBash", "hard punch hit",10, 1, 100,20, 0, attacks.AttackType.normal, attacks.EffectType.none, false, items.items[0], SItems.ItemsList[0]));
        AttackList.Add(new attacks(5, "quickDraw", "FastSword hit", 10, 1, 100, 20, 0, attacks.AttackType.normal, attacks.EffectType.none, false, items.items[0], SItems.ItemsList[0]));

        //thunder attacks
        AttackList.Add(new attacks(6, "HeadBud", "hit with head", 10, 1, 100, 10, 0, attacks.AttackType.normal, attacks.EffectType.none, false, items.items[0], SItems.ItemsList[0]));

        //skill attacks that require and item to use
        // check make sure attack type and isatack item and right item is on attack
        AttackList.Add(new attacks(7, "boomCannon", "beam Of Light From Cannon", 10, 1, 100, 20, 0, attacks.AttackType.hero1, attacks.EffectType.none, true, items.items[2], SItems.ItemsList[2]));
        AttackList.Add(new attacks(8, "boomCannon", "beam Of Light From Cannon", 10, 1, 100, 20, 0, attacks.AttackType.hero1, attacks.EffectType.none, true, items.items[4], SItems.ItemsList[4]));



    }

}
