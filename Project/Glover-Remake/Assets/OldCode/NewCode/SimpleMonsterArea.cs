using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleMonsterArea : MonoBehaviour {

    public enum AreaType
    {
        area1,
        area2,
        area3,
        area4
    }
    public int LevelLimit = 5;

    public AreaType area;
    public SimpleClasses clas;
    public SimpleBattleHandler battleEvent;

    // Use this for initialization
    void Start()
    {
        clas = FindObjectOfType<SimpleClasses>();
        battleEvent = FindObjectOfType<SimpleBattleHandler>();

        area = AreaType.area1;
    }

    // Update is called once per frame
    void Update()
    {

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.Log("PlayerEnteredBattleTrigger...");
            StartBattle(collision.gameObject.GetComponent<SimpleMove>());
        }
    }

    public void StartBattle(SimpleMove move)
    {
        Debug.Log("Hit the trigger");

        List<SimpleClasses.Enamy> EnamyList = new List<SimpleClasses.Enamy>();

        // set up for different areas
        if (area == AreaType.area1)
        {
            for (int i = 0; i < clas.enamies.Count; i++)
            {
                if (clas.enamies[i].classType == SimpleClasses.TypeClass.normal)
                {
                    EnamyList.Add(clas.enamies[i]);
                }
            }

            int enami = Random.Range(0, EnamyList.Count);
            int enamyLv = Random.Range(LevelLimit - 3, LevelLimit);
            EnamyList[enami].Level = enamyLv;
            battleEvent.StartBattle(EnamyList[enami],move);
        }
    }
}
