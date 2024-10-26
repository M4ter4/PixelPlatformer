using System.Collections.Generic;
using Basic;
using UnityEngine;

namespace Player
{
    public class PlayerAttack : MonoBehaviour
    {
        private PlayerMovement _playerMovement;
        private float _attackTimer = Mathf.Infinity;
        private List<Projectile> _projectiles;
        [SerializeField] private float attackCooldown;
        [SerializeField] private Transform firePoint;
        [SerializeField] private GameObject fireballHolder;
        [SerializeField] private AudioClip fireballSound;
    
        private void Start()
        {
            _playerMovement = GetComponent<PlayerMovement>();
            _projectiles = new List<Projectile>();
            foreach (Transform projectile in fireballHolder.transform)
                _projectiles.Add(projectile.gameObject.GetComponent<Projectile>());
        }

        private void Update()
        {
            int projectileIndex =  GetFireballIndex();
            if (Input.GetMouseButton(0) &&
                _attackTimer >= attackCooldown && _playerMovement.CanAttack() && projectileIndex != -1)
            {
                Attack(projectileIndex);
                _attackTimer = 0;
            }
            else
                _attackTimer += Time.deltaTime;
        }

        private void Attack(int index)
        {
            _projectiles[index].Shoot(firePoint, (gameObject.transform.localScale.x > 0) ? 0f : 180f);
            SoundManager.Instance.PlaySound(fireballSound);
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
