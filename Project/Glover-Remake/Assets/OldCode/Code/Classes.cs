using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Classes : MonoBehaviour {


    public List<Player> players = new List<Player>();
    public List<Enamy> Enamies = new List<Enamy>();
    public enum TypeClass
    {
        earth,
        water,
        fire,
        nature,
        electric,
        normal,
        player

    }
    // Use this for initialization
    void Start () {
        ConstructGameClasses();

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public class Player {
        public int PlayerNumber { get; set; }
        public string PlayerName { get; set; }
        public string PlayerDesc { get; set; }

        /// base stats
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int SpAttack { get; set; }
        public int SpDefence { get; set; }

        public int pointsToLevel { get; set; }
        public int currentlvPoints { get; set; }
        public TypeClass classType { get; set;}

       public Player (int pNumber,string name,string description,int lv,int hp,int atk,int def,int speed,int evase,int acc,int spAtk,int spDef,int ptL,int clp,TypeClass clas)
        {
            this.PlayerNumber = pNumber;
            this.PlayerName = name;
            this.PlayerDesc = description;
            this.Level = lv;
            this.Hp = hp;
            this.Attack = atk;
            this.Defence = def;
            this.Speed = speed;
            this.Accuracy = acc;
            this.Evasion = evase;
            this.SpAttack = spAtk;
            this.SpDefence = spDef;
            this.pointsToLevel = ptL;
            this.currentlvPoints = clp;
            this.classType = clas;

        }
        
    }
    //........................................................Enamy class
    public class Enamy
    {
        public int Number { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        /// base stats
        public int Level { get; set; }
        public int Hp { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int SpAttack { get; set; }
        public int SpDefence { get; set; }

        public int pointsToLevel { get; set; }
        public int currentlvPoints { get; set; }
        public TypeClass classType { get; set; }
        public int ItemDropId { get; set; }

        public Enamy(int pNumber, string name, string description, int lv, int hp, int atk, int def, int speed, int evase, int acc, int spAtk, int spDef, int ptL, int clp, TypeClass clas, int item)
        {
            this.Number = pNumber;
            this.Name = name;
            this.Description = description;
            this.Level = lv;
            this.Hp = hp;
            this.Attack = atk;
            this.Defence = def;
            this.Speed = speed;
            this.Accuracy = acc;
            this.Evasion = evase;
            this.SpAttack = spAtk;
            this.SpDefence = spDef;
            this.pointsToLevel = ptL;
            this.currentlvPoints = clp;
            this.classType = clas;
            this.ItemDropId = item;

        }

    }

    public void ConstructGameClasses()
    {
        // Player lists
        players.Add(new Player(0, "Cleft", "Guy Who has Experiance", 5, 50, 10, 10, 20, 0, 100, 0, 10, 10, 0, TypeClass.player)) ;
        players.Add(new Player(1, "Gloryia", "Noble queen", 5, 50, 10, 10, 20, 0, 100, 0, 10, 10, 0, TypeClass.player));
        // Enamy Lists
        Enamies.Add(new Enamy(0, "RockMag", "creature That Dwells in clifs", 5, 50, 10, 10, 20, 0, 100, 0, 10, 10, 0, TypeClass.normal,1));
        Enamies.Add(new Enamy(1, "FizleBuz", "creature That Dwells Where there is Much Thunder", 5, 50, 10, 10, 20, 0, 100, 0, 10, 10, 0, TypeClass.normal,1));
    }
}
