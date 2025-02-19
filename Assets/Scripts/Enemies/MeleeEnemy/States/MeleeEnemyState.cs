using Basic;
using UnityEngine;

namespace Enemies.MeleeEnemy.States
{
    public abstract class MeleeEnemyState : MonoBehaviour
    {
        [SerializeField] protected TriggerHandler observeTriggerHandler;
        [SerializeField] protected TriggerHandler canAttackTriggerHandler;
        
        protected MeleeEnemyController Controller;
        protected Animator Animator;
        
        protected virtual void Awake()
        {
            Controller = GetComponent<MeleeEnemyController>();
            Animator = GetComponent<Animator>();
            observeTriggerHandler.AddListener((other) =>
            {
                if (other.CompareTag("Player"))
                {
                    Controller.LastSeenPlayer = other.transform;
                    Controller.ChangeState(MeleeEnemyStates.Following);
                }
            });
            canAttackTriggerHandler.AddListener(other =>
            {
                if (other.CompareTag("Player"))
                    Controller.ChangeState(MeleeEnemyStates.Attacking);
            });
        }

        public abstract void Enter();

        public abstract void Action();

        public abstract void Exit();
    }
}