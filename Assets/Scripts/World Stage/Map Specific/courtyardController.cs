using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class courtyardController : MonoBehaviour
{
    void Start(){ 
        updateScene();
        GameObject.Find("kanyeNPC").GetComponent<SpriteRenderer>().enabled = false;
    }
        
    void updateScene(){
        if(saveDataController.globalSave.oneTimes[6]){ //Logic in here to decide whether to spawn statue with or without kanye on it, going to get to later
            GameObject kanyeStatue = GameObject.Find(("kanyeStatue"));
            kanyeStatue.SendMessage("swapSprite");
            kanyeStatue.GetComponent<betterNPC>().dia = new List<string>();
            kanyeStatue.GetComponent<betterNPC>().diaOrder = new List<string>();
        }

        //When kanye statue has been triggered I need to change the sprite on it in here and also change the dialogue
    }
}
