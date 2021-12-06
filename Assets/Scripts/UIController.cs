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
        }
    }
    void changeText(string inputText){
        textbox.text = inputText;
    }

    void enableUIItem(string item){
        disableUIItem("*"); //Disables all UI items to prevent 2 states from occuring at same time
        switch(item){
            case "photoDia":
                photoDia.SetActive(true);
                break;
            case "noPhotoDia":
                noPhotoDia.SetActive(false);
                break;
        }
        assignTags(/*Want to add a reference to active object*/); //Tags need to be assigned to teh children of the object in order for rest of game to call correct objects
    }

    void disableUIItem(string item){
        switch(item){
            case "*":
                photoDia.SetActive(false);
                noPhotoDia.SetActive(false);
                break;
            case "photoDia":
                photoDia.SetActive(false);
                break;
            case "noPhotoDia":
                noPhotoDia.SetActive(false);
                break;
        }
    }

    void assignTags(){ //This function is going to be used to assign tags to the children of the currently active UI item

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

        disableUIItem("*");
    }    
}
