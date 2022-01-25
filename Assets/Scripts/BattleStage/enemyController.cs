using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class enemyController : MonoBehaviour
{
   public string enemy = "Drake"; //Going to be set dynamically in the future
   public GameObject enemyVisualInfo;
   private SpriteRenderer enemySprite;
   private TextMeshProUGUI enemyNameTextBox;
   private Slider enemyHealthBar;

   void Awake(){
      enemyVisualInfo = GameObject.FindWithTag("enemyVisualInfo");
   }


   void Start(){
        loadEnemyInfo();
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

      enemySprite.sprite = charProfileImageController.enemyDictionary[enemy];
      enemyNameTextBox.text = enemy;
      enemyHealthBar.value = 0.5f;

   }

   
}
