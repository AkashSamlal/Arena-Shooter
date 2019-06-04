using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement; 

public class GlobalHealth : MonoBehaviour {
    public static int PlayerHealth = 100;
    public int InternalHealth;
   

     void Update()
    {
        InternalHealth = PlayerHealth;
      
        if(PlayerHealth == 0)
        {
            SceneManager.LoadScene(1); 
        }
    }
}
