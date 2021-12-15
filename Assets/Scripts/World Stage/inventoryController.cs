using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventoryController : MonoBehaviour
{
//------------------------------------------------------------------------
//Predefined variables for the script
    public inventory playerInv;
    //just using Unity Editor to assign this
    public GameObject inventoryRowPrefab;
    private GameObject UI;
    private GameObject inventoryUI;

//------------------------------------------------------------------------
//User Defined Functions
    void initInventory(){
        //Creates and loads player inventory
        playerInv = new inventory();
        playerInv.readFromJson();
    }

    void loadInventoryGUI(){
        gameObject.SendMessage("lockPlayer", true);
        for(int i = 0; i < playerInv.items.Count; i++){
            Vector3 rowLocation = new Vector3(400, 200 * (i + 1), 0);
            GameObject newInvRow = Instantiate(inventoryRowPrefab, rowLocation, Quaternion.identity);
            newInvRow.transform.SetParent(inventoryUI.transform);
            newInvRow.transform.Find("invName").GetComponent<TextMeshProUGUI>().text = playerInv.items[i].itemName;
        }
        UI.SendMessage("enableUIItem", "inventory");
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
