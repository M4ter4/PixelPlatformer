using UI;
using UnityEngine;

namespace Enemies
{
    public class SpikeHead : MonoBehaviour
    {
        [SerializeField] private float speed;
        [SerializeField] private float range;
        [SerializeField] private float checkDelay;
        [SerializeField] private LayerMask playerLayer;
        [SerializeField] private AudioClip spikeSound;
    
        private Vector3 _destination;
        private bool _isAttacking;
        private float _checkTimer;
        private Vector3[] _directions = new Vector3[4];

        void Start() =>
            SetDirections();

        private void OnEnable() =>
            Stop();
        

        void Update()
        {
            if(_isAttacking)
                transform.Translate(speed * Time.deltaTime*_destination);
            else
            {
                _checkTimer += Time.deltaTime;
                if (_checkTimer >= checkDelay)
                {
                    CheckForPlayer();
                }
            }
        }

        private void CheckForPlayer()
        {
            for (int i = 0; i < _directions.Length; i++)
            {
                RaycastHit2D hit = Physics2D.Raycast(transform.position, _directions[i], range, playerLayer);

                if (hit.collider && !_isAttacking)
                {
                    _isAttacking = true;
                    _destination = _directions[i];
                    _checkTimer = 0;
                }
            }
        }

        private void SetDirections()
        {
            _directions[0] = transform.right;
            _directions[1] = transform.right * (-1f);
            _directions[2] = transform.up ;
            _directions[3] = transform.up * (-1f);
        }

        private void Stop()
        {
            _destination = transform.position;
            _isAttacking = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            SoundManager.Instance.PlaySound(spikeSound);
            if (!(other.CompareTag("Player") || other.CompareTag("Projectile")))
            {
                Stop();
            }
        }
    }
}
