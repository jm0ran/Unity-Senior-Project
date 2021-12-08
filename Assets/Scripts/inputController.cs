using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class inputController : MonoBehaviour
{
    //Input Controller is where I eventually want to move all my key triggers to keep them all organized in one place
    public GameObject UI;
    public GameObject player;

    void Update(){ //Used for singular non movement button inputs like menus and interactiosn
        if(Input.GetKeyDown(KeyCode.I)){
            Debug.Log("Inventory Open");
            UI.SendMessage("enableUIItem", "inventory");
        }
        if(Input.GetKeyDown(KeyCode.O)){
            Debug.Log("Inventory Close");
            UI.SendMessage("disableUIItems");
        }
        if(Input.GetKeyDown(KeyCode.E)){
            player.SendMessage("triggerInteract");
        }
    }
    void Start(){
        UI = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");
    }
}
