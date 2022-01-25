using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class enemyController : MonoBehaviour
{
   public string enemy = "Drake"; //Going to be set dynamically in the future
   private SpriteRenderer enemySprite;
   private TextMeshProUGUI enemyNameTextBox;

   void Awake(){
        loadEnemyInfo();
   }

   void loadEnemyInfo(){
      gameObject.GetComponent<SpriteRenderer>().enabled = false;
      enemySprite = GameObject.Find("enemyObject").GetComponent<SpriteRenderer>();
      enemyNameTextBox = GameObject.Find("enemyName").GetComponent<TextMeshProUGUI>();

      if(enemy != ""){
         try{
            enemySprite.sprite = charProfileImageController.enemyDictionary[enemy];
         }catch{
            Debug.Log("Failed to fetch a sprite for: " + enemy + " from the enemy dictionary");
         }
      }

      enemyNameTextBox.text = enemy;

   }

   
}
