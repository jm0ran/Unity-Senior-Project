using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chestController : MonoBehaviour
{
    //Variables for the program
    public GameObject player;
    public Sprite altSprite;
    public bool triggered = false;

    //User defined functions
    public void openChest(){
        //This is where I want to open the chest and pass all my items to my player
        //I want to be checking for stacks and just generally want to be interacting with the inventory controller and inventory in general which should be instantiated inside the player script
        if(altSprite != null && !triggered){
            gameObject.GetComponent<SpriteRenderer>().sprite = altSprite;
            triggered = true;
        }
        Debug.Log("Triggered an openChest");
    }


    //Unity Defined functions
    public void Awake(){
        player = GameObject.FindWithTag("Player"); //Grabs the player object for the chest to interact with and send messages to
    }
    
}
