using Basic;
using UI;
using UnityEngine;

namespace Level
{
    public class HealthCollectable : MonoBehaviour
    {
        [SerializeField] private float healAmount = 100f;
        [SerializeField] private AudioClip healSound;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Health health = other.GetComponent<Health>();
            if (health != null && health.NeedsHealing() && other.CompareTag("Player"))
            {
                health.Heal(healAmount);
                gameObject.SetActive(false);
                SoundManager.Instance.PlaySound(healSound);
            }
        }
    }
}
