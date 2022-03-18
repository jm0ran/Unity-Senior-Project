using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class npcFollowUp : MonoBehaviour
{
    void fadeRemove(){
        StartCoroutine(fadeRemoveCo());
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
}
