using UnityEngine;

namespace Basic
{
    public class ContactDamageEnabling : ContactDamage
    {
        public bool IsEnabled{get; set;}

        protected override void DealDamage(Collider2D other)
        {
            if(IsEnabled)
                base.DealDamage(other);
        }
    }
}