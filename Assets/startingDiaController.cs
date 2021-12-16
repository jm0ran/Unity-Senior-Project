using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class startingDiaController : MonoBehaviour
{
    //Need to migrate my starting dia stuff into this instead of where it is spread out rn
    public bool triggered = false;
    public int persistID = -1;

    public List<string> dia;
    public List<string> diaOrder;
    public GameObject UI;
    public GameObject player;

    void Start(){
        UI = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");

        if(!persistController.gameProg[persistID] || true){ //Want this to always run to test functionality
            List<string> processed = new List<string>();
            if(dia.Count == diaOrder.Count){
                for(int i = 0; i < dia.Count; i++){
                    processed.Add(dia[i] + ";" + diaOrder[i]);
                }
                UI.SendMessage("startDiaLoop", processed);
            }else{
                Debug.Log("You prob forgot to fill out Dia order");
            }
            
        }else if(persistController.gameProg[persistID]){
            triggered = true;
        }
    }

}
