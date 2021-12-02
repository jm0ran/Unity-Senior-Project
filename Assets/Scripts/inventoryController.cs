using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inventoryController : MonoBehaviour
{
//Pre defined variables for the script
    public inventory playerInv;

//User Defined Functions
    

//Unity Defined Functions
    void Start()
    {
        //Creates and loads player inventory
        playerInv = new inventory();
        playerInv.readFromJson();
        
        //Saves the player inventory data to streaming assets
        playerInv.saveToJson();

    }

}
