using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

//The inventory system is contained in a testing scene for the moment and will be implemented later

[System.Serializable]
public class inventory
{
    public List<item> items;

    public inventory(){ //Constructor function for my inventory
        this.items = new List<item>();
    } 
    public void addObj(string itemName, int itemAmount){ //Function to addObj to inventory, will be called by other scripts
        this.items.Add(new item(itemName, itemAmount));
        this.balance(); //Balances inv after adding a new item
    }
    public void remObj(string itemName, int itemAmount){ //Going to be used to remove objects
        this.balance(); //Balances inv before deleting anything
        int itemIndex = findObj(itemName);
        if(itemIndex != -1){
            if((this.items[itemIndex].itemAmount - itemAmount) < 1){
                this.items.RemoveAt(itemIndex);
            }else{
                this.items[itemIndex].itemAmount -= itemAmount;
            }
        }else{
            Debug.Log("Could not locate item for deletion: " + itemName);
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
        //Checks for integrity of the inventory and combines duplicate items
        for (int i = 0; i < this.items.Count; i++){
            if(this.items[i].itemAmount < 1){
                this.items.RemoveAt(i);
                Debug.Log("Removed a zero item");
            }
            for(int p = i + 1; p < this.items.Count; p++){ //Starts at the second item and moves forward to check for dupe items
                if(this.items[i].itemName == this.items[p].itemName){
                    this.items[i].itemAmount += this.items[p].itemAmount;
                    this.items.RemoveAt(p);
                    Debug.Log("Dupe located, merged");
                }
            }
        }
    }

    public void saveToJson(){ //Function to serialize my inventory object to a json file
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "invData.json"), jsonDataW);
    }
    public void readFromJson(){ //Reads from json and creates an empty list if the json doesnt exist
        string jsonDataR = "";
        try{
            jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "invData.json"));
            if(jsonDataR != ""){
                this.items = JsonUtility.FromJson<inventory>(jsonDataR).items;
            }else{
                Debug.Log("json file is empty");
                this.items = new List<item>();
            }
        }catch(Exception e){
            Debug.Log("Could not locate the json file: " + e);
            this.items = new List<item>(); //Creates a new empty list if the inventory data cannot be found
        }

        this.balance(); //Balances list upon being added
    }
}
