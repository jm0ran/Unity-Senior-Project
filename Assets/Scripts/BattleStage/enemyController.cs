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
   private Transform enemyObjTransform;
   private GameObject rootLayer;
   private GameObject actionTextLayer;
   private GameObject buttonLayers;

   void Awake(){
      enemyVisualInfo = GameObject.FindWithTag("enemyVisualInfo");
      enemyObjTransform = gameObject.GetComponent<Transform>();
      rootLayer = GameObject.Find("rootLayer");
      actionTextLayer = GameObject.Find("actionTextLayer");
      buttonLayers = GameObject.Find("ButtonLayers");
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
      enemyHealthBar.value = enemyObj.currentHealth / enemyObj.maxHealth;
   }

   public void recieveDamage(Move recievedMove){
      StartCoroutine(damageProcess(recievedMove));
      
      
      
   }

   IEnumerator damageProcess(Move recievedMove){
      buttonLayers.SendMessage("menuTransition", actionTextLayer);

      yield return new WaitForSeconds(0.3f);
      enemyObj.currentHealth -= recievedMove.damage;
      updateHealth();
      enemyObjTransform.position = new Vector3(enemyObjTransform.position.x + 0.25f, enemyObjTransform.position.y, enemyObjTransform.position.z);
      yield return new WaitForSeconds(0.1f);
      enemyObjTransform.position = new Vector3(enemyObjTransform.position.x - 0.5f, enemyObjTransform.position.y, enemyObjTransform.position.z);
      yield return new WaitForSeconds(0.1f);
      enemyObjTransform.position = new Vector3(enemyObjTransform.position.x + 0.25f, enemyObjTransform.position.y, enemyObjTransform.position.z);
      yield return new WaitForSeconds(0.5f);
      
      //This is where I want to trigger a dialougue loop prob or possibly call another function to handle dialougue as a universal

      buttonLayers.SendMessage("menuTransition", rootLayer);
      
   }
   
}
