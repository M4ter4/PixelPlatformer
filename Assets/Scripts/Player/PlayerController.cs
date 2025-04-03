using Basic;
using Basic.CharacterMovement;
using Basic.CharacterMovement.Jumping;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(HorizontalMovement))]
    [RequireComponent(typeof(SurfaceControl))]
    public class PlayerController : EntityController
    {
        [Header("Movement")] private HorizontalMovement _horizontalMovement;
        private Vector3 _scale;

        [Header("Jump")] 
        private GroundJump _groundJump;
        private SurfaceControl _surfaceControl;
        private WallJump _wallJump;

        [Header("Attack")] 
        private PlayerAttack _playerAttack;

        private void Awake()
        {
            _horizontalMovement = GetComponent<HorizontalMovement>();
            _scale = transform.localScale;
            _surfaceControl = GetComponent<SurfaceControl>();
            _groundJump = GetComponent<GroundJump>();
            _groundJump.SetJumpCondition(_surfaceControl.IsGrounded);
            _groundJump.SetRechargeCondition(() => _surfaceControl.IsGrounded() || _surfaceControl.IsOnWall());
            _wallJump = GetComponent<WallJump>();
            _wallJump.SetJumpCondition(_surfaceControl.IsOnWall);
            _wallJump.SetRechargeCondition(_surfaceControl.IsOnWall);
            _playerAttack = GetComponent<PlayerAttack>();
        }

        private void OnDisable() =>
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        private void Update()
        {
            float horizontal = Input.GetAxis("Horizontal");
            
            ChangeTransform(horizontal);
            
            if (_surfaceControl.IsOnWall() || _surfaceControl.IsGrounded())
                _horizontalMovement.enabled = true;

            MoveHorizontal(horizontal);

            if (Input.GetKeyDown(KeyCode.Space))
            {
                Jump(horizontal);
            }

            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _playerAttack.Attack();
            }
        }

        private void MoveHorizontal(float horizontal)
        {
            if (_surfaceControl.IsOnWall() && (Mathf.Abs(horizontal) < 0.1f || Mathf.Approximately(
                    Mathf.Sign(horizontal),
                    Mathf.Sign(transform.localScale.x))))
            {
                _wallJump.Grapple();
            }
            else
            {
                _wallJump.UnGrapple();
                _horizontalMovement.Move(horizontal);
            }
        }

        private void ChangeTransform(float horizontalInput)
        {
            if (horizontalInput < 0f)
                transform.localScale = new Vector3(-1f * _scale.x, _scale.y, _scale.z);
            else if (horizontalInput > 0f)
                transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
        }

        private void Jump(float horizontal)
        {
            if (_surfaceControl.IsOnWall() && !_surfaceControl.IsGrounded())
            {
                _wallJump.TryWallJump();
            }
            else if (!_surfaceControl.IsOnWall())
            {
                _groundJump.TryGroundJump(horizontal);
            }
        }
    }
}