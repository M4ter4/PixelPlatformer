using Basic;
using UnityEngine;

namespace Enemies
{
    public class ContactDamage : MonoBehaviour
    {
        [SerializeField] private float damage;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag == "Player")
                other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}
