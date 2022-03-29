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
        StartCoroutine(chestCo(followUpArg));
    }

    void swapSprite(){
        if(altSprite != null){
            gameObject.GetComponent<SpriteRenderer>().sprite = altSprite;
        }else{
            Debug.Log("AltSprite is null");
        }
    }

    IEnumerator fadeRemoveCo(){ //Used to fade screen in and out while also disabling the NPC that this is triggered on
        StartCoroutine(UIController.fadeOut(null));
        yield return new WaitForSeconds(0.8f);
        StartCoroutine(UIController.fadeIn());
        gameObject.GetComponent<SpriteRenderer>().enabled = false;
        Component[] collidersToDestroy = gameObject.GetComponents<CircleCollider2D>() as Component[];
        //Destroys both the collision collider and interaction collider
        foreach(Component indCol in collidersToDestroy){
            Destroy(indCol as CircleCollider2D);
        }
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
