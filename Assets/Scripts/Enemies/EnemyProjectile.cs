using Basic;
using UnityEngine;

namespace Enemies
{
    public class EnemyProjectile : Projectile
    {
        private new void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            // Collider.enabled = false;
            // Rigidbody.velocity = Vector2.zero;
        }
    }
}
