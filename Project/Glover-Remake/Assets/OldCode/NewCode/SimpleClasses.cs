using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleClasses : MonoBehaviour {


    public List<Player> players = new List<Player>();
    public List<Enamy> enamies = new List<Enamy>();
    public enum TypeClass
    {
        // set type classes for game there
        earth,
        water,
        fire,
        air,
        normal,
        nature,
        other
    }
	// Use this for initialization
	void Start () {
        ConstructGameClasses();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public class Player
    {
        public int PlayerNumber { get; set; }
        public string PlayerName { get; set; }
        public string PlayerDescription { get; set; }

        // base stats
        public int Level { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }

        // stats to add
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int SpAttack { get; set; }
        public int SpDefence { get; set; }

        // type class of player
        public TypeClass classType { get; set; }
        

        public Player (int numb,string name,string desc,int lvl,int hp, int atk,int def,int spd,TypeClass clas)
        {
            this.PlayerNumber = numb;
            this.PlayerName = name;
            this.PlayerDescription = desc;
            this.Level = lvl;
            this.Health = hp;
            this.Attack = atk;
            this.Defence = def;
            this.Speed = spd;
            this.classType = clas;
        }
    }
    public class Enamy
    {

        public int EnamyNumber { get; set; }
        public string EnamyName { get; set; }
        public string EnamyDescription { get; set; }

        // base stats
        public int Level { get; set; }
        public int Health { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }

        // stats to add
        public int Accuracy { get; set; }
        public int Evasion { get; set; }
        public int SpAttack { get; set; }
        public int SpDefence { get; set; }

        // type class of player
        public TypeClass classType { get; set; }


        public Enamy(int numb, string name, string desc, int lvl, int hp, int atk, int def, int spd, TypeClass clas)
        {
            this.EnamyNumber = numb;
            this.EnamyName = name;
            this.EnamyDescription = desc;
            this.Level = lvl;
            this.Health = hp;
            this.Attack = atk;
            this.Defence = def;
            this.Speed = spd;
            this.classType = clas;
        }
    }

    public void ConstructGameClasses()
    {
        // player classes
        players.Add(new Player(0, "Yuri", "Epic girl knight", 3,100, 10, 10, 4, TypeClass.normal));
        players.Add(new Player(1, "Uno", "Epic girl knight", 3, 50, 10, 10, 10, TypeClass.normal));

        // enamy classes
        enamies.Add(new Enamy(0, "BolderGiant", "An Epic Bolder worrior", 10, 10, 10, 10, 3, TypeClass.normal));
        enamies.Add(new Enamy(0, "BolderGiant", "An Epic Bolder worrior", 2, 10, 4, 4, 4, TypeClass.earth));
        enamies.Add(new Enamy(1, "FireBugli", "An Epic Fire Creature", 2, 10, 4, 4, 4, TypeClass.fire));
        enamies.Add(new Enamy(2, "WaterKnight", "An Epic water worrior", 2, 10, 4, 4, 4, TypeClass.water));
    }
}
