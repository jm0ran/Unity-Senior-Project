using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class inputController : MonoBehaviour
{

//The input controller handles almost all of the input bar a few small key strokes and player movement

//------------------------------------------------------------------------
//Predefined Script Variables   
    private GameObject UI; //Reference to UI Object
    private GameObject player; //Reference to player Object
    public GameObject persistController; //Reference to persist controller
    private GameObject inventoryController; //Reference to inventoryController

    //STATES
    public bool playerLocked = false; //Is player locked
    public bool inInventory = false; //Is in inventory
    public bool inDialogue = false; //Is in dialogue

//User Defined Functions

//------------------------------------------------------------------------
//Unity Defined Functions
    void Update(){ //Used for singular non movement button inputs like menus and interactiosn
        //General Input Logic
        if(Input.GetKeyDown(KeyCode.I)){ //If player presss I
            //Need to lock player during inventory but want to fix the input controller first
            if(!inInventory){ //if not in inventory
                if(UIController.currentLayer == null){ //Clear current layer
                    UIController.setMenuState("inventory"); //Set menu state to inventory
                    inInventory = true; //Set in inventory to true
                }
            }else{ //If already in inventory
                UIController.setMenuState("none"); //exit inventory
                inInventory = false; //Set in inventory to false
            }
        }
        if(Input.GetKeyDown(KeyCode.E)){ //If player hits E
            player.SendMessage("triggerInteract"); //Trigger an interaction
        }
        if(Input.GetKeyDown(KeyCode.Return)){ //If player hits return
            UIController.returnGate = true; //Flip return gate
        }
        if(UIController.currentLayer == UIController.inventory){ //Inventory control
            string inputToPass = null; //Input to pass default is null
            //Sets input to pass to key pressed
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
            if(inputToPass != null){ //If there is an input to pass
                inventoryController.SendMessage("menuInput", inputToPass); //Send input to the inventory controller to be processes
            }
        }
    }

    void Awake(){
        //Assigns values to the appropriate variables
        persistController = GameObject.Find("persistController");
        UI = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");
        inventoryController = GameObject.FindWithTag("inventory");
    }
}
