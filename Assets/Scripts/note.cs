using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class note{
    public float time;
    public string button;

    //Constructor
    public note(float timeA, string buttonA){
        this.time = timeA;
        this.button = buttonA;
    }
}
