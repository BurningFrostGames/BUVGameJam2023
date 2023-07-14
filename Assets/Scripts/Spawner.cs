using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    public GameObject[] objectsToSpawn;
    public Vector2 _BoundMin;
    public Vector2 _BoundMax;
    public int spawnAmount;
    public float spawnRadius;
    public float spawnCollisionCheckRadius;
    [SerializeField] private BoxCollider2D _collider2D;

    private int maximumIteration = 100;
    // Update is called once per frame
    void Start()
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
            Vector2 SpawnPos = new Vector2(Random.Range(_BoundMin.x, _BoundMax.x), Random.Range(_BoundMin.y, _BoundMax.y)) + Random.insideUnitCircle * spawnRadius;
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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
       Gizmos.DrawWireSphere(transform.position, 1);
    }
}
