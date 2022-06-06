using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class betterNPC : MonoBehaviour
{

//The better NPC controller got its name because its a better version of the NPC Controller, it controls trigger dialogue and player interaction

//------------------------------------------------------------------------
//Main Variables Used in Scripts

    [Header("Dialogue")]
    public List<string> dia; //NPC Dialogue
    public List<string> diaOrder; //Dialogue Order
    [Header("Persistance")]
    public bool oneTime = false; //One time determines if this is a one time interaction
    public int persistID = -1; //Persist id that npc corresponds to, Set to -1 by default to call errors instead of false flags
    [Header("Parameters")]
    public bool rewards = false; //Does NPC give rewards
    public bool fight = false; //Does NPC trigger fight
    public string diaType = "photoDia"; //Does npc use photoDia or noPhotoDia
    public string followUp = ""; //NPC follow up function
    public string followUpArgument = ""; //NPC follow up argument 
    private bool hasStartRan = false; //Just needed to order some things for development
    
//------------------------------------------------------------------------
//User Defined Functions
    void startDia(){ //Function to start Dialogue
        if(oneTime){ //If dialogue is one time
            if(persistID == -1){ //If persist ID is default
                Debug.Log("PersistID has not been set"); //Alert of error
                return; //Escape
            }else{ //If persist ID is set 
                if(!saveDataController.globalSave.oneTimes[persistID]){ //If false
                    saveDataController.globalSave.oneTimes[persistID] = true; //Trigger by setting to true
                    saveDataController.globalSave.serializeSaveData(); //Serialize changes
                }else{ //If true
                    return; //Escape
                }
            }
        }
        if(diaType == "photoDia"){ //If of photoDia type
            StartCoroutine(UIController.DiaCycle(dia,diaOrder, gameObject, followUp, followUpArgument)); //Start Dialogue cycle with the photoDia method
        }else if(diaType == "noPhotoDia"){ //if of noPhotoDia type
            StartCoroutine(UIController.DiaCycle(dia,gameObject, followUp, followUpArgument)); //Start Dialogue cycle with noPhotoDia method
        }
    }

    void Update(){
        if(!hasStartRan){ //If hasStarted
            hasStartRan = true; //Has Started
            if((oneTime) && (persistID != -1) && (saveDataController.globalSave.oneTimes[persistID]) && (gameObject.GetComponent<npcFollowUp>().altSprite != null)){ //If one time has been triggered
                gameObject.SendMessage("swapSprite"); //Swap sprite on game obejct
            }
        }
        

        

        
    }


}
