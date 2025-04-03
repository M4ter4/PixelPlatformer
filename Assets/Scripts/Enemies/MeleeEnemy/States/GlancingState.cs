using System.Collections;
using UnityEngine;

namespace Enemies.MeleeEnemy.States
{
    public class GlancingState : MeleeEnemyState
    {
        private Coroutine _glancingCoroutine;
        public static readonly int IdleAnimation = Animator.StringToHash("Idle");

        public override void Enter()
        {
            observeTriggerHandler.SetActive(true);
            canAttackTriggerHandler.SetActive(true);
            if (_glancingCoroutine is not null)
                StopCoroutine(_glancingCoroutine);
            _glancingCoroutine = StartCoroutine(Glance());
            Animator.SetTrigger(IdleAnimation);
        }

        public override void Action() {}

        private IEnumerator Glance()
        {
            transform.localScale = new Vector3((transform.localScale.x) * (-1f), transform.localScale.y,
                transform.localScale.z);
            yield return new WaitForSeconds(1f);
            transform.localScale = new Vector3((transform.localScale.x) * (-1f), transform.localScale.y,
                transform.localScale.z);
            yield return new WaitForSeconds(1f);
            Controller.ChangeState(MeleeEnemyStates.Patroling);
        }

        public override void Exit()
        {
            StopCoroutine(_glancingCoroutine);
            _glancingCoroutine = null;
        }
    }
}