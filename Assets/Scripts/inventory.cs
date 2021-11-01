using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class inventory
{
    public List<item> items;

    public inventory(){
        this.items = new List<item>();
    } 
    public void addObj(string itemName, int itemAmount){
        this.items.Add(new item(itemName, itemAmount));
    }
}
