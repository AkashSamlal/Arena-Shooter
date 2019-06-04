using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damagePlayer : MonoBehaviour {

    public int playerHealth = 30; 
    public int damage = 10;

	void Start () {
        print(playerHealth); 	
	}

    private void OnCollisionEnter(Collision collision)
    {
       if(collision.gameObject.tag == "enemy")
        {
            print("Ouch");
        } 
    }
}
