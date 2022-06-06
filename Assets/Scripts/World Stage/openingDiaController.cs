using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingDiaController : MonoBehaviour
{

//The opening dialogue controller manages opening dialogue on scenes, in retrospect this should have been moved to the individual scene controllers

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    [Header("Dialogue")]
    public List<string> dia; //Dialogue storage
    public List<string> diaOrder; //Dialogue storage order
    [Header("Persistance")]
    public bool oneTime = true; //OneTime default is true for opening dialogue
    public int persistID = -1; //Set to -1 by default to call errors instead of false flags
    [Header("Parameters")]
    public bool rewards = false; //Default rewards false
    public bool fight = false; //Default fight false
    private bool hasChecked = false; //Default has been checked false
    public string diaType = "photoDia"; //Photo dia type
    public string followUp = ""; //No follow up
    public string followUpArg; //No follow up arg
    
//------------------------------------------------------------------------
//Unity Defined Functions
    void FixedUpdate(){ 
        if(!hasChecked){ //If has not checked
            hasChecked = true; //Set true so it only runs one time
            if(oneTime){ //if one time event
                if(!saveDataController.globalSave.oneTimes[persistID]){ //If persist ID is false
                    saveDataController.globalSave.oneTimes[persistID] = true; //Update persist id
                    saveDataController.globalSave.serializeSaveData(); //Serialize save data changes
                    if(diaType == "photoDia"){ //If photo dia
                        StartCoroutine(UIController.DiaCycle(dia,diaOrder, gameObject, followUp, followUpArg)); //Trigger photoDia with args
                    }else if(diaType == "noPhotoDia"){ //If no photo dia
                        StartCoroutine(UIController.DiaCycle(dia, gameObject, followUp, followUpArg)); //Trigger noPhotoDia with args
                    }
                }  
            }else{ //If not one time
                //Trigger the thing according to type again
                if(diaType == "photoDia"){
                    StartCoroutine(UIController.DiaCycle(dia,diaOrder, gameObject, followUp, followUpArg));
                }else if(diaType == "noPhotoDia"){
                    StartCoroutine(UIController.DiaCycle(dia, gameObject, followUp, followUpArg));
                }
            }
        }
    }

}
