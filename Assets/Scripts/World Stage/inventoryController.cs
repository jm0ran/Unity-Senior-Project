using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inventoryController : MonoBehaviour
{
//------------------------------------------------------------------------
//Predefined variables for the script
    public inventory playerInv;
    //just using Unity Editor to assign this
    public GameObject inventoryRowPrefab;
    private GameObject inventoryUI;

//------------------------------------------------------------------------
//User Defined Functions
    
//------------------------------------------------------------------------
//Unity Defined Functions
    void Start()
    {
        // //Creates and loads player inventory
        // playerInv = new inventory();
        // playerInv.readFromJson();
        
        // //Saves the player inventory data to streaming assets
        // playerInv.saveToJson();

        inventoryUI = GameObject.FindWithTag("inventory");

        Debug.Log("run");
        GameObject newInvRow = Instantiate(inventoryRowPrefab, new Vector3(400, 400, 0), Quaternion.identity);
        newInvRow.transform.SetParent(inventoryUI.transform);

    }

}
