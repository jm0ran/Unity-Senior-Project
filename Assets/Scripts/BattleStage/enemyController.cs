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
   private TextMeshProUGUI actionMessageBox;
   private GameObject buttonLayers;

   void Awake(){
      
   }


   void Start(){
      loadEnemy();
   }

   void loadEnemy(){
      enemyObj = new Enemy(enemyName); // <------ This is where I left off
   }


   
   
   
}
