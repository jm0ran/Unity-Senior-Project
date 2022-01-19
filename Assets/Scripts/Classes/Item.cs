using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item{ //I removed monobehavior here which may or may not be a bad idea
    //This is going to be my item class, properties
    public string itemName;
    public int itemAmount;

    //This is the constructor for my class
    public Item(string itemName, int itemAmount){
        this.itemName = itemName;
        this.itemAmount = itemAmount;
    }
    
}
