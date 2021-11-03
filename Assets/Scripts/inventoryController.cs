using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class inventoryController : MonoBehaviour
{
    public inventory playerInv;
    // Start is called before the first frame update
    void Start()
    {
        playerInv = new inventory();
        playerInv.readFromJson();
        playerInv.addObj("Item Name Testing", 0);
        playerInv.saveToJson();
        playerInv.findObj("Locate Item");
        Debug.Log(JsonUtility.ToJson(playerInv)); //Just serializes and logs the info
    }

}
