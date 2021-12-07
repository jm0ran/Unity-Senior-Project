using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    //Want to use UI Controller to control what is being shown in the UI stuff like menus and Dialogue for when I have more than just textboxs
    //------------------------------------------------------------------------
    //Main variables used in Script
    //The character profiles being used are stored in these Sprite variables
    public Sprite mainProfile;
    public Sprite YeProfile;
    public Sprite gokuProfile;
    public Sprite hunterProfile;
    public Sprite banditProfile;
    public Sprite imposterProfile;
    

    //Need to find a way to dynamically set these based on what UI Element is avaliable
    public Image profileBox;
    public Text textbox;
    public GameObject textObj;


    //Layers of the UI
    public List<GameObject> layersList;
    public GameObject activeItemObj;
    public string activeItemType;

    public GameObject photoDia;
    public GameObject noPhotoDia;

    //------------------------------------------------------------------------
    //User defined functions
    void changeProfile(string inProfile){
        //This is where we're going to be changinng the profile photo for the charcter window
        switch (inProfile){
            case "Kanye":
                profileBox.sprite = YeProfile;
                break;
            case "Amongus":
                profileBox.sprite = imposterProfile;
                break;
            case "Main":
                profileBox.sprite = mainProfile;
                break;
            default:
                profileBox.sprite = null;
                break;
        }
    }

    void changeText(string inputText){ //Probably want to take argument as a concatanated string
            foreach (Transform child in activeItemObj.transform){ //Runs for each child of the current active item
                if(child.gameObject.tag == "textBox"){
                   textObj = child.gameObject;
               }
            }
            if(activeItemType == "photoDia"){
                changeProfile(inputText.Substring(inputText.IndexOf(";") + 1));
            }
            textObj.GetComponent<Text>().text = inputText.Substring(0, inputText.IndexOf(";"));
            
    }

    void enableUIItem(string item){
        disableUIItems(); //Disables all UI items to prevent 2 states from occuring at same time
        activeItemObj = null;
        activeItemType = item;
        switch(item){
            case "photoDia":
                photoDia.SetActive(true);
                activeItemObj = photoDia;
                break;
            case "noPhotoDia":
                activeItemObj = noPhotoDia;
                noPhotoDia.SetActive(false);
                break;
            default:
                Debug.Log("No object with name: " + item + " found");
                break;
        }
    }

    void disableUIItems(){
        foreach(GameObject layer in layersList){
                    layer.SetActive(false);
        }   
    }

    void assignTags(){ //This function is going to be used to assign tags to the children of the currently active UI item
        foreach(GameObject layer in layersList){
            Debug.Log(layer.activeSelf);
        }

    }

    //------------------------------------------------------------------------
    //Unity Defined Functions
    void Start()
    {
        //Very initial testing of just how changing the image works in unity and stuff preparing for a system to change during dialougue
        profileBox = GameObject.FindWithTag("profileBox").GetComponent<Image>();
        textbox = GameObject.FindWithTag("textBox").GetComponent<Text>();
        photoDia = GameObject.FindWithTag("photoDia");
        noPhotoDia = GameObject.FindWithTag("noPhotoDia");

        layersList = new List<GameObject>();
        layersList.Add(photoDia);
        layersList.Add(noPhotoDia);

        disableUIItems();
        // enableUIItem("photoDia");
        // changeText("Testing");
    }    
}
