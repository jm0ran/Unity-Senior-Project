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
    public void saveToJson(){ //Function to serialize my inventory object to a json file
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText("./testData.json", jsonDataW);
    }
    public void readFromJson(){
        string jsonDataR = System.IO.File.ReadAllText("./testData.json");
        this.items = JsonUtility.FromJson<inventory>(jsonDataR).items;
    }
}
