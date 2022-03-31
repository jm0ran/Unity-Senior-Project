using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class junkyardController : MonoBehaviour
{
    //The main role of this controller is going to be to manage the state of specifically the junkyard, there will be others in this style for each map just to handle spawning of entities based on one time events and the sorts
    //Should be reached right now by a send message to the object with the tag "Map"

    //Objects
    private GameObject ned;


    void Start(){ //This will determine who to spawn and all of that
        ned = GameObject.Find("ned");
       //Work from the top down likely although not super efficent probably wont get toooooo confusing but you get it don't you future Joseph, you're smarter than past Joseph so you must
        updateScene();
    }
        
    void updateScene(){
        //The Ned Check Section
        if(saveDataController.globalSave.oneTimes[5]){
            GameObject ned = GameObject.Find("ned");
            if(ned.GetComponent<SpriteRenderer>().enabled){ 
                ned.GetComponent<SpriteRenderer>().enabled = false;
                Debug.Log("Disabled Ned's Sprite Renderer");
                //Gets rid of the stuff
                Component[] collidersToDestroy = ned.GetComponents<CircleCollider2D>() as Component[];
                //Destroys both the collision collider and interaction collider
                foreach(Component indCol in collidersToDestroy){
                    Destroy(indCol as CircleCollider2D);
                }
            }
        }
        else if(saveDataController.globalSave.oneTimes[3]){ //If initial speak with ned
            betterNPC npcComponent = ned.GetComponent<betterNPC>();
            npcComponent.followUp = "checkForYeezys";
            npcComponent.persistID = -1;
            npcComponent.oneTime = false;
            npcComponent.followUpArgument = null;  
            npcComponent.dia = new List<string>(){
                "Have you found the Yeezy?"
            };
            npcComponent.diaOrder = new List<string>(){
                "ned"
            };
        }


        //CHESTS
        
    } 


}

