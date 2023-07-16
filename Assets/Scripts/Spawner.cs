using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class Spawner : MonoBehaviour
{
    public int numToSpawn;

    public GameObject[] enemies;
    public Transform[] spawnPoints;
    public float spawnBreakTime;
    public float spawnRadius = 4f;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Spawn());
        }
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < numToSpawn; i++)
        {
            int randomIndex = Random.Range(0, enemies.Length);
            foreach (var spawnPoint in spawnPoints)
            {
                Instantiate(enemies[randomIndex], (Vector2)spawnPoint.position + (Random.insideUnitCircle * spawnRadius), Quaternion.identity);
            }
            yield return new WaitForSeconds(spawnBreakTime);
        }
        
        Destroy(gameObject);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        foreach (var spawnPoint in spawnPoints)
        {
            Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
        }
    }
}