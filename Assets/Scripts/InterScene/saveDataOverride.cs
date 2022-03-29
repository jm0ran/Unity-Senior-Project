using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataOverride : MonoBehaviour
{
    Save targetSave;

    void Start(){
        targetSave = saveDataController.globalSave;
        targetSave.oneTimes = new List<bool>(){
            false, //0 Opening Diaogue in Junk Cave
            false, //1 Opening Dialogue in Junkyard
            false, //2 Test chest
            false, //3 First development chest
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
            false,
        };
        targetSave.inventory.items = new List<Item>();
        
    }
}
