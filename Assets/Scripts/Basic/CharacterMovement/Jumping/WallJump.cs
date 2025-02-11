using UnityEngine;

namespace Basic.CharacterMovement.Jumping
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class WallJump : AbstractJump
    {
        [Header("WallJump")]
        [SerializeField] private float verticalImpulse;
        [SerializeField] private float horizontalImpulse;
        private Rigidbody2D _rigidbody;
        private float _gravityScale;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _gravityScale = _rigidbody.gravityScale;
        }

        public void Grapple()
        {
            _rigidbody.gravityScale = 0;
            _rigidbody.velocity = Vector2.zero;
        }

        public void UnGrapple()
        {
            _rigidbody.gravityScale = _gravityScale;
        }

        public bool TryWallJump() => TryJump();

        protected override void PerformJump()
        {
            UnGrapple();
            _rigidbody.transform.position = new Vector2(transform.position.x - 0.1f*Mathf.Sign(transform.localScale.x), transform.position.y);
            _rigidbody.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*horizontalImpulse, verticalImpulse), ForceMode2D.Impulse);
            _rigidbody.transform.localScale = new Vector3(-1f*_rigidbody.transform.localScale.x,
                _rigidbody.transform.localScale.y, _rigidbody.transform.localScale.z);
        } 
    }
}
