using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Item classes used to store data for each item in inventory

[System.Serializable] //Serializable to JSON
public class Item{
    public string itemName; //item name
    public int itemAmount; //Item amount

    public Item(string itemName, int itemAmount){ //Constructor
        //Assigns appropriate values
        this.itemName = itemName;
        this.itemAmount = itemAmount;
    }
    
}
