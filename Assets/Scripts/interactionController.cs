using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class interactionController : MonoBehaviour
{
    public float interactionRange; //Want to implement later
    public bool talks;
    public bool item;
    public List<string> dia; //Creates a list for the Dialogue
    public GameObject UI;
    public GameObject player;
    public Text textBox;
    public int totLines;
    public bool inProgress = false;
    public int textProgress;
    public int nextLine;
    
    void Awake(){
        UI = GameObject.FindWithTag("UI");
        textBox = GameObject.FindWithTag("textBox").GetComponent<Text>();
        player = GameObject.FindWithTag("Player");
        inProgress = false;
    }

    void Start(){
        StartCoroutine(Testing());
    }


    void renderSpeech(){
        totLines = dia.Count;
        if (totLines > 1){
            nextLine = 1;
        }
        UI.SetActive(true);
        inProgress = true;
        textProgress = 0;
        //This will be what creates and renders the dialougue, want to seperate the speech function

        //Steps:

        //Enter a locked state where player cannot make any actions
        //Intend to take a list with strings of text split up for dialouge differences and cycle through waiting for the next keyprompt to continue
        //After cycling through speech, check if the interactable object has an item and if so give it to the player
        //Reenable player movement



    }


    void printHi(GameObject player){
        player.SendMessage("lockPlayer", true);
        renderSpeech();
    }

    // Update is called once per frame
    void Update()
    {
        if(inProgress && textProgress < nextLine){
            textBox.text = dia[textProgress];
        }
        if(inProgress && Input.GetKeyDown(KeyCode.R)){
            textProgress++;
            if(nextLine < totLines){
                nextLine++;
            }
            else{
                UI.SetActive(false);
                player.SendMessage("lockPlayer", false);
            }
        }
    }
    //Want to use an IENumerator to ignore input for 3 seconds after the press of a key
    IEnumerator Testing(){
        while(!Input.GetKeyDown(KeyCode.T)){
            yield return null;
        }
        if(Input.GetKeyDown(KeyCode.T)){
            Debug.Log("Triggered");
            yield return new WaitForSeconds(5f);
        }
        StartCoroutine(Testing());

    }
}
