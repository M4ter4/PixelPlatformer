using System.Linq;
using UnityEngine;

namespace Basic
{
    [RequireComponent(typeof(Collider2D))]
    public class ContactDamage : MonoBehaviour
    {
        [SerializeField] protected string[] tagsToDamage;
        [SerializeField] protected float damage;

        protected void OnTriggerStay2D(Collider2D other)
        {
            DealDamage(other);
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            DealDamage(other);
        }

        protected virtual void DealDamage(Collider2D other)
        {
            if (tagsToDamage.Contains(other.tag))
                other.GetComponent<Health>().TakeDamage(damage);
        }
    }
}