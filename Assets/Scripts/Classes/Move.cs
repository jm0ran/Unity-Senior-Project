using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Move{
    public string name;
    public int maxUses;
    public int remainingUses;
    public int damage;
    //Prob want to add in healing property too in case I add healing moves later on

    public Move(string name, int maxUses, int damage){
        this.name = name;
        this.maxUses = maxUses;
        this.remainingUses = maxUses;
        this.damage = damage;
    }
}


