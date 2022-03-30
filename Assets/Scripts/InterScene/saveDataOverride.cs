using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataOverride : MonoBehaviour
{
    Save targetSave;

    void Start(){
        targetSave = saveDataController.globalSave;
        targetSave.oneTimes = new List<bool>(){
            true, //0 Opening Diaogue in Junk Cave
            true, //1 Opening Dialogue in Junkyard
            false, //2 MBDTF chest
            false, //3 initial time speaking to Ned in junkyard
            false, //4 Yeezy chest
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
