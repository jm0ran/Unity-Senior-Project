using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

[System.Serializable]
public class Character
{
    public string name;
    public int maxHealth;
    public int currentHealth;
    public int level;
    public List<Move> moves;
    //Probably want to create a system for learning new moves at some point

    public Character(string name, int maxHealth, List<Move> moves){
        this.name = name;
        this.maxHealth = maxHealth;
        this.currentHealth = maxHealth;
        this.level = 1; //Characters are created at the default level of 1
        this.moves = moves;
    }

    public void changeMove(int moveIndex, string moveName, int moveMaxUses, int moveDamage){
        //Need a try and catch loop for if the move index doesn't exist
        //Moves should be created as a list of 4 null items, might switch to arrays latter as I think that may let me force size
        this.moves[moveIndex] = new Move(moveName, moveMaxUses, moveDamage);
    }

    public void serialize(){
        string jsonDataW = JsonUtility.ToJson(this);
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "characters", this.name + ".json"), jsonDataW);
    }
}
