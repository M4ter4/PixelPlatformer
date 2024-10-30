using Basic;
using Level;
using UI;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class PlayerRespawn : MonoBehaviour
    {
        private Transform _currentCheckpoint;
        private Animator _animator;
        private Health _health;
        private UIManager _uiManager;
        private int _lives;
        [SerializeField] private Transform startCheckpoint;
        [SerializeField] private int maxLives;
        [SerializeField] private Text livesText;
        private static readonly int Death = Animator.StringToHash("Death");
        private static readonly int Respawn1 = Animator.StringToHash("Respawn");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _health = GetComponent<Health>();
            _currentCheckpoint = startCheckpoint;
            _uiManager = FindObjectOfType<UIManager>();
            _lives = maxLives;
            UpdateText();
        }

        public void Respawn()
        {
            --_lives;
            if (_lives <= 0)
            {
                _uiManager.GameOver();
                return;
            }
            UpdateText();
            transform.position = _currentCheckpoint.position;
            _health.Revive();
            Camera.main.GetComponent<CameraController>().Move(_currentCheckpoint.parent.position);
            _currentCheckpoint.parent.gameObject.GetComponent<RoomReset>().ActivateRoom(true);
            _animator.ResetTrigger(Death);
            _animator.SetTrigger(Respawn1);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Checkpoint"))
            {
                _currentCheckpoint = other.transform;
                other.GetComponent<Animator>().SetTrigger("Activate");
                other.GetComponent<Collider2D>().enabled = false;
            }
        }

        private void UpdateText() => livesText.text = $"Lives left: {_lives}";
    }
}
