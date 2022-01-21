using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class actionButtonLayerController : MonoBehaviour
{
    //Layers of the menu
    private GameObject rootLayer;
    private GameObject fightLayer;
    private GameObject switchLayer;
    private string currentChar;
    private GameObject currentLayer;
    private GameObject currentCharProfile;
    private GameObject[] layers;
    
    public Sprite altSprite;
    

    void Start(){
        rootLayer = GameObject.Find("rootLayer");
        fightLayer = GameObject.Find("fightLayer");
        switchLayer = GameObject.Find("switchLayer");
        currentCharProfile = GameObject.Find("currentCharProfile");
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
        }
    }

    void menuTransition(GameObject dest){
        for(int i = 0; i < layers.Length; i++){
            Debug.Log(layers[i].name);
            layers[i].SetActive(false);
        }
        dest.SetActive(true);
        currentLayer = dest;
        //This is where I'll go to the next menu
    }

    void Update(){
        if(currentLayer != null & Input.GetKeyDown(KeyCode.Escape)){ //Takes us back to the root layer
            menuTransition(rootLayer);
        }
    }
}
