using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The note class is used to store data for each note in the beatMap class

[System.Serializable] //Serializable to JSON
public class note{
    public float time; //Note time target
    public string button; //Note button type

    public note(float timeA, string buttonA){ //Constructor
        //Assigns appropriate values
        this.time = timeA;
        this.button = buttonA;
    }
}
