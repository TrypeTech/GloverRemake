using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    
    public float currentHP ;
    public int HP;
    public int Attack;
    public int Defence;
    public int Speed;
    public int Level;
    public int Power;
    public int PointsToLevel;
    public int CurrentPointsToLevel;
    public int PointsToLevelMultiplayer = 2;

    public List<Player> players = new List<Player>();


    // Start is called before the first frame update
    void Start()
    {
        ConstructPlayers();
        SetPlayerStats();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CheckCanLevelUp(int PointsGained)
    {
        CurrentPointsToLevel += PointsGained;
        if(CurrentPointsToLevel >= PointsToLevel)
        {
            // if Points have pass the amout of points needed to level up level up
            Level += 1;
            int leftPoints;

            leftPoints = CurrentPointsToLevel - PointsToLevel;
            // increase the amout of points to level by mulitplying it by the  muliplayer
            PointsToLevel *= PointsToLevelMultiplayer;
            // if there are any exp points left add them to the next round of leveling points
            CurrentPointsToLevel = leftPoints;
        }
    }


    public void TakeDamage(float Damage)
    {
        currentHP -= Damage;
        if(currentHP < 0)
        {
            currentHP = 0;
            Debug.Log("Player Has Died");
        }
    }

    public void GainHealth(float HealthAmount)
    {
        currentHP += HealthAmount;
        if(currentHP > HP)
        {
            currentHP = HP;
            
        }
    }
   public class Player
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }
        public int Level { get; set; }
        public int Power { get; set; }
        public Player(int id,string name,string desc,int hp,int attack,int defence,int speed, int level,int power)
        {
            this.ID = id;
            this.Name = name;
            this.Description = desc;
            this.HP = hp;
            this.Attack = attack;
            this.Defence = defence;
            this.Speed = speed;
            this.Level = level;
            this.Power = power;

        }
    }

    public void ConstructPlayers()
    {
        players.Add(new Player(0, "Kibler", "Might worrior", 100, 10, 30, 40, 5, 20));
    }

    public void SetPlayerStats()
    {
        HP = players[0].HP;
        currentHP = HP;
        Attack = players[0].Attack;
        Defence = players[0].Defence;
        Speed = players[0].Speed;
        Power = players[0].Power;

    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {
            // Do Damage to enamy
        }
    }
}
