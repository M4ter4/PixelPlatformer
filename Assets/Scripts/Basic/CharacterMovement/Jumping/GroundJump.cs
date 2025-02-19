using UnityEngine;

namespace Basic.CharacterMovement.Jumping
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class GroundJump : AbstractJump
    {
        [Header("GroundJump")]
        [SerializeField] private float verticalSpeed;
        [SerializeField] private float maxAngle;
        private Rigidbody2D _rigidbody;
        private float _horizontalInput;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
        }

        public bool TryGroundJump(float horizontalInput)
        {
            _horizontalInput = horizontalInput;
            return TryJump();
        }

        protected override void PerformJump()
        {
            float angle = 90f - (maxAngle * _horizontalInput);
            Vector2 jumpImpulse = new Vector2(verticalSpeed * Mathf.Cos(angle * Mathf.Deg2Rad),
                verticalSpeed * Mathf.Sin(angle * Mathf.Deg2Rad));
            _rigidbody.velocity = jumpImpulse;
        }
    }
}