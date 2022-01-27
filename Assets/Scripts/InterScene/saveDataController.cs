using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataController : MonoBehaviour
{
    public static Save globalSave;

    void Awake(){
        globalSave = new Save();
        globalSave.loadSavaData(); //Loads the save data up as a static variable so everyone can have a piece
        globalSave.pruneTeam();
    }
}
