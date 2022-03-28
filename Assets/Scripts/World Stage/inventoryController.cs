using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class inventoryController : MonoBehaviour
{
//GOING TO EVENTUALLY MIGRATE TO THE ACTUAL INVENTORY GAME OBJECT AS THIS NO LONGER NEEDS TO BE HERE

//------------------------------------------------------------------------
//Predefined variables for the script
    private Inventory playerInv;
    //just using Unity Editor to assign this
    private GameObject UI;
    private GameObject inventoryUI;
    private static GameObject itemsContainer;
    private GameObject player;
    private List<GameObject> itemObjectsList;

    private static int currentSelection = 0; //Going to be static so that my input controller can change them (may be bad idea may be fine)
    public static bool inAction;
    private GameObject[] currentItemObjects;


//------------------------------------------------------------------------
//User Defined Functions

    void menuInput(string inputPassed){
        if(inAction){
            if(inputPassed == "down"){
                updateSelected(currentSelection + 1);
            }else if(inputPassed == "up"){
                updateSelected(currentSelection - 1);
            }
            //Going to use this input to scroll through basically
        }
    }

    public static void invStatusUpdate(bool status){ //Basically starts the inventory
        if(status){
            currentSelection = 0; //Sets default selection to 0 upon opening the menu
            GameObject.FindWithTag("inventory").SendMessage("initController");
        }else{
            inAction = false;
            //Need to get rid of all the old objects
            //Just makes sure inventory is clear, may run a bit more than I like but will be fine
            foreach(Transform child in itemsContainer.transform){
                Destroy(child.gameObject);
            }
        }
    }

    public void updateSelected(int target){ //This function is just going to clearly highlight the currently selected item and if needed move the objects up in order to  view and stuff
        //Design for this is as follows
        //1. Get list of the item objects
        //2. Obviously figure out which is selected based on current selection index
        //3. possibly move the objects in order to slide for the menu, may need to dynamically speed or just time the transition with coroutines in order to make it less jumpy
        //4. Initial implementation will likely just be skippy
        //5. write the necessary text in the funny box
        if(target >= 0 & target < itemObjectsList.Count){
            currentSelection = target; //Updates current selection to reflect a successful change

            GameObject selectedObject = itemObjectsList[target]; //Might need to add something here if inventory has less than 1 item who knows lol
            
            //I want a section here to determine if the items need to be moves down or up with funny algorithm of if and butts and coconuts
            

            for(int i = 0; i < itemObjectsList.Count; i++){ //Logic for resetting item box colors and item box positioning
                GameObject item = itemObjectsList[i];
                item.GetComponent<Image>().color = new Color32(255, 255, 255, 255);
                Transform itemRowPosition = item.GetComponent<Transform>();
                //Want to determine if I even need to slide, just generall positions my boxes
                if(itemObjectsList.Count >= 4 && currentSelection >= 2){ //Want to check if 
                    itemRowPosition.localPosition = new Vector3(0,340 - (280 * i - (280 * (currentSelection - 1))),0);
                }else{
                    itemRowPosition.localPosition = new Vector3(0,340 - (280 * i),0); 
                }
                
            }
            selectedObject.GetComponent<Image>().color = new Color32(255, 112, 112, 100); //Set selected gameObject to different color
        }
    }

    public void initController(){
        currentSelection = 0;
        inAction = true;
        itemObjectsList = new List<GameObject>();

        //The following just find the children and throws them into a list of gameObjects;
        foreach (Transform child in itemsContainer.transform){
            itemObjectsList.Add(child.gameObject);
        }
        
        updateSelected(currentSelection); //This is the initial update for selected, just initially highlights the first option
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        inAction = false;
        UI = GameObject.FindWithTag("UI");
        inventoryUI = GameObject.FindWithTag("inventory");
        itemsContainer = GameObject.Find("itemsContainer");

    }
    void Start(){
       
    }

    

}
