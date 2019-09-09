using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour {


    public List<Item> items = new List<Item>();
	// Use this for initialization
	void Start () {
        constructItems();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public class Item
    {
        
        public int itemNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        // atributes
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }
        public int Accuracy { get; set; }
        public int Hp { get; set; }
        public int Evasion { get; set; }
        public int SpDefence { get; set; }
        public int SpAttack { get; set; }
        public string BuffType { get; set; }

        public enum ItemType
        {
            Useable,
            Eqiptable,
            Quest,
            Craft,
            Empty
        }
        public ItemType type;
        public int Count { get; set; }
        public bool Equipted { get; set; }
        public int Index { get; set; }
        public enum EquiptType
        {
            MainAttacker,
            MainArmor,
            MainEmblem,
            SubAttacker,
            SubArmor,
            SubeEmblem,
            MinorAttacker,
            MinorEmblem,
            MinorArmor,
            none
        }
        public EquiptType equiptType { get; set; }

        // store properties
        public bool canBuy { get; set; }
        public int Cost { get; set; }
        public Item (int num,string name,string desc,int attack,int def,int speed, int acc, int hp, int evase,int spdef, int spAtk,string buff,ItemType tpe,int count,bool equipt,int index,EquiptType typ,bool canB,int cost)
        {
            this.itemNumber = num;
            this.Name = name;
            this.Description = desc;
            this.Attack = attack;
            this.Defence = def;
            this.Speed = speed;
            this.Accuracy = acc;
            this.Hp = hp;
            this.Evasion = evase;
            this.SpDefence = spdef;
            this.SpAttack = spAtk;
            this.BuffType = buff;
            this.type = tpe;
            this.Count = count;
            this.Equipted = equipt;
            this.Index = index;
            this.equiptType = typ;
            this.canBuy = canB;
            this.Cost = cost;
        }
    }

    public void constructItems()
    {
        // make a order where items of same type r grouped together
        items.Add(new Item(0, "-", "No Description", 0, 0, 0, 0, 0, 0, 0, 0, "None", Item.ItemType.Empty,0,false,0,Item.EquiptType.none,false,0));
        items.Add(new Item(1, "Health", "cures 10 health points", 0, 0, 0, 0, 10, 0, 0, 0, "None", Item.ItemType.Useable,0,false,0, Item.EquiptType.none,true,10));
        items.Add(new Item(2, "Sword", "Might Sword that increases Attack", 10, 0, 0, 0, 0, 0, 0, 0, "None", Item.ItemType.Eqiptable,0,false,0, Item.EquiptType.SubAttacker,false,10));
        items.Add(new Item(3, "Sheld", "Might Shield That increases Defence", 0, 10, 0, 0, 0, 0, 0, 0, "None", Item.ItemType.Eqiptable,0,false,0, Item.EquiptType.SubArmor, false, 10));
        items.Add(new Item(4, "JustusEmblem", "Nobel item that allows passege that gives good luck", 0, 0, 0, 10, 0, 10, 0, 0, "None", Item.ItemType.Quest,0,false,0, Item.EquiptType.none, false, 10));
        items.Add(new Item(5, "nobleEmblem", "Nobel item that allows passege that gives good luck", 0, 0, 0, 10, 0, 10, 0, 0, "None", Item.ItemType.Quest, 0, false, 0, Item.EquiptType.none, false, 10));
        items.Add(new Item(6, "wayEmblem", "Nobel item that allows passege that gives good luck", 0, 0, 0, 10, 0, 10, 0, 0, "None", Item.ItemType.Quest, 0, false, 0, Item.EquiptType.none, false, 10));
        items.Add(new Item(7, "part1", "part of 3 part that make a item", 0, 0, 0, 0, 0, 0, 0, 0, "None", Item.ItemType.Craft,0,false,0, Item.EquiptType.none, false, 10));
    }
}
