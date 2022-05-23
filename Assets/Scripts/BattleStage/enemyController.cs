using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

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
   

   void Awake(){
      enemySlider = GameObject.Find("enemyHealthDisplay").GetComponent<Slider>();
      victoryScreen = GameObject.Find("victoryScreen");
      hitSoundEffect = gameObject.GetComponent<AudioSource>();
      drakeTransform = gameObject.GetComponent<Transform>();
   }


   void Start(){
      victoryScreen.SetActive(false);
      loadEnemy();
      updateSlider();

      //Temporary
   }

   void updateSlider(){
      float newValue = enemyHealth / enemyMaxHealth;
      if(newValue <= 0){
         Debug.Log("Victory Condition");
         victoryScreen.SetActive(true);//Prob want to put this transition into a coroutine to make it a bit smoother, also remove old notes with prune prob
         enemySlider.value = newValue;
      }else{
         enemySlider.value = newValue;
      }
      
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
