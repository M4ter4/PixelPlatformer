using System.Collections.Generic;
using Basic;
using Enemies.MeleeEnemy.States;
using UnityEngine;

namespace Enemies.MeleeEnemy
{
    public class MeleeEnemyController : EntityController
    {
        [Header("States")] [SerializeField] private GlancingState glancingState;
        [SerializeField] private PatrolState patrolState;
        [SerializeField] private FollowingState followingState;
        [SerializeField] private AttackState attackState;

        [Header("Triggers")] [SerializeField] protected TriggerHandler observeTriggerHandler;
        [SerializeField] protected TriggerHandler canAttackTriggerHandler;
        [SerializeField] private GameObject swordTrigger;

        [SerializeField] private MeleeEnemyState _currentState;
        private MeleeEnemyStates _currentStateEnum;
        private Dictionary<MeleeEnemyStates, MeleeEnemyState> _statesDictionary;

        public Transform LastSeenPlayer { get; set; }

        void Awake()
        {
            _statesDictionary = new Dictionary<MeleeEnemyStates, MeleeEnemyState>()
            {
                { MeleeEnemyStates.Glancing, glancingState },
                { MeleeEnemyStates.Patroling, patrolState },
                { MeleeEnemyStates.Following, followingState },
                { MeleeEnemyStates.Attacking, attackState }
            };
            observeTriggerHandler.AddListener((other) =>
            {
                if (other.CompareTag("Player"))
                {
                    LastSeenPlayer = other.transform;
                    ChangeState(MeleeEnemyStates.Following);
                }
            });
            canAttackTriggerHandler.AddListener(other =>
            {
                if (other.CompareTag("Player"))
                    ChangeState(MeleeEnemyStates.Attacking);
            });
            foreach (var state in _statesDictionary.Values)
            {
                state.Init();
                state.enabled = false;
            }


            _currentState = glancingState;
            _currentStateEnum = MeleeEnemyStates.Glancing;
        }

        private void OnEnable()
        {
            swordTrigger.SetActive(false);
            _currentState.Enter();
        }


        private void OnDisable() =>
            _currentState.Exit();


        private void Update() =>
            _currentState.Action();

        public void ChangeState(MeleeEnemyStates newEnemyState)
        {
            if (_currentStateEnum == newEnemyState)
                return;
            _currentState.Exit();
            _currentStateEnum = newEnemyState;
            _currentState = _statesDictionary[newEnemyState];
            _currentState.Enter();
        }
    }
}