using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcController : MonoBehaviour
{
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public List<string> dia; //Creates a list for the Dialogue
    public List<string> diaOrder; //Will maybe be used for potraits of characters during Dialougue, still not exactly sure how I want to do this yet
    public GameObject UI;
    public GameObject player;
    public int totalLines;
    public int textProgress;
    [Header("Persistance")]
    public string diaType; //Dia type defines type of Dialogue window to render, options are image and noImage, going to be implemented later
    public bool oneTime = false; //Determines whether or not this is a one time event
    public int persistID = -1; //Set to -1 by default to call errors instead of false flags

//------------------------------------------------------------------------
//Main User Defined Functions
    void startDia(){ //Called by playerController in order to start the Dialougue System
        bool willContinue = true;
        if(oneTime){
            if(!persistController.gameProg[persistID]){
                willContinue = true;
                persistController.gameProg[persistID] = true;
            }else{
                willContinue = false;
            }
        }
        if(willContinue){
            player.SendMessage("lockPlayer", true);
            totalLines = dia.Count;
            textProgress = 0;
            UI.SendMessage("enableUIItem", "photoDia");
            UI.SendMessage("changeText", dia[textProgress] + ";" + diaOrder[textProgress]); //Sets the text for the dialogue
            StartCoroutine(DiaLoop());
        }
    }

    IEnumerator DiaLoop(){ //Coroutine called and loops through for Dialougue
        //This while loop will continue to test for KeyDown T (Temportary key used to progress the text)
        while(!Input.GetKeyDown(KeyCode.T)){
            yield return null;
        }
        //After the key is pressed we break out of the loop
        if(textProgress < (totalLines - 1)){
            textProgress++; //Instantiate before we render the text
            UI.SendMessage("changeText", dia[textProgress] + ";" + diaOrder[textProgress]);
            yield return new WaitForSeconds(0.1f);; //Waits the set amount of time before continuing the coroutine
            StartCoroutine(DiaLoop());
        }else{
            UI.SendMessage("disableUIItems");
            player.SendMessage("lockPlayer", false);
            yield return null;
        }
    }

//------------------------------------------------------------------------
//Unity Functions
    void Awake(){
        UI = GameObject.FindWithTag("UI");
        player = GameObject.FindWithTag("Player");
    }
}