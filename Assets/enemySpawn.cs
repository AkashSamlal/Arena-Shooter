using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemySpawn : MonoBehaviour
{
    public GameObject enemy;
    public Transform enemyPos;
    public float timer = 5.0f;


    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            InvokeRepeating("EnemySpawner", 0.5f, timer);
            Destroy(gameObject, 11);
            gameObject.GetComponent<BoxCollider>().enabled = false; 
        }
    }
    void EnemySpawner()
    {
        Instantiate(enemy, enemyPos.position, enemyPos.rotation);
    }
}
