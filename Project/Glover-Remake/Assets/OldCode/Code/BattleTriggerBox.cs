using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BattleTriggerBox : MonoBehaviour {

    public enum Areas
    {
        area1,
        area2,
        area3,
        area4

    }
    public int LevelLimit = 5;

    public Areas area;
    public Classes clas;
    public BattleEvent battleEvent;
  //  public List<Classes.Enamy> EnamyList = new List<Classes.Enamy>();
   
	// Use this for initialization
	void Start () {
        clas = FindObjectOfType<Classes>();
        battleEvent = FindObjectOfType<BattleEvent>();
        // temp
        area = Areas.area1;
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}


    void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            Debug.Log("PlayerEnterTrigger");
            StartBattle();
        }
    }

    public void StartBattle()
    {

      Debug.Log("Hit the Trigger............................... Trigger");
      
       List<Classes.Enamy> EnamyList = new List<Classes.Enamy>();
   
        if (area == Areas.area1)
        {
          
            for(int i = 0; i < clas.Enamies.Count; i++)
            {
              //  if(clas.Enamies[i].classType == Classes.TypeClass.normal && clas.Enamies[i].Level <= LevelLimit)
                    if (clas.Enamies[i].classType == Classes.TypeClass.normal )
                    {
                    EnamyList.Add(clas.Enamies[i]);
                    Debug.Log("added enamy");
                }
            }
            Debug.Log(EnamyList.Count);
            int enami = Random.Range(0, EnamyList.Count );
            Debug.Log(EnamyList.Count + "number" + enami + EnamyList[enami].Name);

            int enamyLv = Random.Range(LevelLimit - 3, LevelLimit);
            EnamyList[enami].Level = enamyLv;
            battleEvent.StartBattle(EnamyList[enami]);
        }
    }
}
