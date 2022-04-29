using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class route1Controller : MonoBehaviour
{
    public bool drakeTriggered = false;
    
    void Start(){ 
        updateScene();
        //This section is just to make development a bit easier
        //----------------------------------------------------
        if(!saveDataController.globalSave.oneTimes[7]){
            GameObject.FindWithTag("Player").GetComponent<Transform>().position = new Vector3(0f, 18f, 0f);
            GameObject cameraObj = GameObject.FindWithTag("MainCamera");
            cameraObj.SendMessage("setCamera", "top");
        }
        //-------------------------------------------------------
        
    }
        
    void updateScene(){
        Debug.Log("Updated scene on courtyard");
    }

    void Update(){
        if(!drakeTriggered){
            if(!saveDataController.globalSave.oneTimes[7]){
                StartCoroutine(UIController.DiaCycle(new List<string>(){
                    "Where do you think you're going", //1
                    "Another psycho?",
                    "Don't you know who I am... Look me in the eyes",
                    "No... why would you be here",
                    "I have some business with those shoes of yours"

                }, new List<string>(){
                    "unknown", //1
                    "main",
                    "unknown",
                    "main",
                    "drake"
                    
                

                }, gameObject, "", ""));
                Debug.Log("Started dialogue maybe");
            }

            drakeTriggered = true;
        }
    }
}
