using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

[System.Serializable]
public class inventory
{
    public List<item> items;

    public inventory(){ //Constructor function for my inventory
        this.items = new List<item>();
    } 
    public void addObj(string itemName, int itemAmount){ //Function to addObj to inventory, will be called by other scripts
        this.items.Add(new item(itemName, itemAmount));
    }
    public void remObj(int itemIndex, int amount){ //Going to be used to remove objects
        if((this.items[itemIndex].itemAmount - amount) < 1){
            this.items.RemoveAt(itemIndex);
        }else{
            this.items[itemIndex].itemAmount -= amount;
        }
    }
    public int findObj(string itemName){ //Going to be used to search for and find objects based on the item name and return their index, will return the first item in the array so we need a balancing function
        int itemLoc = -1; //Defaulting item location to -1, so I can return this if no item is found
        for (int i = 0; i < this.items.Count; i++){
            if(this.items[i].itemName == itemName){
                itemLoc = i;
                break; //Breaks out of the loop if the item is found
            }
        }
        return itemLoc; //Returns the index of the item location
    }
    public void balance(){
        //This function is going to be used to balance my array and make sure that there are no dupe listings of items, will stack items if they somehow get seperated, gonna have to code something smart for this one 
    }
    public void saveToJson(){ //Function to serialize my inventory object to a json file
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText("SaveData/invData.json", jsonDataW);
    }
    public void readFromJson(){
        string jsonDataR = "";
        try{
            jsonDataR = System.IO.File.ReadAllText("SaveData/invData.json");
            this.items = JsonUtility.FromJson<inventory>(jsonDataR).items;
        }catch(Exception e){
            Debug.Log("Could not locate the json file: " + e);
            this.items = new List<item>(); //Creates a new empty list if the inventory data cannot be found
        }
    }
}
