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
                "main",
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
        if(saveDataController.globalSave.inventory.findObj("Yeezy") != -1 && !saveDataController.globalSave.oneTimes[6]){
            Debug.Log("He got da yeezys");
            saveDataController.globalSave.oneTimes[6] = true;
            StartCoroutine(UIController.DiaCycle(new List<string>(){
                "Something is happening to the Yeezy?!",
                "Yeezus just rose again...",
                "What-",
            }, new List<string>(){
                "main",
                "unknown", //This needs to be a mystery icon I don't have it yet though
                "main",
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

        swapSprite();
        Vector3 targetPosition = new Vector3(0f, -0.5f, 0f);
        GameObject.FindWithTag("Player").GetComponent<Transform>().position = targetPosition;
        Vector3 currentCamPosition = GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position;
        GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(targetPosition.x, targetPosition.y, currentCamPosition.z);
        //Need to update camera position on this later as well

        GameObject.Find("kanyeNPC").GetComponent<SpriteRenderer>().enabled = true;

        yield return new WaitForSeconds(0.2f);


        StartCoroutine(UIController.DiaCycle(new List<string>(){
                "Are you...", //1
                "Kanye West", //2
                "How is that even possible?!", //3
                "Ten years ago <i>they<i> sealed me...", //4
                "Even now I am only a projection, I must combine with my 3 other parts", //5
                "I can maintain this form temporarily, but will need an object to assume", //6
                "Did somebody drug me, am I hallucinating", //7
                "You woke me up with my old pair of Yeezys, those will work fine", //8
                "Wait wait wait. Is this real??", //9
                "Yes",
                "And why should I believe you",
                "Because I'm Kanye freaking West",
                "Now I'm going to posses those Yeezys, we need to get to the closest city as soon as possible"




            }, new List<string>(){
                "main", //1
                "kanye", //2
                "main", //3
                "kanye", //4
                "kanye", //5
                "kanye", //6
                "main", //7
                "kanye", //8
                "main", //9
                "kanye",
                "main",
                "kanye",
                "kanye"



            }, gameObject, "kanyePostFirstDia", ""));

        alpha = 1.0f;
        canvasGroup.alpha = alpha;
        while (alpha > 0f){
            alpha -= 0.05f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.025f);
        }
        
    }

    void kanyePostFirstDia(){ //Needs to return IE Numerator so have to move this stuff out into another function
        StartCoroutine(coKanyePostFirstDia());
    }

    IEnumerator coKanyePostFirstDia(){
        GameObject fadeShade = UIController.fadeShade;
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>();
        float alpha = 0.0f;
        
        alpha = 0.0f;
        while (alpha < 1f){
            alpha += 0.05f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.025f);
        }

        yield return new WaitForSeconds(0.2f);
        UIController.setMenuState("none");
        GameObject.Find("kanyeNPC").GetComponent<SpriteRenderer>().enabled = false;

        alpha = 1.0f;
        canvasGroup.alpha = alpha;
        while (alpha > 0f){
            alpha -= 0.05f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.025f);
        }

        saveDataController.globalSave.oneTimes[6] = true;
        saveDataController.globalSave.serializeSaveData();

        StartCoroutine(UIController.DiaCycle(new List<string>(){
                "<i>I don't think I'm crazy...<i>", //1
                "<i>What could be the worst thing that could happen? I'll listen to him for now<i>",
                "<i>I'll leave the junkyard and head towards the city<i>"

            }, new List<string>(){
                "main", //1
                "main", //2
                "main"

            }, gameObject, "", ""));
        
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
