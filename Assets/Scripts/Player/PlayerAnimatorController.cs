using Basic.CharacterMovement.Jumping;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(Animator))]
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(SurfaceControl))]
    public class PlayerAnimatorController : MonoBehaviour
    {
        private Animator _animator;
        private Rigidbody2D _rigidbody;
        private SurfaceControl _surfaceControl;
        
        public readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
        public readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
        public readonly int IsGrounded = Animator.StringToHash("IsGrounded");
        public readonly int IsOnWall = Animator.StringToHash("IsOnWall");

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _rigidbody = GetComponent<Rigidbody2D>();
            _surfaceControl = GetComponent<SurfaceControl>();
        }

        private void Update()
        {
            _animator.SetFloat(HorizontalInput, Mathf.Abs(Input.GetAxis("Horizontal")));
            _animator.SetFloat(VerticalSpeed, _rigidbody.velocity.y);
            _animator.SetBool(IsGrounded, _surfaceControl.IsGrounded());
            _animator.SetBool(IsOnWall, _surfaceControl.IsOnWall());
        }

        private void OnDisable()
        {
            _animator.SetFloat(HorizontalInput, 0);
            _animator.SetFloat(VerticalSpeed, 0);
            _animator.SetBool(IsGrounded, false);
            _animator.SetBool(IsOnWall, false);
        }
    }
}