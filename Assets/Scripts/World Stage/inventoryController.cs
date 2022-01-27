using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

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
    void loadInventoryGUI(){ //Prob want to maybe move this to UI controller not sure yet
        player.GetComponent<inputController>().inInventory = true;
        player.SendMessage("lockPlayer", true);
        for(int i = 0; i < playerInv.items.Count; i++){
            Vector3 rowLocation = new Vector3(-530,(-590) + (170 * (i + 1)), 0);
            GameObject newInvRow = Instantiate(inventoryRowPrefab, rowLocation, Quaternion.identity);
            newInvRow.transform.SetParent(inventoryUI.transform, false);
            newInvRow.transform.Find("invName").GetComponent<TextMeshProUGUI>().text = playerInv.items[i].itemName;
            //This is where I want to also assign the uh yk thing, but I need to move this script somewhere else now that inventory data is part of the globalSave object
        }
        UI.SendMessage("enableUIItem", "inventory");
    }

    void exitInv(){
        UI.SendMessage("disableUIItems");
        foreach(Transform child in inventoryUI.transform){
            GameObject.Destroy(child.gameObject);
        }
        player.GetComponent<inputController>().inInventory = false;
        player.SendMessage("lockPlayer", false);
    }


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
