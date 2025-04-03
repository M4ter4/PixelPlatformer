using System.Collections;
using UI;
using UnityEngine;

namespace Basic
{
    public class Health : MonoBehaviour
    {
        [Header("Health")]
        [SerializeField] private float maxHealth;
        [SerializeField] private bool needsToBeStopped;
        private float _health;
        protected Animator Animator;
        private bool _isDead;
        private Rigidbody2D _rigidbody2D;
        private EntityController _controller;
    
        [Header("Invulnerability")]
        [SerializeField] private bool needInvulnerability;
        [SerializeField] private float invulnerabilityTime;
        [SerializeField] private float flashTime;
        private SpriteRenderer _spriteRenderer;
        private bool _isInvulnerable;
    
        [Header("Audio")]
        [SerializeField] private AudioClip hurtSound;
        [SerializeField] private AudioClip deathSound;
    
        private static readonly int Hit = Animator.StringToHash("Hit");
        private static readonly int Death = Animator.StringToHash("Death");

        private void Awake()
        {
            _health = maxHealth;
            Animator = GetComponent<Animator>();
            _rigidbody2D = GetComponent<Rigidbody2D>();
            _spriteRenderer = GetComponent<SpriteRenderer>();
            _controller = GetComponent<EntityController>();
        }

        public void TakeDamage(float damage)
        {
            if (_isInvulnerable)
                return;
            _health = Mathf.Clamp(_health - damage, 0, maxHealth);
            if (_health > 0)
            {
                Animator.SetTrigger(Hit);
                if(needsToBeStopped)
                    _controller.enabled = false;
                if(needInvulnerability)
                    StartCoroutine(Invulnerability());
            }
            else if (!_isDead)
            {
                Die();
            }
            
            if(!_isDead)
                SoundManager.Instance.PlaySound(hurtSound);
        }

        public virtual void Die()
        {
            SoundManager.Instance.PlaySound(deathSound);
            _rigidbody2D.velocity = Vector2.zero;
            _isDead = true;
            _controller.enabled = false;
            Animator.SetTrigger(Death);
        }

        public void Heal(float heal) => 
            _health = Mathf.Clamp(_health + heal, 0, maxHealth);

        public bool NeedsHealing() => !Mathf.Approximately(_health, maxHealth);

        public float GetHealthPercentage() => _health / maxHealth;

        private IEnumerator Invulnerability()
        {
            _isInvulnerable = true;
            int flashesCount = Mathf.RoundToInt(invulnerabilityTime/flashTime);
            float actualFlashTime = (invulnerabilityTime/flashesCount)/2;
            for (int i = 0; i < flashesCount; i++)
            {
                _spriteRenderer.color = Color.black;
                yield return new WaitForSeconds(actualFlashTime);    
                _spriteRenderer.color = Color.white;
                yield return new WaitForSeconds(actualFlashTime);  
            }
            _isInvulnerable = false;
        }

        public virtual void Revive()
        {
            _isDead = false;
            _health = maxHealth;
            _controller.enabled = true;
        }

        public void ExitMovementStop() =>
            _controller.enabled = true;
        
        public void Disable() =>
            gameObject.SetActive(false);
    }
}
