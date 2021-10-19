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
    
    public Image profileBox;

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
    void changeText(){
        //Want to move the functione used to update the text on the screen to here
    }

    //------------------------------------------------------------------------
    //Unity Defined Functions
    void Start()
    {
        //Very initial testing of just how changing the image works in unity and stuff preparing for a system to change during dialougue
        profileBox = GameObject.FindWithTag("profileBox").GetComponent<Image>();
        gameObject.SetActive(false); //Disables object initially so that the UI is not visable on the start of a Scene
    }    
}
