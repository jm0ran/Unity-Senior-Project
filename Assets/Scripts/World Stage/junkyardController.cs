using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class junkyardController : MonoBehaviour
{
    //The main role of this controller is going to be to manage the state of specifically the junkyard, there will be others in this style for each map just to handle spawning of entities based on one time events and the sorts
    //Should be reached right now by a send message to the object with the tag "Map"

    //Objects
    private GameObject ned;


    void Start(){ //This will determine who to spawn and all of that
        ned = GameObject.Find("ned");
       
        if(saveDataController.globalSave.oneTimes[3]){ //If initial speak with ned
            Destroy(ned);
        }
        
        

    }
}
