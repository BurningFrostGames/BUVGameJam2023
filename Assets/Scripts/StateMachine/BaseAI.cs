using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EnemySystem
{
    public abstract class BaseAI : MonoBehaviour
    {
        protected StateMachine _stateMachine;

        protected IState initialState;
        
        private void Awake()
        {
            _stateMachine = new StateMachine();
            SetupStateMachine();
        }
        
        private void Update()
        {
            _stateMachine.Update();
            OnStateMachineUpdate();
        }
        
        /// <summary>
        /// Use this method to setup any related states and transition.
        /// </summary>
        protected virtual void SetupStateMachine() { }
        protected virtual void OnStateMachineStart() { }
        protected virtual void OnStateMachineUpdate() { }

        protected virtual void OnEnable()
        {
            SetState(initialState);
        }

        protected virtual void OnDisable()
        {
            Stop();
        }
        
        public void SetState(IState state)
        {
            if (!_stateMachine.IsStarted) 
                OnStateMachineStart();
            
            _stateMachine.SetState(state);
        }
        
        public IEnumerator SetStateWithDelay(IState state, float delay)
        {
            yield return new WaitForSeconds(delay);
            SetState(state);
        }

        protected void AddAnyTransition(IState to, Func<bool> condition) =>
            _stateMachine.AddAnyTransition(to, condition);
        
        protected void AddTransition(IState from, IState to, Func<bool> condition) =>
            _stateMachine.AddTransition(from, to, condition);

        public void SetPause(bool isPaused) => _stateMachine.SetPause(isPaused);

        public void Stop() => _stateMachine.Stop();

        public bool IsStateMachineStarted() { return _stateMachine.IsStarted; }
    }
}
