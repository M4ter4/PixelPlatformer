using Basic;
using Player;
using UI;
using UnityEngine;

namespace Level
{
    public class LevelManager : MonoBehaviour
    {
        [SerializeField] private Transform levelStart;
        [SerializeField] private TriggerHandler levelCompletedTrigger;
        
        [SerializeField] private UIManager uiManager;
        
        private void Start()
        {
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.transform.position = levelStart.position;
            player.GetComponent<PlayerRespawn>().SetStartingCheckpoint(levelStart);
            levelCompletedTrigger.AddListener(OnLevelCompleted);
        }

        public void OnGameOver() => uiManager.GameOver();

        public void OnLevelCompleted(GameObject other)
        {
            if (!other.CompareTag("Player"))
                return;
            uiManager.LevelCompleted();
            other.GetComponent<PlayerController>().enabled = false;
            other.GetComponent<PlayerAnimatorController>().enabled = false;
        }
    }
}