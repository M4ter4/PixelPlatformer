using System.Collections;
using Basic;
using UI;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(ContactDamageEnabling))]
    public class FireTrap : MonoBehaviour
    {
        [SerializeField] private float activationDelay;
        [SerializeField] private float activeTime;
        [SerializeField] private AudioClip activationSound;
    
        private Animator _animator;
        private ContactDamageEnabling _contactDamage;
        private bool _isTriggered;

        private void Awake()
        {
            _animator = GetComponent<Animator>();
            _contactDamage = GetComponent<ContactDamageEnabling>();
            _contactDamage.IsEnabled = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag is "Player" && !_isTriggered)
                StartCoroutine(Activate());
            
        }

        private IEnumerator Activate()
        {
            _isTriggered = true;
            _animator.enabled = true;
            _animator.SetTrigger("Trigger");
            yield return new WaitForSeconds(activationDelay);
            _contactDamage.IsEnabled = true;
            _animator.SetTrigger("Active");
            SoundManager.Instance.PlaySound(activationSound);
            yield return new WaitForSeconds(activeTime);
            _contactDamage.IsEnabled = false;
            _isTriggered = false;
            _animator.SetTrigger("Inactive");
        }
    }
}
