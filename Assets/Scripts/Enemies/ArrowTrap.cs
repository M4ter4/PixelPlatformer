using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attackCooldown;
    [SerializeField] private Transform firePoint;
    private float _cooldownTimer;
    
    private List<Projectile> _projectiles;
    [SerializeField] private GameObject projectileHolder;
    [SerializeField] private AudioClip projectileSound;

    private Animator _animator;
    
    private void Attack(int index)
    {
        SoundManager.Instance.PlaySound(projectileSound);
        _projectiles[index].Shoot(firePoint, gameObject.transform.parent.rotation.eulerAngles.z + 90f);
    }

    private void Start()
    {
        _animator = GetComponent<Animator>();
        _projectiles = new List<Projectile>();
        foreach (Transform projectile in projectileHolder.transform)
        {
            _projectiles.Add(projectile.gameObject.GetComponent<Projectile>());
        }
    }

    private void Update()
    {
        int index = GetProjectileIndex();
        if (_cooldownTimer >= attackCooldown && index != -1)
        {
             Attack(index);
             _animator.SetTrigger("Shoot");
             _cooldownTimer = 0f;
        }
        else
        {
            _cooldownTimer += Time.deltaTime;
        }
    }
    
    private int GetProjectileIndex()
    {
        for (int i = 0; i < _projectiles.Count; i++)
        {
            if (!_projectiles[i].gameObject.activeSelf)
            {
                return i;
            }
        }
        return -1;
    }
}
