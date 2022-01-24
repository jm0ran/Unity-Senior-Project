using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class charProfileImageController : MonoBehaviour
{
    //Def a better way to do this that future me will find
    [Header("Characters")]
    public Sprite kanye;
    public Sprite goku;
    public Sprite imposter;

    [Header("Enemies")]
    public Sprite drake;
    public Sprite bandit;


    private Image profile;

    public static Dictionary<string, Sprite> spriteDictionary = new Dictionary<string, Sprite>();
    public static Dictionary<string, Sprite> enemyDictionary = new Dictionary<string, Sprite>();

    void Start(){
        profile = gameObject.GetComponent<Image>();

        //Sprite Dictionary
        spriteDictionary.Add("Kanye", kanye);
        spriteDictionary.Add("Goku", goku);
        spriteDictionary.Add("Imposter", imposter);

        //Enemy Dictionary
        enemyDictionary.Add("Drake", drake);
        enemyDictionary.Add("Bandit", bandit);
    


        switchProfile(saveDataController.globalSave.currentTeam[0]);
    }

    void switchProfile(string charName){
        profile.sprite = spriteDictionary[charName];
    }


}
