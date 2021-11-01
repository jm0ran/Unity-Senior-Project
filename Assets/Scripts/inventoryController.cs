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
        playerInv.addObj("Item Name Testing", 0);
        Debug.Log(JsonUtility.ToJson(playerInv)); //Just serializes and logs the info
    }

}
