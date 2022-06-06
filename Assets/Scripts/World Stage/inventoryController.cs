using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class inventoryController : MonoBehaviour
{

//The inventory controller controls the logic of the inventory UI layer

//------------------------------------------------------------------------
//Predefined variables for the script
    private Inventory playerInv; //player inventory reference
    private GameObject UI; //UI reference
    private GameObject inventoryUI; //InventoryUI reference
    private static GameObject itemsContainer; //itemsContainer reference
    private GameObject player; //player reference
    private List<GameObject> itemObjectsList; //Storage for itemObjects
    private static int currentSelection = 0; //Going to be static so that my input controller can change them (may be bad idea may be fine)
    public static bool inAction; //In action booleon
    private GameObject[] currentItemObjects; //Storage for current item gameObjects
    public AudioSource inputSFX; //InputSFX reference
    private Image currentItemImage; //Current item image reference
    private TextMeshProUGUI currentItemDescription; //Current item descriptin reference
    private TextMeshProUGUI currentItemAmount; //Current item amount reference
    
//------------------------------------------------------------------------
//User Defined Functions
    void menuInput(string inputPassed){ //Process menu input function
        if(inAction){ //If in action
            if(inputPassed == "down"){ //If input is down
                updateSelected(currentSelection + 1); //Update selected object
                inputSFX.Play(); //Play input SFX
            }else if(inputPassed == "up"){ //
                updateSelected(currentSelection - 1); //Update selected object
                inputSFX.Play(); //Play input SFX
            }
        }
    }

    public static void invStatusUpdate(bool status){ //Basically starts the inventory
        if(status){ //If status is true
            currentSelection = 0; //Sets default selection to 0 upon opening the menu
            GameObject.FindWithTag("inventory").SendMessage("initController"); //InitController on inventory
        }else{
            inAction = false; //No longer in action
            foreach(Transform child in itemsContainer.transform){ //For each old object
                Destroy(child.gameObject); //Destroy it
            }
        }
    }

    public void updateSelected(int target){ //This function is just going to clearly highlight the currently selected item and if needed move the objects up in order to  view and stuff
        if(target >= 0 & target < itemObjectsList.Count){ //If items and there is space to move
            currentSelection = target; //Updates current selection to reflect a successful change
            GameObject selectedObject = itemObjectsList[target]; //Might need to add something here if inventory has less than 1 item who knows lol
            for(int i = 0; i < itemObjectsList.Count; i++){ //Logic for resetting item box colors and item box positioning, also updates current item info stuff
                GameObject item = itemObjectsList[i]; //Set current item
                item.GetComponent<Image>().color = new Color32(255, 255, 255, 255); //Set selected item color
                Transform itemRowPosition = item.GetComponent<Transform>(); //Get reference to item row position
                //Want to determine if I even need to slide, just generall positions my boxes
                if(itemObjectsList.Count >= 4 && currentSelection >= 2){ //Want to check if there is space
                    itemRowPosition.localPosition = new Vector3(0,340 - (280 * i - (280 * (currentSelection - 1))),0); //Move item
                }else{
                    itemRowPosition.localPosition = new Vector3(0,340 - (280 * i),0); //Move item
                }
            }
            selectedObject.GetComponent<Image>().color = new Color32(255, 112, 112, 100); //Set selected gameObject to different color
            //Set currentItemInfo Image and description
            currentItemImage.sprite = itemController.itemDictionary[selectedObject.name];
            currentItemDescription.text = itemController.infoDictionary[selectedObject.name];
            currentItemAmount.text = saveDataController.globalSave.inventory.items[saveDataController.globalSave.inventory.findObj(selectedObject.name)].itemAmount.ToString();;
        }
    }

    public void initController(){ //Initialization for controller
        currentSelection = 0; //Default first item for current selection
        inAction = true; //Set inAction to true
        itemObjectsList = new List<GameObject>(); //Create empty list for items
        //The following just find the children and throws them into a list of gameObjects;
        foreach (Transform child in itemsContainer.transform){
            itemObjectsList.Add(child.gameObject);
        }
        updateSelected(currentSelection); //This is the initial update for selected, just initially highlights the first option
    }

//------------------------------------------------------------------------
//Unity Defined Functions
    void Awake(){
        //Sets necessary values
        inAction = false;
        UI = GameObject.FindWithTag("UI");
        inventoryUI = GameObject.FindWithTag("inventory");
        itemsContainer = GameObject.Find("itemsContainer");
        //If not title scene
        if(SceneManager.GetActiveScene().name != "Title Screen" && SceneManager.GetActiveScene().name != "Battle Stage" && SceneManager.GetActiveScene().name != "Cutscene 1"){
            currentItemImage = GameObject.Find("currentItemImage").GetComponent<Image>();
            currentItemDescription = GameObject.Find("currentItemDescription").GetComponent<TextMeshProUGUI>();
            currentItemAmount = GameObject.Find("currentItemAmount").GetComponent<TextMeshProUGUI>();
            inputSFX = GameObject.Find("dialogueSoundEffect").GetComponent<AudioSource>();
        }
    }
}
