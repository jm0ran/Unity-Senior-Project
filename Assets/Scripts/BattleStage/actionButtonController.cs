using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actionButtonController : MonoBehaviour
{
    public string triggerName;
    private Button button;
    private GameObject buttonLayers;

    void Start(){
        buttonLayers = GameObject.Find("ButtonLayers");
    }

    void OnEnable(){
        button = gameObject.GetComponent<Button>();
        button.onClick.RemoveAllListeners();
        button.onClick.AddListener(actionButtonClick);
        if(triggerName == "fight"){
            button.Select();
        }else if(triggerName == "move1"){
            button.Select();
        }else if(triggerName == "char1"){
            button.Select(); //Combine all this into one conditional soon
        }
    }

    void actionButtonClick(){
        buttonLayers.SendMessage("processButton", triggerName);
    }
    
}
