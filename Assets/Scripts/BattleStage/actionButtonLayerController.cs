using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;

public class actionButtonLayerController : MonoBehaviour
{
    //Layers of the menu
    private GameObject rootLayer;
    private GameObject fightLayer;
    private GameObject switchLayer;
    private string currentChar;
    private GameObject playerVisualInfo;
    private GameObject currentLayer;
    private GameObject currentCharProfile;
    private GameObject[] layers;
    
    

    void Start(){
        rootLayer = GameObject.Find("rootLayer");
        fightLayer = GameObject.Find("fightLayer");
        switchLayer = GameObject.Find("switchLayer");
        currentCharProfile = GameObject.Find("currentCharProfile");
        playerVisualInfo = GameObject.Find("playerVisualInfo");
        fightLayer.SetActive(false);
        switchLayer.SetActive(false);
        layers = new GameObject[] {rootLayer, fightLayer, switchLayer};
        currentChar = saveDataController.globalSave.currentTeam[0];
    }

    void updateCharProfile(string newChar){
        currentCharProfile.SendMessage("switchProfile", currentChar);
    }

    void processButton(string trigger){
        if(trigger == "fight"){
            menuTransition(fightLayer);
            assignFightButtonData();
        }else if(trigger == "switch"){
            menuTransition(switchLayer);
        }else if(trigger == "root"){
            menuTransition(rootLayer);
        }else if(trigger == "char1" || trigger == "char2" || trigger == "char3"){
            menuTransition(rootLayer);
            switch(trigger){
                case "char1":
                    currentChar = saveDataController.globalSave.currentTeam[0];
                    break;
                case "char2":
                    currentChar = saveDataController.globalSave.currentTeam[1];
                    break;
                case "char3":
                    currentChar = saveDataController.globalSave.currentTeam[2];
                    break;
            }
        }
        else if(trigger == "move1" || trigger == "move2" || trigger == "move3" || trigger =="move4"){
            processMove(trigger);
        }
        if(trigger == "switch"){
            playerVisualInfo.SetActive(false);
            assignSwitchButtonData();
        }else{
            playerVisualInfo.SetActive(true);
            updateCharProfile(currentChar);
        }
    }

    void processMove(string Trigger){
        
    }

    void menuTransition(GameObject dest){
        for(int i = 0; i < layers.Length; i++){
            layers[i].SetActive(false);
        }
        dest.SetActive(true);
        currentLayer = dest;
        //This is where I'll go to the next menu
    }


    void assignSwitchButtonData(){
        enableChildren(switchLayer);

        GameObject char1 = GameObject.Find("buttonChar1");
        GameObject char2 = GameObject.Find("buttonChar2");
        GameObject char3 = GameObject.Find("buttonChar3");

        GameObject[] charButtons = new GameObject[] {char1, char2, char3};

        for(int i = 0; i < saveDataController.globalSave.currentTeam.Length; i++){ //Need handling for empty character slots
            if(saveDataController.globalSave.currentTeam[i] == ""){
                charButtons[i].SetActive(false);
             }
        }

        for(int i = 0; i < charButtons.Length; i++){
            GameObject charName = null;
            GameObject charProfile = null;
            foreach(Transform child in charButtons[i].transform){
                if(child.gameObject.name == "charName"){
                    charName = child.gameObject;
                }else if(child.gameObject.name == "charProfile"){
                    charProfile = child.gameObject;
                }
            }
            if(saveDataController.globalSave.currentTeam[i] != ""){
                if(charProfile != null){
                    charProfile.GetComponent<Image>().sprite = charProfileImageController.spriteDictionary[saveDataController.globalSave.currentTeam[i]];
                }
                if(charName != null){
                    charName.GetComponent<TextMeshProUGUI>().text = saveDataController.globalSave.currentTeam[i];
                }
            }
        }   
    }

    void assignFightButtonData(){
        enableChildren(fightLayer);

        GameObject move1 = GameObject.Find("buttonMove1");
        GameObject move2 = GameObject.Find("buttonMove2");
        GameObject move3 = GameObject.Find("buttonMove3");
        GameObject move4 = GameObject.Find("buttonMove4");

        GameObject[] moveButtons = new GameObject[] {move1, move2, move3, move4};
        Move[] moves = null;

        //For the current character I have to return the move list and then assign it to the buttons
        
        for(int i = 0; i < saveDataController.globalSave.acquiredCharacters.Count; i++)
        {
            if(saveDataController.globalSave.acquiredCharacters[i].name == currentChar){
                moves = saveDataController.globalSave.acquiredCharacters[i].moves;
            }
        };

        if(moves != null){
            for(int j = 0; j < moveButtons.Length; j++){ //Remember to pass if button is null
                if(moves[j].name != ""){
                    foreach(Transform child in moveButtons[j].transform){
                        if(child.gameObject.name == "moveName"){
                            child.gameObject.GetComponent<TextMeshProUGUI>().text = moves[j].name;
                        };
                    }
                }else{
                    moveButtons[j].SetActive(false); //This will work but I need to remember to loop through the children of fight layer before it starts to make sure everything is enabled
                }
            }
            //This is where I need to assign each move to each button
        }


    }

    void enableChildren(GameObject target){
        foreach(Transform child in target.transform){
            child.gameObject.SetActive(true);
        }
    }

    void Update(){
        if(currentLayer != null & Input.GetKeyDown(KeyCode.Escape)){ //Takes us back to the root layer
            processButton("root");
        }
    }
}
