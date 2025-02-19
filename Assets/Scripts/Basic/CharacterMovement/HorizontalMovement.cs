using Basic.CharacterMovement.Jumping;
using UnityEngine;

namespace Basic.CharacterMovement
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))]
    public class HorizontalMovement : MonoBehaviour
    {
        [Header("Movement")]
        [SerializeField] private float speed;
        [SerializeField] private float airAccelerationCoefficient;
        [SerializeField] private LayerMask terrainLayer;
        private Rigidbody2D _rigidbody;
        private SurfaceControl _surfaceControl;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody2D>();
            _surfaceControl = GetComponent<SurfaceControl>();
        }

        public void Move(float horizontalInput)
        {
            Vector2 velocity;
            if(_surfaceControl.IsGrounded() || _surfaceControl.IsOnWall())
                velocity = new Vector2(horizontalInput * speed, _rigidbody.velocity.y);
            else
            {
                float limit = Mathf.Sign(transform.localScale.x) * speed;
                float calcVelocityX = _rigidbody.velocity.x + horizontalInput * speed * airAccelerationCoefficient;
                float speedX = (Mathf.Abs(limit) <= Mathf.Abs(calcVelocityX)) ? limit : calcVelocityX;
                velocity = new Vector2(speedX, _rigidbody.velocity.y);
            }
            _rigidbody.velocity = velocity;
        }
    }
}