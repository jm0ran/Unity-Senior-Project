using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class junkyardController : MonoBehaviour
{

//The main role of this controller is going to be to manage the state of specifically the junkyard, there will be others in this style for each map just to handle spawning of entities based on one time events and the sorts

//------------------------------------------------------------------------
//Main Variables Used in Script
    private GameObject ned; //Reference for ned GameObject

//------------------------------------------------------------------------
//Unity Defined Functions
    void Start(){ //This will determine who to spawn and all of that
        updateScene(); //Update Scene
    }
        
    void updateScene(){
        if(saveDataController.globalSave.oneTimes[5]){ //If secondary ned dialogue has occurred
            GameObject ned = GameObject.Find("ned"); //Find ned
            if(ned.GetComponent<SpriteRenderer>().enabled){ //If he is enabled
                ned.GetComponent<SpriteRenderer>().enabled = false; //disable him
                Component[] collidersToDestroy = ned.GetComponents<CircleCollider2D>() as Component[]; //Find his colliders
                //Destroys both the collision collider and interaction collider
                foreach(Component indCol in collidersToDestroy){ //For his colliders
                    Destroy(indCol as CircleCollider2D); //Destroy them
                }
            }
        }else if(saveDataController.globalSave.oneTimes[3]){ //If initial speak with ned
            betterNPC npcComponent = GameObject.Find("ned").GetComponent<betterNPC>(); //Get access to ned's betterNPC controller
            npcComponent.followUp = "checkForYeezys"; //Change neds followup to Yeezy check
            npcComponent.persistID = -1; //Change his peristsID to -1 so his initial dialogue doesnt happen again
            npcComponent.oneTime = false; //Disable one time nature
            npcComponent.followUpArgument = null;  //Disable his follow up arguments
            npcComponent.dia = new List<string>(){ //New Dialogue
                "Have you found the Yeezy?"
            };
            npcComponent.diaOrder = new List<string>(){ //New Dialogue order
                "ned"
            };
        }
        if(saveDataController.globalSave.oneTimes[6]){ //If Kanye has been summoned we need to unblock the pathway to Route 1
            GameObject.Find("route1DoorBlocker").SendMessage("disableBarrier"); //Disable Route 1 Barrier
        }

    } 


}

