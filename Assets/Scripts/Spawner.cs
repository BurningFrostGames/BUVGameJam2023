using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public int spawnAmount;
    public float spawnRadius;
    public float spawnCollisionCheckRadius;
    [SerializeField] private BoxCollider2D _collider2D;

    private int maximumIteration = 100;
    // Update is called once per frame
    void Start()
    {
       
    }

    void SpawnEne()
    {
        int i = 0;
               int currentLimit = 0;
               while(i < spawnAmount) 
               {
                   if (currentLimit >= maximumIteration)
                   {
                       Debug.Log("reached limit, cant spawn anymore");
                       break;
                   }
                   int randomIndex = Random.Range(0, objectsToSpawn.Length);
                   Vector2 SpawnPos = new Vector2(this.transform.position.x, this.transform.position.y) + Random.insideUnitCircle * spawnRadius;
                   if (!Physics2D.OverlapCircle(SpawnPos,spawnCollisionCheckRadius))
                   {
                       Debug.Log("able to spawn");
                       Instantiate(objectsToSpawn[randomIndex], SpawnPos, Quaternion.identity);
                       i++;
                   }
                   else
                   {
                       Debug.Log("not able to spawn");
                   }
       
                   currentLimit++;
               }
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            SpawnEne();
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, 1);
    }
}
