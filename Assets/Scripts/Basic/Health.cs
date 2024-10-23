using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float maxHealth;
    private float _health;
    private Animator _animator;
    private bool _isDead;
    private Rigidbody2D _rigidbody2D;
    private Movement _movement;
    
    [Header("Invulnerability")]
    [SerializeField] private bool needInvulnerability;
    [SerializeField] private float invulnerabilityTime;
    [SerializeField] private float flashTime;
    private SpriteRenderer _spriteRenderer;
    private bool _isInvulnerable;
    
    [Header("Audio")]
    [SerializeField] private AudioClip hurtSound;
    [SerializeField] private AudioClip deathSound;
    private void Awake()
    {
        _health = maxHealth;
        _animator = GetComponent<Animator>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _movement = GetComponent<Movement>();
    }

    public void TakeDamage(float damage)
    {
        if (_isInvulnerable)
            return;
        _health = Mathf.Clamp(_health - damage, 0, maxHealth);
        if (_health > 0)
        {
            _animator.SetTrigger("Hit");
            if(needInvulnerability)
                StartCoroutine(Invulnerability());
        }
        else if (!_isDead)
        {
            SoundManager.Instance.PlaySound(deathSound);
            _rigidbody2D.velocity = Vector2.zero;
            _isDead = true;
            _movement.enabled = false;
            _animator.SetFloat("HorizontalInput", 0f);
            _animator.SetFloat("VerticalSpeed", 0f);
            _animator.SetBool("IsGrounded", false);
            _animator.SetBool("IsOnWall", false);
            _animator.SetTrigger("Death");
        }
            
        if(!_isDead)
            SoundManager.Instance.PlaySound(hurtSound);
    }

    public void Heal(float heal)
    {
        _health = Mathf.Clamp(_health + heal, 0, maxHealth);
    }

    public bool NeedsHealing()
    {
        return !Mathf.Approximately(_health, maxHealth);
    }

    public float GetHealthPercentage()
    {
        return _health / maxHealth;
    }

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

    public void Revive()
    {
        _isDead = false;
        _health = maxHealth;
        _movement.enabled = true;
    }
}
