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
    public GameObject inventoryRowPrefab;
    private GameObject UI;
    private GameObject inventoryUI;
    private GameObject player;

    private int currentSelection = 0;
    public static bool inAction;
    private GameObject[] currentItemObjects;


//------------------------------------------------------------------------
//User Defined Functions

    void menuInput(string inputPassed){
        if(inAction){
            Debug.Log(inputPassed);
            //Going to use this input to scroll through basically
        }
    }

    public static void invStatusUpdate(bool status){
        if(status){
            GameObject.FindWithTag("inventory").SendMessage("initController");
        }else{
            inAction = false;
        }
    }

    public void initController(){
        currentSelection = 0;
        inAction = true;
        //THIS IS WHERE IM LEAVING OFF FOR THE DAY AND AM GOING TO START CODING THE SELECTION AND MENU MOVENMENT PARTS
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        inAction = false;
        UI = GameObject.FindWithTag("UI");

    }
    void Start(){
       
    }

    

}
