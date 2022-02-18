using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIController : MonoBehaviour
{
    
    // [Header("Parent Level Game Obects")]
    public static GameObject photoDia;
    public static GameObject noPhotoDia;
    public static GameObject inventory;

    public static GameObject currentLayer = null;
    public static GameObject currentTextBox = null;
    public static GameObject currentProfileBox = null;

    public static bool returnGate = false;
    

   public static GameObject findChild(string target, GameObject parent){
        if(parent != null){
            foreach (Transform child in parent.transform){
                if(child.gameObject.name == target){
                    return child.gameObject;
                }
            }
        }
        return null;
    }

    public static void prepareChildren(){
        currentTextBox = findChild("text", currentLayer);
        currentProfileBox = findChild("profileImage", currentLayer);
    }

    public static void setMenuState(string desiredState){
        switch (desiredState)
        {
            //Here I want to store the current layer in a variable and use it to setup other variables
            case "none":
                currentLayer = null;
                photoDia.SetActive(false);
                noPhotoDia.SetActive(false);
                inventory.SetActive(false);
                break;
            case "photoDia":
                currentLayer = photoDia;
                photoDia.SetActive(true);
                noPhotoDia.SetActive(false);
                inventory.SetActive(false);
                break;
            case "inventory":
                currentLayer = inventory;
                photoDia.SetActive(false);
                noPhotoDia.SetActive(false);
                inventory.SetActive(true);
                break;
            default:
                Debug.Log("Entered an invalid desired state for the UI");
                break;
        }
        prepareChildren();
    }

    public static void updateDia(string inputText, string spriteName){
        if(currentLayer == photoDia){
            currentTextBox.GetComponent<TextMeshProUGUI>().text = inputText;
            currentProfileBox.GetComponent<Image>().sprite = persistSprites.profiles[spriteName]; //Going to call my sprite dictionary
        }else if(currentLayer == noPhotoDia){
            currentTextBox.GetComponent<TextMeshProUGUI>().text = inputText;
        }else if(currentLayer == inventory){
            //Nothing here yet
        }else{
            //Also nothing here yet
        }
    }


    void Awake(){
        photoDia = findChild("photoDia", gameObject);
        noPhotoDia = findChild("noPhotoDia", gameObject);
        inventory = findChild("inventory", gameObject);
        
        setMenuState("none");
    }

    //Going to override for Dialogue without images
    public static IEnumerator DiaCycle(List<string> dia, List<string> diaOrder){
        returnGate = false;
        setMenuState("photoDia");
        updateDia(dia[0], diaOrder[0]);
        yield return new WaitForSeconds(0.3f);
        for(int i = 1; i < dia.Count; i++){
            while(!returnGate){
                yield return null;
            }
            updateDia(dia[i], diaOrder[i]);
            yield return new WaitForSeconds(0.3f);
            returnGate = false;
        }
        while(!returnGate){
            yield return null;
        }
        returnGate = false;
        setMenuState("none");
    }
}
