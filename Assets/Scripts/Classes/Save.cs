using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

//The Save class is the heart of data persistence and holds basically all of the important info

[System.Serializable]
public class Save{
    public List<Character> acquiredCharacters; //Stores acquired characters, Deprecated
    public string[] currentTeam; //Stores current team, deprecated
    public Inventory inventory; //Stores inventory
    public List<bool> oneTimes = new List<bool>(){ //Stores all one time events which determine scene states and progression
        false, //0 Opening Diaogue in Junk Cave
        false, //1 Opening Dialogue in Junkyard
        false, //2 MBDTF chest
        false, //3 initial time speaking to Ned in junkyard
        false, //4 Yeezy chest
        false, //5 Yeezy Dialogue Complete and ned is gone
        false, //6 Kanye summoned trigger
        false, //7 Initial encounter with drake fight
        false, //8 Completion of drake fight <-- Want to spawn player in 
    };

    public Save(){ //Constructor
        //Creates empty values
        acquiredCharacters = new List<Character>();
        currentTeam = new string[3] {null, null, null};
        inventory = new Inventory();
    }

    public void serializeSaveData(){ //Serializes save data to json to store it between plays
        string jsonDataW = JsonUtility.ToJson(this); //Converts to json
        System.IO.File.WriteAllText(Path.Combine(Application.streamingAssetsPath, "bigBoi.json"), jsonDataW); //Writes save data to file bigBoi
    }

    public void loadSavaData(){ //Load save data from json
        string jsonDataR = ""; //Storage for json
        try{ //Try to read the save date
            jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "bigBoi.json")); //Read data from file
            if(jsonDataR != ""){ //If not empty
                //Load the properties individually because object itself is read only, I could put this into a larger object but that sounds annoying, but is it more annoying then typing this long ass comment
                Save acquiredData = JsonUtility.FromJson<Save>(jsonDataR);
                this.acquiredCharacters = acquiredData.acquiredCharacters;
                this.currentTeam = acquiredData.currentTeam;
                this.inventory = acquiredData.inventory;
                this.oneTimes = acquiredData.oneTimes;
            }else{ //If empty
                Debug.Log("Save data json file is empty"); //Alert that save data object is empty
            }
        }catch{ //If not read
            Debug.Log("Could not find json file at " +  Path.Combine(Application.streamingAssetsPath, "bigBoi.json")); //Alert that json file could not be found
        }
    }

    public void instantiateCharacter(string charName){ //Instantiate character from acquired characters, deprecated after battle rework
        bool willContinue = true; //default value
        for(int i = 0; i < acquiredCharacters.Count; i++){ //For each acquired character
            if(acquiredCharacters[i].name == charName){ //If match
                willContinue = false; //Stop function
            }else{ //If not match
                willContinue = true; //Continue function
            }
        }
        if(willContinue){ //If continue because found
            try{ //Try to read character
                string jsonDataR = System.IO.File.ReadAllText(Path.Combine(Application.streamingAssetsPath, "characters", charName + ".json")); //Read json
                Character returnChar = JsonUtility.FromJson<Character>(jsonDataR); //Sort json
                acquiredCharacters.Add(returnChar); //Add character read
            }catch{ //If failed to read
                willContinue = false; //Stop function
                Debug.Log("No character found with name: " + charName); //Alert failure
            }
        }else{
            Debug.Log("Character Already Exists"); //Alert of character duplication
        }
        serializeSaveData(); //Serialize changes
    }

    public void assignToTeam(string charName, int charIndex){ //Sssign character to a team
        bool charExists = false; //By default character is expected not to exist
        bool charAlreadyInTeam = false; //By default char is expected to be not in team
        for(int i = 0; i < this.acquiredCharacters.Count; i++){ //For all the acquired characters
            if(acquiredCharacters[i].name == charName){ //If match
                charExists = true; //Update char exists
            }
        }
        for(int i = 0; i < this.currentTeam.Length; i++){ //For each character in team
            if(currentTeam[i] == charName){ //If match
                charAlreadyInTeam = true; //Update char in team
            }
        }
        if(charExists && !charAlreadyInTeam && charIndex <= 3 && charIndex >= 0){ //If character exists and is not in team and char index is valid
            this.currentTeam[charIndex] = charName; //Add char to team
        }
        serializeSaveData(); //Serialize changes
    }

    public void pruneTeam(){ //USed to prune team if format invalid
        for(int i = 0; i < this.currentTeam.Length; i++){ //For each character
            if(this.currentTeam[i] == ""){ //If character is null
                for(int j = i + 1; j < this.currentTeam.Length; j++){ //For characters after one in question
                    currentTeam[j - 1] = currentTeam[j]; //Move all 1 left
                    currentTeam[j] = ""; //Make right null
                } //Repeat as many times as necessary to ensure proper team format
            }
        }
        serializeSaveData(); //Serialize changes
    }
}
