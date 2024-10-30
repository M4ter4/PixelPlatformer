using Basic;
using UI;
using UnityEngine;

namespace Enemies
{
    public class MeleeEnemy : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private float damage;
        [SerializeField] private float sightRange;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private AudioClip attackSound;
    
        private float _cooldownTimer = Mathf.Infinity;
        private Animator _animator;
        private BoxCollider2D _bodyCollider;
        private BoxCollider2D _swordCollider;
        private EnemyPatrol _patrol;
        private Transform _lastSeenPlayer;
        private float _attackRange = 1.5f;
        private bool _isAttacking;
        private bool _isHurt;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _patrol = GetComponent<EnemyPatrol>();
            _bodyCollider = GetComponents<BoxCollider2D>()[0];
            _swordCollider = GetComponents<BoxCollider2D>()[1];
            _swordCollider.enabled = false;
        }

        private void OnEnable()
        {
            _isHurt = false;
        }

        private void Update()
        {
            if (_isHurt)
            {
                _patrol.Stop();
                return;
            }
            Collider2D playerCollider = SeePlayer();
            if (playerCollider is not null)
            {
                _lastSeenPlayer = playerCollider.transform;
                if (_cooldownTimer >= attackCooldown && CanDamagePlayer(playerCollider.transform))
                {
                    _cooldownTimer = 0f;
                    _patrol.Stop();
                    _animator.SetTrigger("MeleeAttack");
                    _isAttacking = true;
                    SoundManager.Instance.PlaySound(attackSound);
                }
                else if(!_isAttacking)
                {
                    _cooldownTimer += Time.deltaTime;
                    _patrol.FollowEnemy(playerCollider.transform);
                }
            }
            else
            {
                _patrol.Patrol();
            }
        }

        private Collider2D SeePlayer()
        {
            RaycastHit2D hit = Physics2D.BoxCast(_bodyCollider.bounds.center, _bodyCollider.bounds.size,
                0f, Vector2.right*Mathf.Sign(transform.localScale.x),sightRange, playerLayer);
            return hit.collider;
        }

        private bool CanDamagePlayer(Transform target)
        {
            return target.position.x >= (_swordCollider.bounds.center.x - _attackRange) &&
                   target.position.x <= (_swordCollider.bounds.center.x + _attackRange);
        }

        private void StartAttack()
        {
            _swordCollider.enabled = true;
        }

        private void StopAttack()
        {
            _swordCollider.enabled = false;
            _patrol.FollowEnemy(_lastSeenPlayer);
            _isAttacking = false;
        }

        private void Hurt()
        {
            _isHurt = true;
        }

        private void UnHurt()
        {
            _isHurt = false;
        }

        private void Death()
        {
            gameObject.SetActive(false);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) 
                other.gameObject.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
