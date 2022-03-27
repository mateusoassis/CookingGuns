using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BaseItem
{  
        public string Name = null;
        //public GameObject Item;
        [Range(0, 100)]
        public int Quantity = 0;
}
