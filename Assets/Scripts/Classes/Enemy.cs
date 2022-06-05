using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Enemy class, mostly deprecated upon revamp of battle system

[System.Serializable] //Serializable to JSon
public class Enemy
{
    public string name; //Enemy Name
    public float maxHealth; //Enemy Max Health
    public float currentHealth; //Enemy current health
    public int level; //Enemy level
    public Move[] moves; //Enemy move storage
    
    public Enemy(string name, float maxHealth, Move[] moves){ //Used for me to create new characters
        //Assign the appropriate values in the constructor
        this.name = name;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.level = 1; //Default level
        this.moves = moves;
    }

    public Enemy(string charName){ //Alternate constructor by instantiating from existing json files
        string jsonDataR; //Storage for JSON Read data
        try{ //Try to load character
            jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "enemies", charName + ".json")); //Read dara
            Enemy acquiredData = JsonUtility.FromJson<Enemy>(jsonDataR); //Format acquired data
            //Assign the acquired data
            this.name = acquiredData.name;
            this.maxHealth = acquiredData.maxHealth;
            this.currentHealth = acquiredData.maxHealth;
            this.level = acquiredData.level;
            this.moves = acquiredData.moves;
        }catch{ //If read fails
            Debug.Log("Failed to instantiate the character " + charName + " reverting to defaults"); //Alert of error
            //Set to defaults 
            this.name = null;
            this.maxHealth = 0f;
            this.currentHealth = 0f;
            this.level = 0;
            this.moves = new Move[4];
        }
    }

    public void serialize(){ //Serialize the enemy, only used in development
        string jsonDataW = JsonUtility.ToJson(this); //Convert to json
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "enemies", this.name + ".json"), jsonDataW); //Write json to file
    }
}
