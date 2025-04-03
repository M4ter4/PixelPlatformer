using Basic;
using UnityEngine;

namespace Player
{
    public class PlayerHealth : Health
    {
        private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
        private static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
        private static readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        private static readonly int IsOnWall = Animator.StringToHash("IsOnWall");
        private static readonly int IsDead = Animator.StringToHash("IsDead");
        
        public override void Die()
        {
            base.Die();
            Animator.SetFloat(HorizontalInput, 0f);
            Animator.SetFloat(VerticalSpeed, 0f);
            Animator.SetBool(IsGrounded, false);
            Animator.SetBool(IsOnWall, false);
            Animator.SetBool(IsDead, true);
        }

        public override void Revive()
        {
            base.Revive();
            Animator.SetBool(IsDead, false);
        }
    }
}