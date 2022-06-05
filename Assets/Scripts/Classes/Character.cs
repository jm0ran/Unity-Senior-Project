using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//Class for characters, mostly deprecated upon the revamp of the battle system

[System.Serializable] //Serializable to json
public class Character
{
    public string name; //Character name
    public int maxHealth; //Charcter max health
    public int currentHealth; //Character current health
    public int level; //Character level
    public Move[] moves; //Array of character moves

    public Character(string name, int maxHealth, Move[] moves){ //Constructor function
        //Assigns passed values
        this.name = name;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.level = 1; //Characters are created at the default level of 1
        this.moves = moves;
    }

    public void changeMove(int moveIndex, string moveName, int moveMaxUses, int moveDamage){ //Changes a move based on passed values
        this.moves[moveIndex] = new Move(moveName, moveMaxUses, moveDamage);
    }

    public void serialize(){ //Serialize character, function used purely in development to create character templates
        string jsonDataW = JsonUtility.ToJson(this); //Convers to JSON
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "characters", this.name + ".json"), jsonDataW); //Writes json to a file
    }
}
