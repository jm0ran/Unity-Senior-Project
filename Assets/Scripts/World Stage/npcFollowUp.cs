using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class npcFollowUp : MonoBehaviour
{

//The NPC FollowUp Controller houses extra functions for NPCs that are usually one time use, they act as callbacks and are triggered after dialogue

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    public Sprite altSprite; //Extra arguments for follow up for sprite change

//------------------------------------------------------------------------
//User Defined Functions
    void fadeRemove(string followUpArg){ //Trigger for fade remove
        StartCoroutine(fadeRemoveCo()); //Starts coresponding coroutine
    }

    IEnumerator fadeRemoveCo(){ //Used to fade screen in and out while also disabling the NPC that this is triggered on
        StartCoroutine(UIController.fadeOut(null)); //Trigger fade out coroutine
        yield return new WaitForSeconds(0.8f); //Time out and continue
        UIController.setMenuState("none"); //Set menu state to none
        StartCoroutine(UIController.fadeIn()); //Trigger a fade in
        yield return null; //Default return case
    }

    void chest(string followUpArg){ //This is for chests, needs to give player an item and stuff yk
        saveDataController.globalSave.serializeSaveData(); //Serialize save changes
        StartCoroutine(chestCo(followUpArg)); //Start corresponding coroutine
    }

    IEnumerator chestCo(string followUpArg){ //Chest coroutine
        UIController.returnGate = false; //Used for return gate to prevent automatic progression
        UIController.setMenuState("noPhotoDia"); //Enables the noPhotoUI layer
        UIController.updateDia("You found a " + followUpArg); //Changes the text to the necessary description
        gameObject.GetComponent<AudioSource>().Play(); //Play chest sound effect
        swapSprite(); //Swap to open chest sprite
        //Pauses code at return gate
        while(!UIController.returnGate){
            yield return null;
        }
        if(itemController.itemDictionary.ContainsKey(followUpArg) && itemController.infoDictionary.ContainsKey(followUpArg)){ //If the item exists
            saveDataController.globalSave.inventory.addObj(followUpArg, 1); //Add item to player inventory
        }else{ //If item doesnt exist
            Debug.Log("Item attempting to be added does not exist in itemDictionary and/or infoDictonary"); //Alert for error
        }
        UIController.setMenuState("none"); //Reset menu state to none
        yield return null; //Default return case
    }

    void swapSprite(){ //Used to swap sprite, mostly on chests
        if(altSprite != null){ //If an altSprite is set
            gameObject.GetComponent<SpriteRenderer>().sprite = altSprite; //Swap with altSprite
        }else{ //If altSprite is not set
            Debug.Log("AltSprite is null"); //Alert of failure to swap sprite
        }
    }

    void checkForYeezys(){ //This checks for Yeezys in the player's inventory and continues if they are there
        if((saveDataController.globalSave.inventory.findObj("Yeezy") != -1) && !saveDataController.globalSave.oneTimes[5]){ //If player has yeezy and is at the appropriate save point
            StartCoroutine(UIController.DiaCycle(new List<string>(){ //Start new dialogue
                "Yeah thats the one, it looks like its seen better days though",
                "Good luck kid, I sense great adventure in your future",
                "What?-",
                "Present it to HIM and your path will open..."
            }, new List<string>(){
                "ned",
                "ned",
                "main",
                "ned"
            }, gameObject, "fadeRemove", "")); //note fade remove callback
            saveDataController.globalSave.oneTimes[5] = true; //Update persistence
            saveDataController.globalSave.serializeSaveData(); //Serialize changes
        }else{ //If player has not hit the correct point
            UIController.setMenuState("none"); //Set menu state back to null
        }      
    }

    void canInteractWithKanyeStatue(){ //Checks for the ability for player to interact with Kanye Statue
        if(saveDataController.globalSave.inventory.findObj("Yeezy") != -1 && !saveDataController.globalSave.oneTimes[6]){ //If player has yeezys and has reacher appropriate part in progression
            saveDataController.globalSave.oneTimes[6] = true; //Update one times
            StartCoroutine(UIController.DiaCycle(new List<string>(){ //New dialogue from kanye
                "Something is happening to the Yeezy?!",
                "Yeezus just rose again...",
                "What-",
            }, new List<string>(){
                "main",
                "unknown", 
                "main",
            }, gameObject, "kanyeArrives", "")); //Note Kanye Arrives follow up
        }else{ //If player cant interact
            UIController.setMenuState("none"); //Set menu state back to none
        }
    }

    void kanyeArrives(){ //Kanye arrives trigger
        StartCoroutine(kanyeArrivesCo()); //Triggers corresponding kanye arrives coroutine
    }

    IEnumerator kanyeArrivesCo(){  //Kanye arrives coroutine
        gameObject.GetComponent<AudioSource>().Play(); //Play kanye summon sound effect
        GameObject fadeShade = UIController.fadeShade; //Get reference to fadeShade
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>(); //Get reference to canvas group on fade shade
        float alpha = 0.0f; //Default alpha of 0, fade shade not visible
        while (alpha < 1f){ //Until alpha 1
            alpha += 0.05f; //Incrase opacity
            canvasGroup.alpha = alpha; //Set opacity
            yield return new WaitForSeconds(0.025f); //Time out shortly and then continue
        }
        swapSprite(); //Swap sprite to empty statue
        Vector3 targetPosition = new Vector3(0f, -0.5f, 0f); //Create target position for player
        GameObject.FindWithTag("Player").GetComponent<Transform>().position = targetPosition; //Move player to target position
        Animator playerAnimator = GameObject.FindWithTag("Player").GetComponent<Animator>(); //Gets reference to animatior
        //Set Player Look direction by setting animatior values   
        playerAnimator.SetFloat("horizontalSpeed", 0);
        playerAnimator.SetFloat("verticalSpeed", 1);
        playerAnimator.SetFloat("totSpeed", 1);
        playerAnimator.SetInteger("lastDirection", 1);
        Vector3 currentCamPosition = GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position; //Get position for main camera
        GameObject.FindWithTag("MainCamera").GetComponent<Transform>().position = new Vector3(targetPosition.x, targetPosition.y, currentCamPosition.z); //Update position for main Camera
        GameObject.Find("kanyeNPC").GetComponent<SpriteRenderer>().enabled = true; //Enables kanye's sprite renderer
        GameObject.Find("defaultPlayer").GetComponent<SpriteRenderer>().sortingLayerName = "MapAbove"; //Move player sorting layer to avoid overlap
        yield return new WaitForSeconds(0.2f); //Short timeout and continue
        StartCoroutine(UIController.DiaCycle(new List<string>(){ //New dialogue for kanye
                "Are you...", //1
                "Kanye West", //2
                "How is that even possible?!", //3
                "Ten years ago <i>they<i> sealed me...", //4
                "Even now I am only a projection, I must combine with my 3 other parts", //5
                "I can maintain this form temporarily, but will need an object to assume", //6
                "Did somebody drug me, am I hallucinating", //7
                "You woke me up with my old pair of Yeezys, those will work fine", //8
                "Wait wait wait. Is this real??", //9
                "Yes", //10
                "And why should I believe you", //11
                "Because I'm Kanye freaking West", //12
                "Now I'm going to posses those Yeezys, we need to get to the closest city as soon as possible" //13
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
                "kanye", //10
                "main", //11
                "kanye", //12
                "kanye" //13
            }, gameObject, "kanyePostFirstDia", "")); //Note kantePostFirstDia followUp

        alpha = 1.0f; //Default alpha of 1.0f
        canvasGroup.alpha = alpha; //Update opacity
        while (alpha > 0f){ //Until alpha is 0
            alpha -= 0.05f; //Decrease alpha
            canvasGroup.alpha = alpha; //Update opacity
            yield return new WaitForSeconds(0.025f); //Timeout and loop
        }
    }

    void kanyePostFirstDia(){ //Kanye post first dia trigger
        StartCoroutine(KanyePostFirstDiaCo()); //Start corresponding coroutine
    }

    IEnumerator KanyePostFirstDiaCo(){ //Coroutine for kanye post dialogue
        GameObject fadeShade = UIController.fadeShade; //Reference to fade shade
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>(); //Reference to fade shade canvas froup
        float alpha = 0.0f; //Default alpha/opacity of zero
        alpha = 0.0f; //Default of alpha 0, not visible
        while (alpha < 1f){ //Until alpha is 1
            alpha += 0.05f; //Increase alpha
            canvasGroup.alpha = alpha; //Update opacity
            yield return new WaitForSeconds(0.025f); //Timeout and loop
        }
        yield return new WaitForSeconds(0.2f); //Timeout and continue
        UIController.setMenuState("none"); //Set menu state back to none
        GameObject.Find("kanyeNPC").GetComponent<SpriteRenderer>().enabled = false; //Disable kanye rendering
        GameObject.Find("defaultPlayer").GetComponent<SpriteRenderer>().sortingLayerName = "Player"; //Set player back to appropriate sorting layer
        alpha = 1.0f; //Default alpha of 1
        canvasGroup.alpha = alpha; //Update canvas group alpha
        while (alpha > 0f){ //Until alpha is 0, not visible again
            alpha -= 0.05f; //Decrease alpha
            canvasGroup.alpha = alpha; //update opacity
            yield return new WaitForSeconds(0.025f); //timeout and loop
        }
        saveDataController.globalSave.oneTimes[6] = true; //Update one times
        saveDataController.globalSave.serializeSaveData(); //Serialize savedata
        StartCoroutine(UIController.DiaCycle(new List<string>(){ //New dialougue to end kanye sequence
                "<i>I don't think I'm crazy...<i>", //1
                "<i>What could be the worst thing that could happen? I'll listen to him for now<i>",
                "<i>I'll leave the junkyard and head towards the city<i>"
            }, new List<string>(){
                "main", //1
                "main", //2
                "main"
            }, gameObject, "", ""));
    }

    void drakeDialogueFollowUp(){ //Drake dialogue follow up trigger
       StartCoroutine(drakeDialogueFollowUpCo()); //Trigger the corresponding coroutine
    }

    IEnumerator drakeDialogueFollowUpCo(){ //Drake dialogue follow up coroutine
        UIController.setMenuState("none"); //Set menu state back to none
        GameObject fadeShade = UIController.fadeShade; //Get reference to fade shade
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>(); //Get reference to CanvasGroup
        float alpha = 0.0f; //Default alpha of 0
        while (alpha < 1f){ //Until alpha is 1
            alpha += 0.05f; //Increase alpha slightly
            canvasGroup.alpha = alpha; //Update opacity
            yield return new WaitForSeconds(0.025f); //Timeout and loop
        }
        SceneManager.LoadSceneAsync("Battle Stage"); //Load battlestage
        yield return null; //Default return condition
    }
}
