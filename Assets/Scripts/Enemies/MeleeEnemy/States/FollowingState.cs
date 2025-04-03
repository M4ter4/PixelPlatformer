using UnityEngine;

namespace Enemies.MeleeEnemy.States
{
    public class FollowingState : MeleeEnemyState
    {
        [SerializeField] private float speed;
        public static readonly int MovingAnimation = Animator.StringToHash("Moving");

        public override void Enter()
        {
            observeTriggerHandler.SetActive(true);
            canAttackTriggerHandler.SetActive(true);
            Animator.SetTrigger(MovingAnimation);
        }

        public override void Action()
        {
            var player = Controller.LastSeenPlayer.position;
            var direction = Mathf.Sign(player.x - transform.position.x);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x)*direction, transform.localScale.y, transform.localScale.z);
            gameObject.transform.Translate(speed * Time.deltaTime * direction, 0, 0);
            if ((speed * direction > 0) ? (transform.position.x > player.x) : (transform.position.x < player.x))
            {
                Controller.ChangeState(MeleeEnemyStates.Glancing);
            }
        }

        public override void Exit(){}
    }
}