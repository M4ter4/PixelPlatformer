using UnityEngine;


public class PlayerMovement : Movement
{
    private Rigidbody2D _rigidbody2D;
    private Vector3 _scale;
    private Animator _animator;
    private BoxCollider2D _boxCollider2D;
    private float _horizontalInput;
    private float _coyoteCounter;
    private int _jumpCounter;
    [SerializeField] private float horizontalSpeed;
    [SerializeField] private float jumpVelocity;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private float gravityScale;
    [SerializeField] private AudioClip jumpSound;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int extraJumps;
    [SerializeField] private float wallJumpX;
    [SerializeField] private float wallJumpY;
    private static readonly int HorizontalInput = Animator.StringToHash("HorizontalInput");
    private static readonly int VerticalSpeed = Animator.StringToHash("VerticalSpeed");
    private static readonly int Grounded = Animator.StringToHash("IsGrounded");
    private static readonly int OnWall = Animator.StringToHash("IsOnWall");

    private void Start()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _scale = transform.localScale;
        _animator = GetComponent<Animator>();
        _boxCollider2D = GetComponent<BoxCollider2D>();
    }

    private void Update() // Используй модификаторы доступа для методов, большие методы разделяй
    {
        _horizontalInput = Input.GetAxis("Horizontal");
        if (_rigidbody2D.velocity.x < 0f)
            
            transform.localScale = new Vector3(-1f * _scale.x, _scale.y, _scale.z);
        else if (_rigidbody2D.velocity.x > 0f)
            transform.localScale = new Vector3(_scale.x, _scale.y, _scale.z);
        
        if(Input.GetKeyDown(KeyCode.Space))
            Jump();
        
        if(Input.GetKeyUp(KeyCode.Space) && _rigidbody2D.velocity.y > 0f)
            _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _rigidbody2D.velocity.y /2);

        if (IsOnWall() && (Mathf.Abs(_horizontalInput) < 0.1f || Mathf.Approximately(Mathf.Sign(_horizontalInput),
                Mathf.Sign(transform.localScale.x))))
        {
            _rigidbody2D.velocity = Vector2.zero;
            _rigidbody2D.gravityScale = 0f;
        }
        else
        {
            _rigidbody2D.gravityScale = gravityScale;
            _rigidbody2D.velocity = new Vector2(_horizontalInput * horizontalSpeed, _rigidbody2D.velocity.y);
            if (IsGrounded())
            {
                _coyoteCounter = coyoteTime;
                _jumpCounter = extraJumps;
            }
            else
            {
                _coyoteCounter -= Time.deltaTime;
            }
        }
        
        _animator.SetFloat(HorizontalInput, Mathf.Abs(_horizontalInput));
        _animator.SetFloat(VerticalSpeed, _rigidbody2D.velocity.y);
        _animator.SetBool(Grounded, IsGrounded());
        _animator.SetBool(OnWall, IsOnWall());
    }

    private void Jump() // Тоже разделить
    {
        if(_coyoteCounter <= 0 && !IsOnWall() && _jumpCounter <= 0)
            return;
        SoundManager.Instance.PlaySound(jumpSound);
        
        if(IsOnWall())
            WallJump();
        else
        {
            if (IsGrounded())
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
            else
            {
                if (_coyoteCounter > 0)
                    _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
                else
                {
                    if (_jumpCounter > 0)
                    {
                        _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpVelocity);
                        --_jumpCounter;
                    }
                }
            }

            _coyoteCounter = 0f;
        }
    }

    private void WallJump() => 
        _rigidbody2D.AddForce(new Vector2(-Mathf.Sign(transform.localScale.x)*wallJumpX, wallJumpY), ForceMode2D.Force);

    private bool IsGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, 
            _boxCollider2D.bounds.size, 0f, Vector2.down, 0.02f, groundLayer);
        return raycastHit2D.collider;
    }

    private bool IsOnWall()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(_boxCollider2D.bounds.center, 
            _boxCollider2D.bounds.size, 0f, 
            new Vector2(transform.localScale.x, 0), 0.02f, groundLayer);
        return raycastHit2D.collider;
    }

    public bool CanAttack() => 
        Mathf.Abs(_rigidbody2D.velocity.x) < 0.01f && IsGrounded() && !IsOnWall();
}