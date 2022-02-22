using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class UIController : MonoBehaviour
{
    
    // [Header("Parent Level Game Obects")]
    public static GameObject photoDia;
    public static GameObject noPhotoDia;
    public static GameObject inventory;

    public static GameObject currentLayer = null;
    public static GameObject currentTextBox = null;
    public static GameObject currentProfileBox = null;

    public static bool returnGate = false;

    public static GameObject player;
    

   public static GameObject findChild(string target, GameObject parent){
        if(parent != null){
            foreach (Transform child in parent.transform){
                if(child.gameObject.name == target){                    return child.gameObject;
                }
            }
        }else{
        }
        return null;
        
    }

    public static void prepareChildren(){
        currentTextBox = findChild("text", currentLayer);
        currentProfileBox = findChild("profileImage", currentLayer);
    }

    public static void setMenuState(string desiredState){
        switch (desiredState)
        {
            //Here I want to store the current layer in a variable and use it to setup other variables
            case "none":
                currentLayer = null;
                photoDia.SetActive(false);
                noPhotoDia.SetActive(false);
                inventory.SetActive(false);
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
                currentLayer = inventory;
                photoDia.SetActive(false);
                noPhotoDia.SetActive(false);
                inventory.SetActive(true);
                renderItems();
                break;
            default:
                Debug.Log("Entered an invalid desired state for the UI");
                break;
        }
        if(desiredState != "none"){
            player.SendMessage("lockPlayer", true);
        }else{
            player.SendMessage("lockPlayer", false);
        }
        if(desiredState != "inventory"){
            prepareChildren();
        }
    }

    public static void updateDia(string inputText, string spriteName){
        currentTextBox.GetComponent<TextMeshProUGUI>().text = inputText;
        currentProfileBox.GetComponent<Image>().sprite = persistSprites.profiles[spriteName]; //Going to call my sprite dictionary
    }
    public static void updateDia(string inputText){
        currentTextBox.GetComponent<TextMeshProUGUI>().text = inputText;
    }



    void Awake(){
        player = GameObject.FindWithTag("Player");
        photoDia = findChild("photoDia", gameObject);
        noPhotoDia = findChild("noPhotoDia", gameObject);
        inventory = findChild("inventory", gameObject);
        
    }

    void Start(){
        StartCoroutine(fadeIn());
        setMenuState("none");
    }

    //Going to override for Dialogue without images
    public static IEnumerator DiaCycle(List<string> dia, List<string> diaOrder, GameObject origin, string followUp){
        returnGate = false;
        setMenuState("photoDia");
        updateDia(dia[0], diaOrder[0]);
        yield return new WaitForSeconds(0.3f);
        for(int i = 1; i < dia.Count; i++){
            while(!returnGate){
                yield return null;
            }
            updateDia(dia[i], diaOrder[i]);
            yield return new WaitForSeconds(0.3f);
            returnGate = false;
        }
        while(!returnGate){
            yield return null;
        }
        returnGate = false;
        setMenuState("none");


        if(followUp != ""){
             origin.SendMessage(followUp);
        }
    }
    public static IEnumerator DiaCycle(List<string> dia, GameObject origin, string followUp){
        returnGate = false;
        setMenuState("noPhotoDia");
        updateDia(dia[0]);
        yield return new WaitForSeconds(0.3f);
        for(int i = 1; i < dia.Count; i++){
            while(!returnGate){
                yield return null;
            }
            updateDia(dia[i]);
            yield return new WaitForSeconds(0.3f);
            returnGate = false;
        }
        while(!returnGate){
            yield return null;
        }
        returnGate = false;
        setMenuState("none");
        if(followUp != ""){
             origin.SendMessage(followUp);
        }
    }

    public static IEnumerator fadeIn(){
        CanvasGroup canvasGroup = GameObject.Find("fadeShade").GetComponent<CanvasGroup>();
        float alpha = 1.0f;
        canvasGroup.alpha = alpha;
        while (alpha > 0f){
            alpha -= 0.05f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.025f);
        }
    }

    public static IEnumerator fadeOut(string destScene){
        CanvasGroup canvasGroup = GameObject.Find("fadeShade").GetComponent<CanvasGroup>();
        float alpha = 0.0f;
        while (alpha < 1f){
            alpha += 0.05f;
            canvasGroup.alpha = alpha;
            yield return new WaitForSeconds(0.025f);
        }
        SceneManager.LoadScene(destScene);
    }

    public static void renderItems(){
        Debug.Log("Attempted to render items");
        GameObject targetObject = findChild("items", currentLayer);
        for(int i = 0; i < saveDataController.globalSave.inventory.items.Count; i++){
            Debug.Log("item " + i);
        }
        // GameObject newInvRow = Instantiate(inventoryRowPrefab, rowLocation, Quaternion.identity);

        //Should clear contents of this section first to prevent buildup every time you open menu but I will do that later
    }

}
