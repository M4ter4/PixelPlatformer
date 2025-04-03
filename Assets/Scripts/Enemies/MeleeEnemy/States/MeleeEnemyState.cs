using UnityEngine;

namespace Enemies.MeleeEnemy.States
{
    public abstract class MeleeEnemyState : MonoBehaviour
    {
        [SerializeField] protected GameObject observeTriggerHandler;
        [SerializeField] protected GameObject canAttackTriggerHandler;
        
        protected MeleeEnemyController Controller;
        protected Animator Animator;
        
        public void Init()
        {
            Controller = GetComponent<MeleeEnemyController>();
            print(Controller);
            Animator = GetComponent<Animator>();
        }

        public abstract void Enter();

        public abstract void Action();

        public abstract void Exit();
    }
}