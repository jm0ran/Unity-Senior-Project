using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inventoryController : MonoBehaviour
{
    public inventory playerInv;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Active");
        //Code to test the inventory system
        
        playerInv = new inventory();
        playerInv.readFromJson();
        // playerInv.addObj("Item Name Testing", 2);
        // playerInv.addObj("Locate Item", 3);
        // playerInv.addObj("Item Name Testing", 1);
        // playerInv.addObj("Locate Item", 2);
        // Debug.Log(playerInv.findObj("Locate Item"));
        playerInv.balance();
        playerInv.saveToJson();

    }

}
