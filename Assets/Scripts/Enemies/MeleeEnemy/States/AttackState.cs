using UnityEngine;

namespace Enemies.MeleeEnemy.States
{
    public class AttackState : MeleeEnemyState
    {
        [SerializeField] private GameObject swordTrigger;
        
        public static readonly int AttackAnimation = Animator.StringToHash("MeleeAttack");
        public override void Enter()
        {
            observeTriggerHandler.gameObject.SetActive(false);
            canAttackTriggerHandler.gameObject.SetActive(false);
            Animator.SetTrigger(AttackAnimation);
        }

        public override void Action() {}

        public void StartAttack() =>
            swordTrigger.SetActive(true);
        
        public void StopAttack() =>
            swordTrigger.SetActive(false);

        public void EndAnimation()
        {
            swordTrigger.SetActive(false);
            observeTriggerHandler.gameObject.SetActive(true);
            canAttackTriggerHandler.gameObject.SetActive(true);
            Controller.ChangeState(MeleeEnemyStates.Following);
        }

        public override void Exit(){}
    }
}