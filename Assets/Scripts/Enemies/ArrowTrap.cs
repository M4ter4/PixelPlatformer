using System.Collections.Generic;
using Basic;
using UI;
using UnityEngine;

namespace Enemies
{
    public class ArrowTrap : MonoBehaviour
    {
        [SerializeField] private float attackCooldown;
        [SerializeField] private Transform firePoint;
        private float _cooldownTimer;
    
        private List<Projectile> _projectiles;
        [SerializeField] private GameObject projectileHolder;
        [SerializeField] private AudioClip projectileSound;

        private Animator _animator;
        private static readonly int Shoot = Animator.StringToHash("Shoot");

        private void Attack(int index)
        {
            SoundManager.Instance.PlaySound(projectileSound);
            _projectiles[index].Shoot(firePoint, gameObject.transform.parent.rotation.eulerAngles.z + 90f);
        }

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _projectiles = new List<Projectile>();
            foreach (Transform projectile in projectileHolder.transform)
                _projectiles.Add(projectile.gameObject.GetComponent<Projectile>());
        }

        private void OnEnable()
        {
            foreach (var arrow in _projectiles)
            {
                arrow.gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            int index = GetProjectileIndex();
            if (_cooldownTimer >= attackCooldown && index != -1)
            {
                Attack(index);
                _animator.SetTrigger(Shoot);
                _cooldownTimer = 0f;
            }
            else
                _cooldownTimer += Time.deltaTime;
        }
    
        private int GetProjectileIndex()
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
