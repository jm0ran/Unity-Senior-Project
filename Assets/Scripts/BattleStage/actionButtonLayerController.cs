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
    
    public Sprite altSprite;
    

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
        updateCharProfile(currentChar);
    }

    void updateCharProfile(string newChar){
        currentCharProfile.SendMessage("switchProfile", currentChar);
    }

    void processButton(string trigger){
        if(trigger == "fight"){
            menuTransition(fightLayer);
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

        
        if(trigger == "switch"){
            playerVisualInfo.SetActive(false);
            assignSwitchButtonData();
        }else{
            playerVisualInfo.SetActive(true);
            updateCharProfile(currentChar);
        }
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
        GameObject char1 = GameObject.Find("buttonChar1");
        GameObject char2 = GameObject.Find("buttonChar2");
        GameObject char3 = GameObject.Find("buttonChar3");

        GameObject[] charButtons = new GameObject[] {char1, char2, char3};

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
            if(charProfile != null){
                charProfile.GetComponent<Image>().sprite = charProfileImageController.spriteDictionary[saveDataController.globalSave.currentTeam[i]];
            }
            if(charName != null){
                charName.GetComponent<TextMeshProUGUI>().text = saveDataController.globalSave.currentTeam[i];
            }
        }
        
    }

    void Update(){
        if(currentLayer != null & Input.GetKeyDown(KeyCode.Escape)){ //Takes us back to the root layer
            processButton("root");
        }
    }
}
