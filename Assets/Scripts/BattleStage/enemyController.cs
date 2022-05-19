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

   void Awake(){
      enemySlider = GameObject.Find("enemyHealthDisplay").GetComponent<Slider>();
   }


   void Start(){
      loadEnemy();
      updateSlider();

      //Temporary
   }

   void updateSlider(){
      float newValue = enemyHealth / enemyMaxHealth;
      Debug.Log(newValue);
      if(newValue <= 0){
         Debug.Log("Victory Condition");
         enemySlider.value = newValue;
      }else{
         enemySlider.value = newValue;
      }
      
   }

   void loadEnemy(){
      enemyObj = new Enemy(enemyName); // <------ This is where I left off
   }

   void recieveHit(){ //Going to recieve a hit and lower the health bar accordingly
      Debug.Log("Enemy controller recieved a hit");
      enemyHealth--;
      updateSlider();


   }


   
   
   
}
