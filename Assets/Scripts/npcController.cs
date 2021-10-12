using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class npcController : MonoBehaviour
{
    public List<string> dia; //Creates a list for the Dialogue
    public GameObject UI;
    public GameObject player;
    public Text textBox;
    public int totalLines;
    public int textProgress;
    
    void Awake(){
        UI = GameObject.FindWithTag("UI");
        textBox = GameObject.FindWithTag("textBox").GetComponent<Text>();
        player = GameObject.FindWithTag("Player");
    }

    void startDia(GameObject player){
        player.SendMessage("lockPlayer", true);
        StartCoroutine(Testing());
        totalLines = dia.Count;
        textProgress = 0;
        textBox.text = dia[textProgress];
        UI.SetActive(true);
    }

    IEnumerator Testing(){
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
}
