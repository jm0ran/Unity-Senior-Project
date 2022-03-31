using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterNPC : MonoBehaviour
{
    [Header("Dialogue")]
    public List<string> dia;
    public List<string> diaOrder;
    [Header("Persistance")]
    public bool oneTime = false;
    public int persistID = -1; //Set to -1 by default to call errors instead of false flags
    [Header("Parameters")]
    public bool rewards = false;
    public bool fight = false;
    public string diaType = "photoDia";
    public string followUp = "";
    public string followUpArgument = "";

    private bool hasStartRan = false; //Just needed to order some things for development
    

    void startDia(){

        if(oneTime){
            if(persistID == -1){
                Debug.Log("PersistID has not been set");
                return;
            }else{
                if(!saveDataController.globalSave.oneTimes[persistID]){
                    saveDataController.globalSave.oneTimes[persistID] = true;
                    saveDataController.globalSave.serializeSaveData();
                }else{
                    Debug.Log("Has already been triggered");
                    return;  
                }
            }
        }

        if(diaType == "photoDia"){
            StartCoroutine(UIController.DiaCycle(dia,diaOrder, gameObject, followUp, followUpArgument));
        }else if(diaType == "noPhotoDia"){
            StartCoroutine(UIController.DiaCycle(dia,gameObject, followUp, followUpArgument));
        }
    }

    void Update(){
        if(!hasStartRan){
            hasStartRan = true;
            if((oneTime) && (persistID != -1) && (saveDataController.globalSave.oneTimes[persistID]) && (gameObject.GetComponent<npcFollowUp>().altSprite != null)){ //If one time has been triggered
                gameObject.SendMessage("swapSprite");
            }
        }
        

        

        
    }


}
