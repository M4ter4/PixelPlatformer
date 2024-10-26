using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        public void QuitGame() => Application.Quit();

        public void StartGame() => SceneManager.LoadScene(1);
    }
}
