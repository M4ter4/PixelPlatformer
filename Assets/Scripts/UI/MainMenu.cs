using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject mainButtons;
        [SerializeField] private GameObject selectionButtons;
        [SerializeField] private SelectionArrow selectionArrow;

        public void QuitGame() => Application.Quit();

        public void StartLevel(int levelIndex)
        {
            SceneManager.LoadScene(levelIndex);
            Time.timeScale = 1;
        }

        public void SwitchMainMenuAndLevelSelection(bool mainMenu)
        {
            if (mainMenu)
            {
                mainButtons.SetActive(true);
                selectionButtons.SetActive(false);
                selectionArrow.UpdateOptions(mainButtons);
            }
            else
            {
                mainButtons.SetActive(false);
                selectionButtons.SetActive(true);
                selectionArrow.UpdateOptions(selectionButtons);
            }
        }
    }
}