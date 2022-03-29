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
    

    void startDia(){
        if(diaType == "photoDia"){
            StartCoroutine(UIController.DiaCycle(dia,diaOrder, gameObject, followUp, followUpArgument));
        }else if(diaType == "noPhotoDia"){
            StartCoroutine(UIController.DiaCycle(dia,gameObject, followUp, followUpArgument));
        }
    }


}
