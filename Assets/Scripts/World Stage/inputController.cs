using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputController : MonoBehaviour
{
   
//------------------------------------------------------------------------
//Predefined Script Variables   
 //Input Controller is where I eventually want to move all my key triggers to keep them all organized in one place
    private GameObject UI;
    private GameObject player;
    public GameObject persistController;
    private GameObject inventoryController;


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
            //Need to lock player during inventory but want to fix the input controller first
            if(!inInventory){
                UIController.setMenuState("inventory");
                inInventory = true;
            }
            else{
                UIController.setMenuState("none");
                inInventory = false;
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
        if(Input.GetKeyDown(KeyCode.Return)){
            UIController.returnGate = true;
        }


        //Should section this by state
        if(UIController.currentLayer == UIController.inventory){
            string inputToPass = null;
            if(Input.GetKeyDown(KeyCode.UpArrow)){
                inputToPass = "up";
            }
            if(Input.GetKeyDown(KeyCode.RightArrow)){
                inputToPass = "right";
            }
            if(Input.GetKeyDown(KeyCode.DownArrow)){
                inputToPass = "down";
            }
            if(Input.GetKeyDown(KeyCode.LeftArrow)){
                inputToPass = "left";
            }
            if(inputToPass != null){
                inventoryController.SendMessage("menuInput", inputToPass);

            }
        }


       //Move logic is staying in player controller right now because the locking system is weird to implement here so I'm working on it
        
    }
    void Awake(){
        persistController = GameObject.Find("persistController");
        UI = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");
        inventoryController = GameObject.FindWithTag("inventory");
    }

    void Start(){
        
    }
}
