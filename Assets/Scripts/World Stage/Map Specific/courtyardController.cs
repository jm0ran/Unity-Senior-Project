using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courtyardController : MonoBehaviour
{

//The courtyard controller is used to set state of scene based on the onetimes in the save object

//------------------------------------------------------------------------
//Unity Defined Functions
    void Start(){ 
        updateScene(); //Update scene when scene starts
        GameObject.Find("kanyeNPC").GetComponent<SpriteRenderer>().enabled = false; //Disable kanyeNPC, whow will be enabled later for dialogue
    }
        
    void updateScene(){
        if(saveDataController.globalSave.oneTimes[6]){ //If kanye statue interact has already happened
            GameObject kanyeStatue = GameObject.Find(("kanyeStatue")); //Get Reference to statue
            kanyeStatue.SendMessage("swapSprite"); //Swap statue sprite with empty sprite
            kanyeStatue.GetComponent<betterNPC>().dia = new List<string>(); //Set dialogue to empty
            kanyeStatue.GetComponent<betterNPC>().diaOrder = new List<string>(); //Set Dia order to empty
        }
    }
}
