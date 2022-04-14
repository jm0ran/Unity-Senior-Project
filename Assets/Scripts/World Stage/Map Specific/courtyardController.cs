using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courtyardController : MonoBehaviour
{
    void Start(){ 
        updateScene();
    }
        
    void updateScene(){
        Debug.Log("Updated scene on courtyard");
        if(saveDataController.globalSave.oneTimes[6]){ //Logic in here to decide whether to spawn statue with or without kanye on it, going to get to later
            Debug.Log("Kanye Statue has been activated");
        }

        //When kanye statue has been triggered I need to change the sprite on it in here and also change the dialogue
    }
}
