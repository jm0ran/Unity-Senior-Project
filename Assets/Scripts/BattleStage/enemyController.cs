using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
   public string enemyName = "Drake"; //Going to be set dynamically in the future
   public Enemy enemyObj;
   public GameObject enemyVisualInfo;
   private SpriteRenderer enemySprite;
   private TextMeshProUGUI enemyNameTextBox;
   private Slider enemyHealthBar;

   void Awake(){
      enemyVisualInfo = GameObject.FindWithTag("enemyVisualInfo");
   }


   void Start(){
      loadEnemy();
      loadEnemyInfo();
   }

   void loadEnemy(){
      enemyObj = new Enemy(enemyName); // <------ This is where I left off
   }

   void loadEnemyInfo(){
      
      foreach (Transform child in enemyVisualInfo.transform)
      {
         switch(child.gameObject.name){
            case "enemyName":
               enemyNameTextBox = child.gameObject.GetComponent<TextMeshProUGUI>();
               break;
            case "enemyHealthSlider":
               enemyHealthBar = child.gameObject.GetComponent<Slider>();
               break;
         }
      }

      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      enemySprite = GameObject.Find("enemyObject").GetComponent<SpriteRenderer>();

      enemySprite.sprite = charProfileImageController.enemyDictionary[enemyName];
      enemyNameTextBox.text = enemyName;
      updateHealth();

   }

   void updateHealth(){ //All enemy damage calculations and stuff I want to handle in this script for better or for worse
      // enemyHealthBar.value = 
   }

   
}
