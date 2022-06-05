using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Move class is depracated upon rework of battle system

[System.Serializable] //Serializable to JSON
public class Move{
    public string name; //Move name
    public int maxUses; //Move Max Uses
    public int remainingUses; //Moves Remaining
    public int damage; //Move damage

    public Move(string name, int maxUses, int damage){ //Constructor
        //Assigns the appropriate values
        this.name = name;
        this.maxUses = maxUses;
        this.remainingUses = maxUses;
        this.damage = damage;
    }
}


