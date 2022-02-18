using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class persistSprites : MonoBehaviour
{
    public Sprite mainProfile;
    public Sprite YeProfile;
    public Sprite gokuProfile;
    public Sprite hunterProfile;
    public Sprite banditProfile;
    public Sprite imposterProfile;
    public Sprite nedProfile;

    public static Dictionary<string, Sprite> profiles = new Dictionary<string, Sprite>();
    
    void Awake(){
        if(!(profiles.Count > 0)){
            profiles.Add("main", mainProfile);
            profiles.Add("kanye", YeProfile);
            profiles.Add("goku", gokuProfile);
            profiles.Add("hunter", hunterProfile);
            profiles.Add("bandit", banditProfile);
            profiles.Add("imposter", imposterProfile);
            profiles.Add("ned", nedProfile);
        }
    }
}
