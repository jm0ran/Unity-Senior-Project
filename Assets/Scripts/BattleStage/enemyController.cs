using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour
{
//Enemy controller controls both the enemy as well as score and victory conditions
//------------------------------------------------------------------------
//Main Variables Used in Scripts   
   public string enemyName = "Drake"; //Enemy name
   public Slider enemySlider; //Reference to enemy slider
   public float enemyHealth = 108f; //Enemy starting health
   public float enemyMaxHealth = 108f; //Enemy max health
   public GameObject victoryScreen; //Reference for victory screen
   public AudioSource hitSoundEffect; //Reference for hit sound effect
   public Transform drakeTransform; //Reference for drake transform object
   public GameObject closingScreen; //Reference for closing scene


//------------------------------------------------------------------------
//User Defined Functions
   public void updateSlider(){ //Updates slider value and checks for victory
      float newValue = enemyHealth / enemyMaxHealth; //Updates slider value to ratio of health to max health
      if(newValue <= 0){ //If ratio is less than or equal to 0 (Player has won)
         audioController targetAudioController = GameObject.Find("audioControllerObj").GetComponent<audioController>(); //Get target audio Controller for music fade
         targetAudioController.noteLocked = true; //Lock note spawns
         targetAudioController.playerWon = true; //Set player won state
         GameObject[] remainingArrows; //Storage for remaining notes
         remainingArrows = GameObject.FindGameObjectsWithTag("arrow"); //Grabs all remaining arrows using arrow tag
         foreach(GameObject arrow in remainingArrows){ //For each remaining note
            arrow.SendMessage("triggerNote"); //Tell note to trigger itself
         }
         victoryScreen.SetActive(true); //Enable victory sceen prob should have had it fade but I forgot to
         enemySlider.value = newValue; //Updates the enemy slider value
         StartCoroutine(startClosing()); //Start the closing process
      }else{ //If player has not yet won
         enemySlider.value = newValue; //Update enemy slider value
      }
   }

   IEnumerator startClosing(){ //Coroutine to start the closing of the battle after victory
      AudioSource mainSong = GameObject.Find("audioControllerObj").GetComponent<AudioSource>(); //Grabs main song audio source to fade it out
      float musicVolume = mainSong.volume; //Grabs current music volume
      while(musicVolume > 0f){ //While music is audible
         musicVolume -= 0.005f; //Lowers music volume by a small amount
         mainSong.volume = musicVolume; //Assigns new volume
         yield return new WaitForSeconds(0.10f); //Times out code and then repeats
      }
      closingScreen.SetActive(true); //Enables closing screen
      CanvasRenderer closingImage = GameObject.Find("closingImage").GetComponent<CanvasRenderer>(); //Gets canvas renderer for closing screen to fade it in
      float opacity = 0f; //Starting opacity of 0 (not visible)
      while(opacity <= 1){ //While opacity is less than 1
         opacity += 0.05f; //Up opacity slightly
         closingImage.SetAlpha(opacity); //Assign new opacity
         yield return new WaitForSeconds(0.05f); //Times out code and then repeats
      }
      victoryScreen.SetActive(false); //Disable victory screen after the closing screen has fully appeares
      //Below is a return gate to hault code until the player hits return key
      while(!Input.GetKeyDown(KeyCode.Return)){ //While return is not held
         yield return null; //LOOP
      }
      //Updates save data
      saveDataController.globalSave.oneTimes[7] = true;
      saveDataController.globalSave.oneTimes[8] = true;
      saveDataController.globalSave.serializeSaveData(); //Serializes new value to json file
      SceneManager.LoadScene("Title Screen"); //Loads title screen
      yield return null; //Default return condition
   }

   IEnumerator shiftDrake(){ //Function to shift drake around on hit
      drakeTransform.position = new Vector3(1.9f,0.44f,0); //Moves him right slightly
      yield return new WaitForSeconds(0.2f); //Short time out
      drakeTransform.position = new Vector3(1.75f,0.44f,0); //Moves him back to original position
      yield return null; //Default return position
   }

   void recieveHit(){ //Function that recieves a hit
      StartCoroutine(shiftDrake()); //Shift drake
      hitSoundEffect.Play(); //Play hit sound effect
      enemyHealth--; //Decrease enemy health by one
      updateSlider(); //Update slider value
   }
//------------------------------------------------------------------------
//Unity Defined Functions

   void Awake(){
      //Assigns necessary values
      enemySlider = GameObject.Find("enemyHealthDisplay").GetComponent<Slider>();
      victoryScreen = GameObject.Find("victoryScreen");
      closingScreen = GameObject.Find("closingScreen");
      hitSoundEffect = gameObject.GetComponent<AudioSource>();
      drakeTransform = gameObject.GetComponent<Transform>();
   }


   void Start(){
      //Disables start and Victory Screen
      victoryScreen.SetActive(false);
      closingScreen.SetActive(false);
      updateSlider();//Updates slider value at start of game
   }


   


   
   
   
}
