using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
//------------------------------------------------------------------------
//Main Variables Used in Scripts
    //References to UI Layers
    public static GameObject photoDia;
    public static GameObject noPhotoDia;
    public static GameObject inventory;
    public static GameObject fadeShade;
    //Storage for UI items
    public static GameObject currentLayer = null;
    public static GameObject currentTextBox = null;
    public static GameObject currentProfileBox = null;
    public static GameObject itemsContainer = null;
    public static bool returnGate = false; //Return gate variable, static, used in many other scripts
    public static GameObject player; //static reference to player
    public GameObject inventoryPrefabPointer; //Pointer to inventoryPrefab
    public static GameObject inventoryPrefab; //Static reference to inventory prefab
    public static GameObject mapObj; //Static reference to map object
    public static AudioSource nextDialogueSFX; //Static reference to nextDialogue sound effect

//------------------------------------------------------------------------
//User Defined Functions
   public static GameObject findChild(string target, GameObject parent){ //Find child function
        if(parent != null){ //if parent exists
            foreach (Transform child in parent.transform){ //For each child
                if(child.gameObject.name == target){ //if Child name matches target                    
                    return child.gameObject; //Return found object
                }
            }
        }
        return null; //If nothing was found return null
    }

    public static void prepareChildren(){ //Function to prepare children references
        currentTextBox = findChild("text", currentLayer); //Assign current text box pointer
        currentProfileBox = findChild("profileImage", currentLayer); //Assign current profile box pointer
    }

    public static void setMenuState(string desiredState){ //Function to set menu state
        switch (desiredState)
        {
            //Based on the passed desire state set inventory layers corresponding to it
            case "none":
                currentLayer = null;
                photoDia.SetActive(false);
                noPhotoDia.SetActive(false);
                inventory.SetActive(false);
                nextDialogueSFX.Play();

                //IMPORTANT TO HAVE FOLLOWUPS SET MENU STATE TO NONE AS THIS UPDATES CHARACTERS
                mapObj.SendMessage("updateScene");
                break;
            case "photoDia":
                currentLayer = photoDia;
                photoDia.SetActive(true);
                noPhotoDia.SetActive(false);
                inventory.SetActive(false);
                break;
            case "noPhotoDia":
                currentLayer = noPhotoDia;
                photoDia.SetActive(false);
                noPhotoDia.SetActive(true);
                inventory.SetActive(false);
                break;
            case "inventory":
                nextDialogueSFX.Play();
                currentLayer = inventory;
                photoDia.SetActive(false);
                noPhotoDia.SetActive(false);
                inventory.SetActive(true);
                renderItems();
                break;
            default:
                Debug.Log("Entered an invalid desired state for the UI"); //If an invalid state was passed alert
                break;
        }
        if(desiredState != "none"){ //if desired state was not none
            player.SendMessage("lockPlayer", true); //Playe needs to be locked
        }else{ //If it was none
            player.SendMessage("lockPlayer", false); //Player needs to be unlocked
        }
        if(desiredState != "inventory"){ //If target is inventory
            prepareChildren(); //Prepare children so pointers are correct
            inventoryController.invStatusUpdate(false); //Update inv status to false
        }else{
            inventoryController.invStatusUpdate(true); //Else update inv status to true
        }
    }

    public static void updateDia(string inputText, string spriteName){ //Update line of dialogue, version 1 with sprite name for photoDia
        currentTextBox.GetComponent<TextMeshProUGUI>().text = inputText; //Update text
        currentProfileBox.GetComponent<Image>().sprite = persistSprites.profiles[spriteName]; //Update sprite
    }

    public static void updateDia(string inputText){ //Update line of dialogue, version 2 with no sprite name for noPhotoDia
        currentTextBox.GetComponent<TextMeshProUGUI>().text = inputText; //Update text
    }

    public static IEnumerator DiaCycle(List<string> dia, List<string> diaOrder, GameObject origin, string followUp, string followUpArg){ //Dia cycle with photos
        returnGate = false; //Set return gate to false default
        setMenuState("photoDia"); //Enable photoDia layer
        bool hasDialogue = true; //There is dialogue remaining
        if(dia.Count > 0){ //If dia count is greater than 0
            nextDialogueSFX.Play(); //Play sound effect
            updateDia(dia[0], diaOrder[0]); //Update dialogue and photo
        }else{ //if out of dialogue
            hasDialogue = false; //No more dialogue remaining
        }
        if(hasDialogue){ //If still has dialogue
            yield return new WaitForSeconds(0.3f); //Time out and continue
            for(int i = 1; i < dia.Count; i++){ //While dialogue still remains
                while(!returnGate){ //Wait for return gate
                    yield return null;
                }
                nextDialogueSFX.Play(); //Play next dialogue SFX
                updateDia(dia[i], diaOrder[i]); //Update dialogue and sprite
                yield return new WaitForSeconds(0.3f); //Timeout and continue
                returnGate = false; //Reset return gate
            }
            while(!returnGate){ //Wair for return gate
                yield return null;
            }
            returnGate = false; //Reset return gate
        }
        if(followUp != ""){ //If follow up is not null
            origin.SendMessage(followUp, followUpArg); //Send follow up message and argument
        }else{ //if no follow up
            setMenuState("none"); //Set menu state back to none
        }
    }

    public static IEnumerator DiaCycle(List<string> dia, GameObject origin, string followUp, string followUpArg){
        returnGate = false; //Set return gate to default
        setMenuState("noPhotoDia"); //Set menu state to no photo dia
        bool hasDialogue = true; //If player has dialogue remaining
        if(dia.Count > 0){ //If dia count is greater than zero
            nextDialogueSFX.Play(); //Play next dialogue sfx
            updateDia(dia[0]); //Update dialogue
        }else{ //if out of dialogue
            hasDialogue = false; //No longer has dialogue
        }
        if(hasDialogue){ //If there is still remaining dialogue
            yield return new WaitForSeconds(0.3f); //Time out and continue
            for(int i = 1; i < dia.Count; i++){ //For each line of dialogue
                while(!returnGate){ //Wait for return gate
                    yield return null;
                }
                nextDialogueSFX.Play(); //Play next dialogue sound effect
                updateDia(dia[i]); //Update dialogue
                yield return new WaitForSeconds(0.3f); //Time out and continue
                returnGate = false; //Reset return gate
            }
            while(!returnGate){ //Wait for return gate
                yield return null;
            }
            returnGate = false; //Reset return gate
            
        }
        if(followUp != ""){ //If followUp is set
            origin.SendMessage(followUp, followUpArg); //Send followup and follow up arguments
        }else{ //If no follow up
            setMenuState("none"); //Set menu state back to none 
        }
    }

    public static IEnumerator fadeIn(){ //Fade in function
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>(); //Get reference to canvas group
        float alpha = 1.0f; //Set default alpha to 1, fully visible
        canvasGroup.alpha = alpha; //Update opacity
        while (alpha > 0f){ //Until opacity is 0
            alpha -= 0.05f; //Decrease alpha
            canvasGroup.alpha = alpha; //Update opacity
            yield return new WaitForSeconds(0.025f); //Time out and loop
        }
    }

    public static IEnumerator fadeOut(string destScene){ //Fade out function
        CanvasGroup canvasGroup = fadeShade.GetComponent<CanvasGroup>(); //Get reference to canvas group
        float alpha = 0.0f; //Default alpha of 0, not visible
        while (alpha < 1f){ //Until alpha is 1
            alpha += 0.05f; //Increase alpha
            canvasGroup.alpha = alpha; //Update opacity
            yield return new WaitForSeconds(0.025f); //Timeout and loop
        }
        if(destScene != null){ //If destScene is set
            SceneManager.LoadScene(destScene); //Transfer to dest scene
        }
    }

    public static void renderItems(){ //Render items for inventory
        GameObject targetObject = findChild("items", currentLayer); //Find items object
        List<Item> playerItems = saveDataController.globalSave.inventory.items; //Create a list with all children on items
        for(int i = 0; i < playerItems.Count; i++){ //for each item
            GameObject newItemRow = Instantiate(inventoryPrefab, new Vector3(0, 0, 0), Quaternion.identity) as GameObject; //Instantiate an inventoryRowPrefab
            newItemRow.gameObject.name = playerItems[i].itemName; //Set item name of inventory row
            TextMeshProUGUI itemNameBox = findChild("itemName", newItemRow).GetComponent<TextMeshProUGUI>(); //Get reference to item name box
            Image itemImageBox = findChild("itemImage", newItemRow).GetComponent<Image>(); //Get reference to item image box
            itemNameBox.text = playerItems[i].itemName; //Set itemNameBox
            itemImageBox.sprite = itemController.itemDictionary[playerItems[i].itemName]; //Set ItemImageBox
            newItemRow.transform.SetParent(itemsContainer.transform, false); //Set itemRow as child of items
        }
    }

//------------------------------------------------------------------------
//Unity Defined Function
    void Awake(){
        //Assign the appropriate values
        player = GameObject.FindWithTag("Player");
        photoDia = findChild("photoDia", gameObject);
        noPhotoDia = findChild("noPhotoDia", gameObject);
        inventory = findChild("inventory", gameObject);
        itemsContainer = findChild("itemsContainer", inventory);
        mapObj = GameObject.FindWithTag("Map");
        fadeShade = GameObject.Find("fadeShade");
        nextDialogueSFX = GameObject.Find("dialogueSoundEffect").GetComponent<AudioSource>();
        inventoryPrefab = inventoryPrefabPointer;
        
    }

    void Start(){
        StartCoroutine(fadeIn()); //Fade in scene
        //Turn all UI layers off
        currentLayer = null;
        photoDia.SetActive(false);
        noPhotoDia.SetActive(false);
        inventory.SetActive(false);

    }

}
