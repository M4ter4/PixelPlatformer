using System.Collections.Generic;
using Basic;
using Basic.Attack;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : Attacker
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject fireballHolder;
        [SerializeField] private AudioClip fireballSound;

        private float _attackTimer;
        private List<Projectile> _projectiles;
        private int _projectileIndex;
    
        private void Start()
        {
            _projectiles = new List<Projectile>();
            foreach (Transform projectile in fireballHolder.transform)
                _projectiles.Add(projectile.gameObject.GetComponent<Projectile>());
        }

        private void Update()
        {
            if (_attackTimer >= 0)
                _attackTimer -= Time.deltaTime;
        }

        private bool CanAttack()
        {
            _projectileIndex = GetFireballIndex();
            if (_attackTimer <= 0 && _projectileIndex != -1)
            {
                return true;
            }
            return false;
        }

        public override void Attack()
        {
            if (CanAttack())
            {
                _projectiles[_projectileIndex].Shoot(firePoint, (gameObject.transform.localScale.x > 0) ? 0f : 180f);
                SoundManager.Instance.PlaySound(fireballSound);
                _attackTimer = attackCooldown;
            }
        }

        private int GetFireballIndex()
        {
            for (int i = 0; i < _projectiles.Count; i++)
            {
                if (!_projectiles[i].gameObject.activeSelf)
                    return i;
            }
            return -1;
        }
    }
}
