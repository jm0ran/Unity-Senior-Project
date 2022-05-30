using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class saveDataOverride : MonoBehaviour
{
    private Save targetSave;
    



    void Start(){
        targetSave = saveDataController.globalSave;
        targetSave.oneTimes = new List<bool>(){
            true, //0 Opening Diaogue in Junk Cave
            true, //1 Opening Dialogue in Junkyard
            true, //2 MBDTF chest
            true, //3 initial time speaking to Ned in junkyard
            true, //4 Yeezy chest
            true, //5 Yeezy Dialogue Complete and ned is gone
            //--------------------------------------------------------------
            //First short section of one times done
            true, //6 Kanye summoned trigger
            true, //7 Initial encounter with drake fight
            true, //8 Completion of drake fight <-- Want to spawn player in 
        };
        
        targetSave.inventory.items = new List<Item>(){
            // new Item("Yeezy", 3),
            // new Item("MBDTF", 1),
            // new Item("Dragon Ball", 1),
            // new Item("Black Glasses", 1)
            

        };
        targetSave.serializeSaveData();
        
    }
    

    
}
