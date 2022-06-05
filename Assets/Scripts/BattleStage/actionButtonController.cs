using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class actionButtonController : MonoBehaviour
{

//The action button controller script is deprecated as I changed the battle system. The script was initially applied to each of the buttons in my original turn based rythmn combo battle system, the controller was purely to hold necessary information for the button and tranfer that information on clicks
//------------------------------------------------------------------------
//General Variables Used in Script
    public string triggerName; //Button name
    private Button button; //Holds reference to button component
    private GameObject buttonLayers; //Holds reference to button layers game object

//------------------------------------------------------------------------
//User Defined Functions

    void actionButtonClick(){ //Function triggered by pressing button, sends information to another controler to process the button click
        buttonLayers.SendMessage("processButton", triggerName); //Sends information to control present on buttonlayers game object passing the trigger name as the argument
    }

//------------------------------------------------------------------------
//Unity Defined Functions

    void Awake(){ //On object awake
        buttonLayers = GameObject.Find("ButtonLayers"); //Assigns buttonLayers
        button = gameObject.GetComponent<Button>(); //Assigns button component
    }

    void OnEnable(){ //On Object enable
        button.onClick.RemoveAllListeners(); //Removes any previous listeners 
        button.onClick.AddListener(actionButtonClick); //Adds listener for click event
        
        //Logic below sets default selection for each button menu so the mouse does not have to be used
        if(triggerName == "fight"){
            button.Select();
        }else if(triggerName == "move1"){
            button.Select();
        }else if(triggerName == "char1"){
            button.Select();
        }
    }

    
    
}
