using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charProfileImageController : MonoBehaviour
{

//Depreacated from old battle system, but was used to store character profile images
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    //Used Headers for visual editor, each Sprite variable just stores the enemy image sprite
    [Header("Characters")]
    public Sprite kanye;
    public Sprite goku;
    public Sprite imposter;

    [Header("Enemies")]
    public Sprite drake;
    public Sprite bandit;

    private Image profile; //Profile image component reference

    //Dictionaries used to store sprites linked to strings, static so other components can access it easily
    public static Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> enemyDictionary = new Dictionary<string, Sprite>();

//------------------------------------------------------------------------
//User Defined Functions
    void switchProfile(string charName){ //Switches current profile
        profile.sprite = spriteDictionary[charName]; //Set profile sprite to passes sprite name looked up in dictionary
    }


//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){ //Want sprite dictionary loaded in Awake
        //Adds characters sprites to dictionary
        spriteDictionary.Add("Kanye", kanye);
        spriteDictionary.Add("Goku", goku);
        spriteDictionary.Add("Imposter", imposter);

        //Adds enemy sprites to dictionary
        enemyDictionary.Add("Drake", drake);
        enemyDictionary.Add("Bandit", bandit);

        profile = gameObject.GetComponent<Image>(); //Sets reference to profile
    }

    void Start(){ //On Start
        switchProfile(saveDataController.globalSave.currentTeam[0]); //Switch profile to first character
    }

    


}
