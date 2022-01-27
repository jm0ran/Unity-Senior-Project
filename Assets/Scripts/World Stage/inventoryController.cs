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
    private GameObject player;

//------------------------------------------------------------------------
//User Defined Functions
//Probably want to move a lot of this off into the inventory menu object as it will likely only cause problems if it is here
    void initInventory(){
        //Creates and loads player inventory
        playerInv = new Inventory();
        playerInv.readFromJson();
    }

    void loadInventoryGUI(){ //Prob want to maybe move this to UI controller not sure yet
        player.GetComponent<inputController>().inInventory = true;
        player.SendMessage("lockPlayer", true);
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
    
    void Start()
    {
        initInventory();
        playerInv.saveToJson();
    }

}
