using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class self_Explode : MonoBehaviour
{
    private EnenmyAI ai_Controler;

    [SerializeField] private int timeToExplode = 3;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (ai_Controler.isInAttackRange)
        {
            timeToExplode--;
        }

        if (timeToExplode <= 0)
        {
            Debug.Log("explodeeeeee");
            Destroy(this.gameObject);
        }
    }
}
