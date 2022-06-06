using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorBlocker : MonoBehaviour
{

//The door blocker controller is on all door blockers meant to prevent the player from access certain areas while returning dialogue

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public bool isActive = false; //bool for state of blocker
    public string returnDirection = "up"; //Direction for blocker to send player
    public GameObject playerObj; //Reference to player object
    public string message = ""; //Message for blocker

//------------------------------------------------------------------------
//User Defined Function
    public void recievePlayer(){ //Function to recieve player
        if(!isActive){ //If blocker is active
            StartCoroutine(recievePlayerCo()); //Starts recievePlayerCoroutine
        } 
    } 

    public IEnumerator recievePlayerCo(){ //Coroutine for recieving the player
        UIController.returnGate = false; //Sets returnGate case to fault
        isActive = true; //Sets blocker to active
        playerObj.SendMessage("lockPlayer", true); //Lock player controlled movement
        UIController.setMenuState("noPhotoDia"); //Set menu state to noPhotoDia
        UIController.updateDia(message); //Set message to dialogue box
        while(!UIController.returnGate){ //Until the return gate flips
            yield return null; //Return null
        }
        UIController.returnGate = false; //Reset return gate
        UIController.setMenuState("none"); //Disable the noPhotoDia scene
        playerObj.SendMessage("lockPlayer", false); //Unlock player movement
        isActive = false; //Return to not active
        yield return null; //Default return case
    }

    void disableBarrier(){ //Function to disable the barrier 
        Component[] colliders = GetComponents<PolygonCollider2D>() as Component[]; //Store the barriers colliders
        foreach(Component collider in colliders){ //For each collider
            Destroy(collider as PolygonCollider2D); //Destroy it
        }
            
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    public void Awake(){
        playerObj = GameObject.FindWithTag("Player"); //Grab player reference
    }
}
