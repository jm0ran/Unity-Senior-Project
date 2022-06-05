using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class route1Controller : MonoBehaviour
{

//------------------------------------------------------------------------
//Main Variables Used in Script
    public bool drakeTriggered = false; //Bool for if drake is triggered
    public static bool finishingDiaTriggered = false; //Bool for if finish dia is triggered
    public GameObject drakeObj; //Reference to drakeObj
    
//------------------------------------------------------------------------
//User Defined Functions
    void updateScene(){
        //Empty but exists for structure sake
    }

//------------------------------------------------------------------------
//Unity Defined Funtions
    void Awake(){
        drakeObj = GameObject.Find("drakeNPC"); //Get Drake object
    }

    void Start(){ 
        updateScene(); //Update Scene At Start
        //This section is just to make development a bit easier because of positioning
        //----------------------------------------------------
        if(saveDataController.globalSave.oneTimes[7] && saveDataController.globalSave.oneTimes[8]){ //If game complete
            drakeTriggered = true; //Trigger drake
            drakeObj.SetActive(false); //Disable drake
        }else if (!drakeTriggered){ //If drake is not triggered
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(0f, 18f, 0f); //Set new player position
            GameObject cameraObj = GameObject.FindWithTag("MainCamera"); //Get Camera refernence
            cameraObj.SendMessage("setCamera", "top"); //Move Camera to top of screen
            drakeTriggered = false; //Set drake to not triggered
        }
        //-------------------------------------------------------   
    }

    void Update(){
        if(saveDataController.globalSave.oneTimes[7] && saveDataController.globalSave.oneTimes[8] && !finishingDiaTriggered){ //If player has fully won and finishing dialogue is not done
            finishingDiaTriggered = true; //Finishing Dia is triggered
            StartCoroutine(UIController.DiaCycle(new List<string>(){ //Finishig Dia
                    "Thanks for playing YeBound",
                    "You are now free to explore the map"
                }, GameObject.Find("route1-base"), "", ""));
        }else if (!drakeTriggered){ //If drake has not been triggered yet
            if(!saveDataController.globalSave.oneTimes[7]){ //Secondary verfication
                StartCoroutine(UIController.DiaCycle(new List<string>(){ //Drake dialogue
                    "Where do you think you're going", //1
                    "Another psycho?",
                    "Don't you know who I am... Look me in the eyes",
                    "Drake...",
                    "No... why would you be here",
                    "I have some business with those shoes of yours"

                }, new List<string>(){
                    "unknown", //1
                    "main",
                    "unknown",
                    "main",
                    "main",
                    "drake"
                }, drakeObj, "drakeDialogueFollowUp", ""));
            }
            drakeTriggered = true; //Set drake triggered to true
        }
    }
}
