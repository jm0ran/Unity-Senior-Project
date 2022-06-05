using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Text;

//The inventory system stores inventory data and is part of the save class

[System.Serializable] //Serializable to JSON
public class Inventory
{
    public List<Item> items;//List to store items
    public Inventory(){ //Constructor function for my inventory
        this.items = new List<Item>(); //Creates list
    } 

    public void addObj(string itemName, int itemAmount){ //Function to addObj to inventory, will be called by other scripts
        this.items.Add(new Item(itemName, itemAmount)); //Adds items from arguments
        this.balance(); //Balances inv after adding a new item
        saveDataController.globalSave.serializeSaveData(); //Serialize the list after adding an item
    }

    public void remObj(string itemName, int itemAmount){ //Going to be used to remove objects
        this.balance(); //Balances inv before deleting anything
        int itemIndex = findObj(itemName); //Looks for item in list
        if(itemIndex != -1){ //If item exists
            if((this.items[itemIndex].itemAmount - itemAmount) < 1){ //if only one exists
                this.items.RemoveAt(itemIndex); //Removes item
            }else{  //If more than two exists
                this.items[itemIndex].itemAmount -= itemAmount; //Subtract corresponding amount
            }
        }else{
            Debug.Log("Could not locate item for deletion: " + itemName); //Alert of item to delete being unable to be found
        }
    }

    public int findObj(string itemName){ //Going to be used to search for and find objects based on the item name and return their index, will return the first item in the array so we need a balancing function
        int itemLoc = -1; //Defaulting item location to -1, so I can return this if no item is found
        for (int i = 0; i < this.items.Count; i++){ //For each item
            if(this.items[i].itemName == itemName){ //If item name matches
                itemLoc = i; //Pass the index
                break; //Breaks out of the loop if the item is found
            }
        }
        return itemLoc; //Returns the index of the item location
    }

    public void balance(){
        //Checks for integrity of the inventory and combines duplicate items
        for (int i = 0; i < this.items.Count; i++){ //For each item
            if(this.items[i].itemAmount < 1){ // If item amount is less than one
                this.items.RemoveAt(i); //Remove item
            }
            for(int p = i + 1; p < this.items.Count; p++){ //Starts at the second item and moves forward to check for dupe items
                if(this.items[i].itemName == this.items[p].itemName){ //If item is dupe
                    this.items[i].itemAmount += this.items[p].itemAmount; //Add item values
                    this.items.RemoveAt(p); //Remove the dupe
                }
            }
        }
    }

    public void saveToJson(){ //Function to serialize my inventory object to a json file, no longer used because of save object encompassing inventory
        string jsonDataW = JsonUtility.ToJson(this); //Convert to json
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "invData.json"), jsonDataW); //Write json to file
    }

    public void readFromJson(){ //Reads from json and creates an empty list if the json doesnt exist, again deprecated because of save object 
        string jsonDataR = ""; //Storage for reading data
        try{ //Try to read
            jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "invData.json")); //Read json
            if(jsonDataR != ""){ //If not empty
                this.items = JsonUtility.FromJson<Inventory>(jsonDataR).items; //Create new object with data
            }else{ //If empty
                this.items = new List<Item>(); //Make new empty inventory
            }
        }catch(Exception e){ //Catch error
            Debug.Log("Could not locate the json file: " + e); //Log error
            this.items = new List<Item>(); //Creates a new empty list if the inventory data cannot be found
        }
        this.balance(); //Balances list upon being added
    }
}
