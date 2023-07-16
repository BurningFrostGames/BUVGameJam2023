using UnityEngine;

public interface IState
{
    void Update();
    void OnEnter();
    void OnExit();
}