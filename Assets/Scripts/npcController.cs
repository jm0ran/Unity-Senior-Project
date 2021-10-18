using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcController : MonoBehaviour
{
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public List<string> dia; //Creates a list for the Dialogue
    public GameObject UI;
    public GameObject player;
    public Text textBox;
    public int totalLines;
    public int textProgress;
    public string[] diaOrder; //Will maybe be used for potraits of characters during Dialougue, still not exactly sure how I want to do this yet

//------------------------------------------------------------------------
//Main User Defined Functions
    void startDia(GameObject player){ //Called by playerController in order to start the Dialougue System
        player.SendMessage("lockPlayer", true);
        totalLines = dia.Count;
        textProgress = 0;
        textBox.text = dia[textProgress];
        UI.SetActive(true);
         StartCoroutine(Testing());
    }

    IEnumerator Testing(){ //Coroutine called and loops through for Dialougue
        //This while loop will continue to test for KeyDown T (Temportary key used to progress the text)
        while(!Input.GetKeyDown(KeyCode.T)){
            yield return null;
        }
        //After the key is pressed we break out of the loop
        if(textProgress < (totalLines - 1)){
            textProgress++; //Instantiate before we render the text
            textBox.text = dia[textProgress];
            yield return new WaitForSeconds(0.1f);; //Waits the set amount of time before continuing the coroutine
            StartCoroutine(Testing());
        }else{
            UI.SetActive(false);
            player.SendMessage("lockPlayer", false);
            yield return null;
        }
    }

//------------------------------------------------------------------------
//Unity Functions
    void Awake(){
        UI = GameObject.FindWithTag("UI");
        textBox = GameObject.FindWithTag("textBox").GetComponent<Text>();
        player = GameObject.FindWithTag("Player");
    }
}
