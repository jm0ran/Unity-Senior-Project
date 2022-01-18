using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionButtonLayerController : MonoBehaviour
{
    void processButton(string trigger){
        if(trigger == "fight"){
            menuTransition("fightLayer");
        }
    }

    menuTransition(string dest){
        //This is where I'll go to the next menu
    }
}
