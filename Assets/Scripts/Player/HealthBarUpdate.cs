using Basic;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    public class HealthBarUpdate : MonoBehaviour
    {
        [SerializeField] private Health playerHealth;
        [SerializeField] private Image actualHealthBar;

        private void Update()
        {
            actualHealthBar.fillAmount = playerHealth.GetHealthPercentage();
        }
    }
}
