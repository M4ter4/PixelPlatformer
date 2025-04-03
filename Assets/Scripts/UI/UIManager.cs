using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class UIManager : MonoBehaviour
    {
        [SerializeField] private GameObject healthBar;
        [SerializeField] private GameObject gameOverScreen;
        [SerializeField] private GameObject levelCompletedScreen;
        [SerializeField] private GameObject pauseScreen;

        private void Start()
        {
            healthBar.SetActive(true);
            gameOverScreen.SetActive(false);
            pauseScreen.SetActive(false);
        }

        public void LevelCompleted()
        {
            healthBar.SetActive(false);
            levelCompletedScreen.SetActive(true);
        }

        public void GameOver() =>
            gameOverScreen.SetActive(true);
        

        public void RestartGame() =>
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        

        public void QuitGame() =>
            Application.Quit();
        

        public void ToMainMenu() =>
            SceneManager.LoadScene(0);
        

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if(pauseScreen.activeInHierarchy)
                    PauseGame(false);
                else
                    PauseGame(true);
            }
        }

        public void PauseGame(bool status)
        {
            pauseScreen.SetActive(status);
            if (status)
                Time.timeScale = 0;
            else
                Time.timeScale = 1;
        }

        public void ChangeSoundVolume() =>
            SoundManager.Instance.ChangeVolume(0.2f);
        
    }
}
