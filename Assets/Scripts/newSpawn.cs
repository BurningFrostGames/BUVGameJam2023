using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;
public class newSpawn : MonoBehaviour
{
    public int numToSpawn;

    public GameObject[] enemies;

    public Transform spawnPoint;

    public float spawnBreakTime;

    public float spawnRadius = 4f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
                {
                    StartCoroutine(Spawn());
                }
    }

    IEnumerator Spawn()
    {
        int randomIndex = Random.Range(0, enemies.Length);
        Instantiate(enemies[randomIndex], (Vector2)spawnPoint.position + (Random.insideUnitCircle * spawnRadius), Quaternion.identity);
        Debug.Log(i);
        yield return new WaitForSeconds(spawnBreakTime);

    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(spawnPoint.position, spawnRadius);
    }
}
