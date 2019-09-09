using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftItems : MonoBehaviour {

    public List<CraftItem> CraftItemsList = new List<CraftItem>();
	// Use this for initialization
	void Start () {
        craftItemsList();
     //   Debug.Log(" item1" + CraftItemsList[0].ItemIDs[0] + CraftItemsList[0].ItemIDs[1]);
      //  Debug.Log(" item2" + CraftItemsList[1].ItemIDs[0] + CraftItemsList[1].ItemIDs[1]);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    public class CraftItem
    {
        public int ID { get; set; }
        public string ItemName { get; set; }
        public string Description { get; set; }
        public int[] ItemIDs { get; set; }
        public int CraftedItemID { get; set; }

        public CraftItem (int id,string name,string desc,int[] ItemsToCraft,int craftedItem)
        {
            this.ID = id;
            this.ItemName = name;
            this.Description = desc;
            this.ItemIDs = ItemsToCraft;
            this.CraftedItemID = craftedItem;
        }
    }

    public void craftItemsList()
    {
        // simple crafter craft list
        int[] arrayNew = new int[3];
        arrayNew[0] = 4;
        arrayNew[1] = 4;

        CraftItemsList.Add(new CraftItem(0, "simpleSword", "GreatSword of Elagence", arrayNew , 2));

       arrayNew = new int[3];
        arrayNew[0] = 5;
        arrayNew[1] = 5;

        CraftItemsList.Add(new CraftItem(0, "MegaSword", "sword of speed might", arrayNew, 6));
    }
}
