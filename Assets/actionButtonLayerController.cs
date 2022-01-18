using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class actionButtonLayerController : MonoBehaviour
{
    //Layers of the menu
    private GameObject rootLayer;
    public GameObject fightLayer;

    private GameObject[] layers;

    void Start(){
        rootLayer = GameObject.Find("rootLayer");
        fightLayer = GameObject.Find("fightLayer");
        fightLayer.SetActive(false);
        layers = new GameObject[] {rootLayer, fightLayer};
    }

    void processButton(string trigger){
        if(trigger == "fight"){
            menuTransition(fightLayer);
        }
    }

    void menuTransition(GameObject dest){
        for(int i = 0; i < layers.Length; i++){
            Debug.Log(layers[i].name);
            layers[i].SetActive(false);
        }
        dest.SetActive(true);
        
        //This is where I'll go to the next menu
    }
}
