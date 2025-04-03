using Basic;
using UnityEngine;

namespace Enemies.MeleeEnemy.States
{
    [RequireComponent(typeof(PointMovement))]
    public class PatrolState : MeleeEnemyState
    {
        [SerializeField] protected PointMovement pointMovement;
        public static readonly int MovingAnimation = Animator.StringToHash("Moving");
        
        public override void Enter()
        {
            observeTriggerHandler.SetActive(true);
            canAttackTriggerHandler.SetActive(true);
            pointMovement.enabled = true;
            Animator.SetTrigger(MovingAnimation);
        }
        
        public override void Action(){}
        public override void Exit()
        {
            if(pointMovement.enabled)
                pointMovement.enabled = false;
        }

        public void OnReachingPatrolPoint()
        {
            pointMovement.enabled = false;
            Controller.ChangeState(MeleeEnemyStates.Glancing);
        }
            
    }
}