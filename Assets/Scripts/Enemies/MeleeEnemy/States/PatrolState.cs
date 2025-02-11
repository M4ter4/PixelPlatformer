using Basic;
using UnityEngine;

namespace Enemies.MeleeEnemy.States
{
    [RequireComponent(typeof(PointMovement))]
    public class PatrolState : MeleeEnemyState
    {
        [SerializeField] protected PointMovement pointMovement;
        
        public override void Enter()
        {
            pointMovement.enabled = true;
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