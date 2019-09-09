using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventorySystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public class Item
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int HP { get; set; }
        public int Attack { get; set; }
        public int Defence { get; set; }
        public int Speed { get; set; }
        public int Power { get; set; }
        public int Level { get; set; }

        public enum ItemType{
            USABLE,
            EQUIPTABLE,
            CRAFTABLE,
            QUESTABLE
        }

        public ItemType type { get; set; }

        
    }
}
