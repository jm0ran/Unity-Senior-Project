using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class battleButtonController : MonoBehaviour
{
    public void clickButton(){
        Debug.Log("Clicked");
        if(gameObject.name == "retryButton"){
            Debug.Log("Retry");
        }else{
            Debug.Log(gameObject.name);
        }
    }


}
