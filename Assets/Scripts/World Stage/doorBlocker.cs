using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class doorBlocker : MonoBehaviour
{
    public bool isActive = false;
    public string returnDirection = "up";
    public GameObject playerObj;
    public string message = "";

    public void Awake(){
        playerObj = GameObject.FindWithTag("Player");
    }


    public void recievePlayer(){
        if(!isActive){
            StartCoroutine(recievePlayerCo());
        } 
    } 

    public IEnumerator recievePlayerCo(){
        UIController.returnGate = false;
        isActive = true;
        playerObj.SendMessage("lockPlayer", true);
        UIController.setMenuState("noPhotoDia");
        UIController.updateDia(message);
        while(!UIController.returnGate){
            yield return null;
        }
        UIController.returnGate = false;
        UIController.setMenuState("none");
        playerObj.SendMessage("lockPlayer", false);
        isActive = false;
        yield return null;
    }

    void disableBarrier(){
        Component[] colliders = GetComponents<PolygonCollider2D>() as Component[];
        foreach(Component collider in colliders){
            Destroy(collider as PolygonCollider2D);
        }
            
    }
}
