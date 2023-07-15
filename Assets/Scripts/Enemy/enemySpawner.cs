using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class enemySpawner : MonoBehaviour
{

    public float spawnRadius = 7, time = 1.5f;

    public GameObject[] enemies;
    public bool ableToSpawn = true;
    public int scoreToPass = 1000;
    [SerializeField] private int playerScore;
    private Score scoreManager;

    private void Start()
    {
        
    }
    void FixedUpdate()
    {
        
     
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (this.gameObject.tag == "SpawnLine")
        {
            if (collision.CompareTag("Player"))
            {
                if (ableToSpawn)
                {
                    StartCoroutine(SpawnEnemy());
                }

                else if (ableToSpawn == false)
                {
                    Debug.Log("Not able to spawn");
                }
                
                
            }
        }
        IEnumerator SpawnEnemy()
        {
            Vector2 spawnPos = GameObject.Find("Player").transform.position;
            spawnPos += Random.insideUnitCircle.normalized * spawnRadius;
            Instantiate(enemies[Random.Range(0, enemies.Length)], spawnPos, Quaternion.identity);
            yield return new WaitForSeconds(time);
            StartCoroutine(SpawnEnemy());
        }
    }
}
