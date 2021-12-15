using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputController : MonoBehaviour
{
   
//------------------------------------------------------------------------
//Predefined Script Variables   
 //Input Controller is where I eventually want to move all my key triggers to keep them all organized in one place
    public GameObject UI;
    public GameObject player;



//------------------------------------------------------------------------
//Unity Defined Functions
    void Update(){ //Used for singular non movement button inputs like menus and interactiosn
        //general Input Logic
        if(Input.GetKeyDown(KeyCode.I)){
            player.SendMessage("loadInventoryGUI");
            Debug.Log("Inventory Open");
        }
        if(Input.GetKeyDown(KeyCode.O)){
            Debug.Log("Inventory Close");
            UI.SendMessage("disableUIItems");
        }
        if(Input.GetKeyDown(KeyCode.E)){
            player.SendMessage("triggerInteract");
        }

       //Move logic is staying in player controller right now because the locking system is weird to implement here so I'm working on it
        
    }
    void Start(){
        UI = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");
    }
}
