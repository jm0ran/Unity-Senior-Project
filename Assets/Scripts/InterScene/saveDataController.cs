using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataController : MonoBehaviour
{

//Incredibly small function used to load Save Data in

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public static Save globalSave; //Storage for save object static for easy access

//------------------------------------------------------------------------
//Unity Defined Function
    void Awake(){
        globalSave = new Save(); //Instantiate save object
        globalSave.loadSavaData(); //Loads the save data up as a static variable so everyone can have a piece
        globalSave.pruneTeam(); //Prune team to verify data is valid
    }
}
