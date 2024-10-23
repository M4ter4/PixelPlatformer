using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectile : Projectile
{
    private new void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        _collider.enabled = false;
        _rigidbody.velocity = Vector2.zero;
    }
}
