using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class route1Controller : MonoBehaviour
{
    public bool drakeTriggered = false;
    public static bool finishingDiaTriggered = false;
    public GameObject drakeObj;
    
    void Awake(){
        drakeObj = GameObject.Find("drakeNPC");
    }

    void Start(){ 
        updateScene();
        //This section is just to make development a bit easier
        //----------------------------------------------------
        if(saveDataController.globalSave.oneTimes[7] && saveDataController.globalSave.oneTimes[8]){
            drakeTriggered = true;
            drakeObj.SetActive(false);
        }else if (!drakeTriggered){
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(0f, 18f, 0f);
            GameObject cameraObj = GameObject.FindWithTag("MainCamera");
            cameraObj.SendMessage("setCamera", "top");
            drakeTriggered = false;
        }
        //-------------------------------------------------------
        
    }
        
    void updateScene(){
        
    }


    void Update(){
        if(saveDataController.globalSave.oneTimes[7] && saveDataController.globalSave.oneTimes[8] && !finishingDiaTriggered){
            finishingDiaTriggered = true;
            StartCoroutine(UIController.DiaCycle(new List<string>(){
                    "Thanks for playing YeBound",
                    "You are now free to explore the map"
                }, GameObject.Find("route1-base"), "", ""));
        }else if (!drakeTriggered){
            if(!saveDataController.globalSave.oneTimes[7]){
                StartCoroutine(UIController.DiaCycle(new List<string>(){
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
            drakeTriggered = true;
        }
    }
}
