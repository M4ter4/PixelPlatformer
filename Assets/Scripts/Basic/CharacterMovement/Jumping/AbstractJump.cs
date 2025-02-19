using UnityEngine;

namespace Basic.CharacterMovement.Jumping
{
    public abstract class AbstractJump : MonoBehaviour
    {
        [Header("AbstractJump")] 
        [SerializeField] protected float coyoteTime;
        [SerializeField] protected int extraJumps;
        protected float CoyoteCounter;
        protected int ExtraJumpCounter;

        public delegate bool JumpConditionDelegate();

        protected JumpConditionDelegate JumpCondition;
        protected JumpConditionDelegate RechargeCondition;
        
        public void SetJumpCondition(JumpConditionDelegate condition)
        {
            JumpCondition = condition;
        }
        
        public void SetRechargeCondition(JumpConditionDelegate condition)
        {
            RechargeCondition = condition;
        }

        protected virtual void Update()
        {
            if (RechargeCondition())
            {
                ExtraJumpCounter = extraJumps;
                CoyoteCounter = coyoteTime;
            }
            else if (CoyoteCounter > 0)
            {
                CoyoteCounter -= Time.deltaTime;
            }
        }

        protected bool TryJump()
        {
            bool performed = false;
            if (JumpCondition() || CoyoteCounter > 0)
            {
                PerformJump();
                CoyoteCounter = -1f;
                performed = true;
            }
            else if (ExtraJumpCounter > 0)
            {
                PerformJump();
                ExtraJumpCounter--;
                performed = true;
            }
            return performed;
        }

        protected abstract void PerformJump();
    }
}