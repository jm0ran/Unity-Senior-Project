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

//------------------------------------------------------------------------
//User Defined Functions
//Probably want to move a lot of this off into the inventory menu object as it will likely only cause problems if it is here

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        player = GameObject.FindWithTag("Player");
        inventoryUI = GameObject.FindWithTag("inventory");
        UI = GameObject.FindWithTag("UI");
    }
    void Start(){
        playerInv = saveDataController.globalSave.inventory;
    }

}
