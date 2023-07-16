using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idle : IState
{
    public void Update()
    {
        
    }

    public void OnEnter()
    {
        Debug.Log("Start idling");
    }

    public void OnExit()
    {
        Debug.Log("Stop idling");
    }
}
