using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerProjectile : Projectile
{
    [SerializeField] private float damage;
    private Animator _animator;

    private new void Awake()
    {
        base.Awake();
        _animator = GetComponent<Animator>();
    }
    private new void OnTriggerEnter2D(Collider2D other)
    {
        if (!(other.tag == "Player" || other.tag == "Enemy" || other.tag == "Ground" || other.tag == "Door"))
            return;
        _collider.enabled = false;
        _rigidbody.velocity = Vector2.zero;
        _animator.SetTrigger("Blast");
        if(other.tag == "Enemy")
            other.GetComponent<Health>().TakeDamage(damage);
    }
}
