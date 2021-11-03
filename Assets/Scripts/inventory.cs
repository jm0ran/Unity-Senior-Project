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
    public void remObj(string itemIndex){ //Going to be used to remove objects
        
    }
    public int findObj(string itemName){ //Going to be used to search for and find objects based on the item name and return their index
        for (int i = 0; i < this.items.Count; i++){
            Debug.Log(this.items[i].itemName);
            if(this.items[i].itemName == itemName){
                break;
            }
        }
        return 1;
    }
    public void saveToJson(){ //Function to serialize my inventory object to a json file
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText("SaveData/invData.json", jsonDataW);
    }
    public void readFromJson(){
        string jsonDataR = "";
        //Work on this try catch loop tomorrow
        
        try{
            jsonDataR = System.IO.File.ReadAllText("SaveData/invData.json");
            this.items = JsonUtility.FromJson<inventory>(jsonDataR).items;
        }catch(Exception e){
            Debug.Log("Could not locate the json file: " + e);
            this.items = new List<item>(); //Creates a new empty list if the inventory data cannot be found
        }
        
        
    }
}
