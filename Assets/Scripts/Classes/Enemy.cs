using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Enemy
{
    public string name;
    public float maxHealth;
    public float currentHealth;
    public int level;
    public Move[] moves;
    
    public Enemy(string name, float maxHealth, Move[] moves){ //Used for me to create new characters
        this.name = name;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.level = 1;
        this.moves = moves;
    }

    public Enemy(string charName){ //Going to load a character based on the template json files using a string declared in unity editor, but will later be dynamically declared when entering battle
        string jsonDataR;
        try{
            jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "enemies", charName + ".json"));
            Enemy acquiredData = JsonUtility.FromJson<Enemy>(jsonDataR);
            this.name = acquiredData.name;
            this.maxHealth = acquiredData.maxHealth;
            this.currentHealth = acquiredData.maxHealth;
            this.level = acquiredData.level;
            this.moves = acquiredData.moves;
        }catch{
            Debug.Log("Failed to instantiate the character " + charName + " reverting to defaults");
            this.name = null;
            this.maxHealth = 0f;
            this.currentHealth = 0f;
            this.level = 0;
            this.moves = new Move[4];
        }
    }

    public void serialize(){
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "enemies", this.name + ".json"), jsonDataW);
    }
}
