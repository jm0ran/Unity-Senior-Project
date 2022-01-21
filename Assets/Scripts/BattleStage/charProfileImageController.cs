using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charProfileImageController : MonoBehaviour
{
    //Def a better way to do this that future me will find
    public Sprite Kanye;
    public Sprite Goku;
    public Sprite Imposter;

    private Image profile;

    public static Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();

    void Start(){
        profile = gameObject.GetComponent<Image>();
        spriteDictionary.Add("Kanye", Kanye);
        spriteDictionary.Add("Goku", Goku);
        spriteDictionary.Add("Imposter", Imposter);
        switchProfile(saveDataController.globalSave.currentTeam[0]);
    }

    void switchProfile(string charName){
        profile.sprite = spriteDictionary[charName];
    }


}
