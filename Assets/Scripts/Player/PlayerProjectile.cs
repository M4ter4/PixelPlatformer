using Basic;
using UnityEngine;

namespace Player
{
    public class PlayerProjectile : Projectile
    {
        [SerializeField] private float damage;
        private Animator _animator;
    
        private static readonly int Blast = Animator.StringToHash("Blast");

        private new void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
        }
        private new void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag is not ("Player" or "Enemy" or "Ground" or "Door"))
                return;
            Collider.enabled = false;
            Rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger(Blast);
            if (other.tag == "Enemy")
                other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
