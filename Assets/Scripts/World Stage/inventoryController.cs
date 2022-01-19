using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventoryController : MonoBehaviour
{
//------------------------------------------------------------------------
//Predefined variables for the script
    public static Inventory playerInv;
    //just using Unity Editor to assign this
    public GameObject inventoryRowPrefab;
    private GameObject UI;
    private GameObject inventoryUI;

//------------------------------------------------------------------------
//User Defined Functions
    void initInventory(){
        //Creates and loads player inventory
        playerInv = new Inventory();
        playerInv.readFromJson();
    }

    void loadInventoryGUI(){ //Prob want to maybe move this to UI controller not sure yet
        gameObject.GetComponent<inputController>().inInventory = true;
        gameObject.SendMessage("lockPlayer", true);
        for(int i = 0; i < playerInv.items.Count; i++){
            Vector3 rowLocation = new Vector3(-530,(-590) + (170 * (i + 1)), 0);
            GameObject newInvRow = Instantiate(inventoryRowPrefab, rowLocation, Quaternion.identity);
            newInvRow.transform.SetParent(inventoryUI.transform, false);
            newInvRow.transform.Find("invName").GetComponent<TextMeshProUGUI>().text = playerInv.items[i].itemName;
        }
        UI.SendMessage("enableUIItem", "inventory");
    }

    void exitInv(){
        UI.SendMessage("disableUIItems");
        foreach(Transform child in inventoryUI.transform){
            GameObject.Destroy(child.gameObject);
        }
        gameObject.GetComponent<inputController>().inInventory = false;
        gameObject.SendMessage("lockPlayer", false);
    }


//------------------------------------------------------------------------
//Unity Defined Functions
    void Start()
    {
        inventoryUI = GameObject.FindWithTag("inventory");
        UI = GameObject.FindWithTag("UI");

        initInventory();
        
        //GameObject newInvRow = Instantiate(inventoryRowPrefab, new Vector3(400, 400, 0), Quaternion.identity);
        //newInvRow.transform.SetParent(inventoryUI.transform);


        //Saves the player inventory data to streaming assets
        playerInv.saveToJson();
    }

}
