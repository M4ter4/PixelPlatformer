using Basic;
using Level;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        private Transform _currentCheckpoint;
        private Animator _animator;
        private Health _health;
        private LevelManager _levelManager;
        private int _lives;
        [SerializeField] private int maxLives;
        [SerializeField] private Text livesText;
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Respawn1 = Animator.StringToHash("Respawn");
        
        private void Start()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _levelManager = FindObjectOfType<LevelManager>();
            _lives = maxLives;
            UpdateText();
        }

        public void SetStartingCheckpoint(Transform spawnPoint) =>
            _currentCheckpoint = spawnPoint;

        public void Respawn()
        {
            --_lives;
            if (_lives <= 0)
            {
                _levelManager.OnGameOver();
                return;
            }
            UpdateText();
            transform.position = _currentCheckpoint.position;
            _health.Revive();
            Camera.main.GetComponent<CameraController>().Move(_currentCheckpoint.parent.localPosition);
            _currentCheckpoint.parent.gameObject.GetComponent<RoomReset>().ActivateRoom(true);
            _animator.ResetTrigger(Death);
            _animator.SetTrigger(Respawn1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Respawn"))
            {
                _currentCheckpoint = other.transform;
                other.GetComponent<Animator>().SetTrigger("Activate");
                other.GetComponent<Collider2D>().enabled = false;
            }
        }

        private void UpdateText() => livesText.text = $"Lives left: {_lives}";
    }
}
