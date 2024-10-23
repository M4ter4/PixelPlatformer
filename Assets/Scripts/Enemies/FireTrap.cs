
using System;
using System.Collections;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] private float damage;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    [SerializeField] private AudioClip activationSound;
    
    private Animator _animator;
    private bool _isActive;
    private bool _isTriggered;
    private Health _health;

    private void Start()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if(_health is not null && _isActive )
            _health.TakeDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _health = other.GetComponent<Health>(); 
            if (!_isTriggered)
            {
                StartCoroutine(Activate());
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _health = null;
        }
    }

    private IEnumerator Activate()
    {
        _isTriggered = true;
        _animator.enabled = true;
        _animator.SetTrigger("Trigger");
        yield return new WaitForSeconds(activationDelay);
        _isActive = true;
        _animator.SetTrigger("Active");
        SoundManager.Instance.PlaySound(activationSound);
        yield return new WaitForSeconds(activeTime);
        _isActive = false;
        _isTriggered = false;
        _animator.SetTrigger("Inactive");
    }
}
