using Basic;
using UnityEngine;

namespace Player
{
    [RequireComponent(typeof(ContactDamageEnabling))]
    public class PlayerProjectile : Projectile
    {
        private Animator _animator;
        private ContactDamageEnabling _contactDamageEnabling;
    
        private static readonly int Blast = Animator.StringToHash("Blast");

        private new void Awake()
        {
            base.Awake();
            _animator = GetComponent<Animator>();
            _contactDamageEnabling = GetComponent<ContactDamageEnabling>();
        }

        public override void Shoot(Transform shootPoint, float angle)
        {
            base.Shoot(shootPoint, angle);
            _contactDamageEnabling.IsEnabled = true;
        }

        private new void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag is not ("Player" or "Enemy" or "Ground" or "Door"))
                return;
            Collider.enabled = false;
            Rigidbody.velocity = Vector2.zero;
            _animator.SetTrigger(Blast);
            _contactDamageEnabling.enabled = false;
        }
    }
}
