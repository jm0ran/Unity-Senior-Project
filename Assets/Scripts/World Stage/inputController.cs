using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputController : MonoBehaviour
{
   
//------------------------------------------------------------------------
//Predefined Script Variables   
 //Input Controller is where I eventually want to move all my key triggers to keep them all organized in one place
    public GameObject UI;
    public GameObject player;

    //STATES
    public bool playerLocked = false;
    public bool inInventory = false;
    public bool inDialogue = false;



//User Defined Functions




//------------------------------------------------------------------------
//Unity Defined Functions
    void Update(){ //Used for singular non movement button inputs like menus and interactiosn
        //general Input Logic
        if(Input.GetKeyDown(KeyCode.I)){
            if(!inInventory){
                player.SendMessage("loadInventoryGUI");
            }
            else{
                player.SendMessage("exitInv");
            }
            
        }
        if(Input.GetKeyDown(KeyCode.O)){

        }
        if(Input.GetKeyDown(KeyCode.E)){
            player.SendMessage("triggerInteract");
        }
        if(Input.GetKeyDown(KeyCode.M)){
            SceneManager.LoadScene("Battle Stage");
        }


       //Move logic is staying in player controller right now because the locking system is weird to implement here so I'm working on it
        
    }
    void Start(){
        UI = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");
    }
}
