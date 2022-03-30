using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcFollowUp : MonoBehaviour
{
    public Sprite altSprite; //Extra arguments for follow up


    void fadeRemove(string followUpArg){
        StartCoroutine(fadeRemoveCo());
        
    }

    void chest(string followUpArg){ //This is for chests, needs to give player an item and stuff yk
        saveDataController.globalSave.serializeSaveData();
        StartCoroutine(chestCo(followUpArg));
    }

    void swapSprite(){
        Debug.Log("Swapped image on " + gameObject.name);
        if(altSprite != null){
            gameObject.GetComponent<SpriteRenderer>().sprite = altSprite;
        }else{
            Debug.Log("AltSprite is null");
        }
    }

    void checkForYeezys(){ //This is based and redpilled
        if((saveDataController.globalSave.inventory.findObj("Yeezy") != -1) && !saveDataController.globalSave.oneTimes[5]){
            StartCoroutine(UIController.DiaCycle(new List<string>(){
                "Yeah thats the one, it looks like its seen better days though",
                "Good luck kid, I sense great adventure in your future"
            }, new List<string>(){
                "ned",
                "ned"
            }, gameObject, "fadeRemove", ""));
            saveDataController.globalSave.oneTimes[5] = true;
            saveDataController.globalSave.serializeSaveData();
        }else{
            UIController.setMenuState("none");
            Debug.Log("You don't have the Yeezy");
        }
        
    }

    IEnumerator fadeRemoveCo(){ //Used to fade screen in and out while also disabling the NPC that this is triggered on
        StartCoroutine(UIController.fadeOut(null));
        yield return new WaitForSeconds(0.8f);
        UIController.setMenuState("none");
        StartCoroutine(UIController.fadeIn());
        yield return null;
    }

    IEnumerator chestCo(string followUpArg){
        UIController.returnGate = false; //Used for return gate to prevent automatic progression
        UIController.setMenuState("noPhotoDia"); //Enables the noPhotoUI layer
        UIController.updateDia(followUpArg); //Changes the text to the necessary description
        
        swapSprite();

        while(!UIController.returnGate){
            yield return null;
        }
        
        if(itemController.itemDictionary.ContainsKey(followUpArg) && itemController.infoDictionary.ContainsKey(followUpArg)){
            saveDataController.globalSave.inventory.addObj(followUpArg, 1);
        }else{
            Debug.Log("Item attempting to be added does not exist in itemDictionary and/or infoDictonary");
        }
        

        UIController.setMenuState("none");
        yield return null;
    }
}
