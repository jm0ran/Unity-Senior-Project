using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistSprites : MonoBehaviour
{

//The Persist Sprites Controller is used to store sprite that will be used in various scripts throughout the game

//------------------------------------------------------------------------
//Main Variables Used in Scripts
    //Storage for the profile sprites
    public Sprite mainProfile;
    public Sprite YeProfile;
    public Sprite gokuProfile;
    public Sprite hunterProfile;
    public Sprite banditProfile;
    public Sprite imposterProfile;
    public Sprite nedProfile;
    public Sprite unknownProfile;
    public Sprite drakeProfile;

    //Dictionary to add keys to sprite
    public static Dictionary<string, Sprite> profiles = new Dictionary<string, Sprite>();
    
    
//------------------------------------------------------------------------
//Unity Defined Function
    void Awake(){
        if(!(profiles.Count > 0)){ //If profiles dictionary has not been set yet
            //Add the necessary sprites
            profiles.Add("main", mainProfile);
            profiles.Add("kanye", YeProfile);
            profiles.Add("goku", gokuProfile);
            profiles.Add("hunter", hunterProfile);
            profiles.Add("bandit", banditProfile);
            profiles.Add("imposter", imposterProfile);
            profiles.Add("ned", nedProfile);
            profiles.Add("unknown", unknownProfile);
            profiles.Add("drake", drakeProfile);
        }
    }
}
