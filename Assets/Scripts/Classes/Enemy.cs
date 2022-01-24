using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Enemy
{
    public string name;
    public int maxHealth;
    public int currentHealth;
    public int level;
    public Move[] moves;
    
    public Enemy(string name, int maxHealth, Move[] moves){
        this.name = name;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.level = 1;
        this.moves = moves;
    }

    public void serialize(){
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "enemies", this.name + ".json"), jsonDataW);
    }
}
