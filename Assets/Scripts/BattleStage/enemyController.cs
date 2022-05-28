using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class enemyController : MonoBehaviour
{
   public string enemyName = "Drake"; //Going to be set dynamically in the future
   public Enemy enemyObj;
   public Slider enemySlider;
   public float enemyHealth = 100f;
   public float enemyMaxHealth = 100f;
   public GameObject victoryScreen;
   public AudioSource hitSoundEffect;
   public Transform drakeTransform;
   public GameObject closingScreen;
   

   void Awake(){
      enemySlider = GameObject.Find("enemyHealthDisplay").GetComponent<Slider>();
      victoryScreen = GameObject.Find("victoryScreen");
      closingScreen = GameObject.Find("closingScreen");
      hitSoundEffect = gameObject.GetComponent<AudioSource>();
      drakeTransform = gameObject.GetComponent<Transform>();
   }


   void Start(){
      victoryScreen.SetActive(false);
      closingScreen.SetActive(false);
      loadEnemy();
      updateSlider();

      //Temporary
   }

   void updateSlider(){
      float newValue = enemyHealth / enemyMaxHealth;
      if(newValue <= 0){
         audioController targetAudioController = GameObject.Find("audioControllerObj").GetComponent<audioController>();
         targetAudioController.noteLocked = true;
         targetAudioController.playerWon = true;
         //Trigger all remaining notes
         GameObject[] remainingArrows;
         remainingArrows = GameObject.FindGameObjectsWithTag("arrow");
         foreach(GameObject arrow in remainingArrows)
         {
            arrow.SendMessage("triggerNote");
         }
         victoryScreen.SetActive(true);//Prob want to put this transition into a coroutine to make it a bit smoother, also remove old notes with prune prob
         enemySlider.value = newValue;

         StartCoroutine(startClosing());
      }else{
         enemySlider.value = newValue;
      }
      
   }

   IEnumerator startClosing(){
      AudioSource mainSong = GameObject.Find("audioControllerObj").GetComponent<AudioSource>();
      float musicVolume = mainSong.volume;
      //Fade out music
      while(musicVolume > 0f){
         musicVolume -= 0.005f;
         mainSong.volume = musicVolume;
         yield return new WaitForSeconds(0.10f);
      }
      closingScreen.SetActive(true);
      //Fade in the closing screen relatively quickly
      
      CanvasRenderer closingImage = GameObject.Find("closingImage").GetComponent<CanvasRenderer>();
      float opacity = 0f;

      while(opacity <= 1){
         opacity += 0.05f;
         closingImage.SetAlpha(opacity);
         yield return new WaitForSeconds(0.05f);
      }

      victoryScreen.SetActive(false); //Disable victory screen after
      
      //Now I need to add a return gate

      while(!Input.GetKeyDown(KeyCode.Return)){
         yield return null;
      }
        

      //NEED TO SET PERSIST ID TO TRUE HERE TO PREVENT REPEAT OF BOSS BATTLE
      saveDataController.globalSave.oneTimes[8] = true;
      
      SceneManager.LoadScene("Title Screen");
      
      yield return null;
   }

   IEnumerator shiftDrake(){ // Function to shift drake around
      drakeTransform.position = new Vector3(1.9f,0.44f,0);
      yield return new WaitForSeconds(0.2f);
      drakeTransform.position = new Vector3(1.75f,0.44f,0);
      yield return null;
   }

   void loadEnemy(){
      enemyObj = new Enemy(enemyName); // <------ This is where I left off
   }

   void recieveHit(){ //Going to recieve a hit and lower the health bar accordingly
      //Play hit sound
      //Shift drake quickly
      StartCoroutine(shiftDrake());
      hitSoundEffect.Play();
      enemyHealth--;
      updateSlider();
   }


   
   
   
}
