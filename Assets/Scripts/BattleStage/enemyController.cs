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

   void Awake(){
      enemySlider = GameObject.Find("enemyHealthDisplay").GetComponent<Slider>();
   }


   void Start(){
      loadEnemy();
      enemySlider.value = 0.75f;

      //Temporary
   }

   void loadEnemy(){
      enemyObj = new Enemy(enemyName); // <------ This is where I left off
   }


   
   
   
}
