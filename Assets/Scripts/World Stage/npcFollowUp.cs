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
                "Good luck kid, I sense great adventure in your future",
                "What?-",
                "Present it to HIM and your path will open...",

            }, new List<string>(){
                "ned",
                "ned",
                "player",
                "ned"
            }, gameObject, "fadeRemove", ""));
            saveDataController.globalSave.oneTimes[5] = true;
            saveDataController.globalSave.serializeSaveData();
        }else{
            UIController.setMenuState("none");
            Debug.Log("You don't have the Yeezy");
        }
        
    }

    void canInteractWithKanyeStatue(){
        if(saveDataController.globalSave.inventory.findObj("Yeezy") != -1){
            Debug.Log("He got da yeezys");
            StartCoroutine(UIController.DiaCycle(new List<string>(){
                "Something is happening to the Yeezy?!",
                "Yeezus just rose again...",
                "What-"
            }, new List<string>(){
                "main",
                "unknown", //This needs to be a mystery icon I don't have it yet though
                "main"
            }, gameObject, "kanyeArrives", ""));
        }else{
            Debug.Log("He aint got the yeezys and cant interact with the statue");
            UIController.setMenuState("none");
        }
         
    }

    void kanyeArrives(){
        StartCoroutine(kanyeArrivesCo());
    }

    IEnumerator kanyeArrivesCo(){
        GameObject fadeShade = UIController.fadeShade;
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>();
        float alpha = 0.0f;
        while (alpha < 1f){
            alpha += 0.05f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.2f);

        alpha = 1.0f;
        canvasGroup.alpha = alpha;
        while (alpha > 0f){
            alpha -= 0.05f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.025f);
        }


        Debug.Log("Flashing lights");
        UIController.setMenuState("none");
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
