using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class openingDiaController : MonoBehaviour
{
    [Header("Dialogue")]
    public List<string> dia;
    public List<string> diaOrder;
    [Header("Persistance")]
    public bool oneTime = true;
    public int persistID = -1; //Set to -1 by default to call errors instead of false flags
    [Header("Parameters")]
    public bool rewards = false;
    public bool fight = false;

    private bool hasChecked = false;
    public string diaType = "photoDia";
    public string followUp = "";
    

    // void startDia(){
    //     StartCoroutine(UIController.DiaCycle(dia,diaOrder));
    // }

    void FixedUpdate(){
        if(!hasChecked){
            hasChecked = true;
            if(oneTime){
                if(!tempOneTimes.oneTimes[persistID]){
                    tempOneTimes.oneTimes[persistID] = true;
                    if(diaType == "photoDia"){
                        StartCoroutine(UIController.DiaCycle(dia,diaOrder, gameObject, followUp));
                    }else if(diaType == "noPhotoDia"){
                        StartCoroutine(UIController.DiaCycle(dia, gameObject, followUp));
                    }
                }  
                
            }else{
                if(diaType == "photoDia"){
                    StartCoroutine(UIController.DiaCycle(dia,diaOrder, gameObject, followUp));
                }else if(diaType == "noPhotoDia"){
                    StartCoroutine(UIController.DiaCycle(dia, gameObject, followUp));
                }
            }
        }
    }

}
