using UnityEngine;

namespace Basic
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(BoxCollider2D))] 
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float projectileSpeed;
        protected Rigidbody2D Rigidbody;
        protected BoxCollider2D Collider;
        protected float Lifetime;
    
        protected void Awake()
        {
            Rigidbody = GetComponent<Rigidbody2D>();
            Collider = GetComponent<BoxCollider2D>();
        }

        private void Update()
        {
            Lifetime += Time.deltaTime;
            if(Lifetime >= 10f)
                Deactivate();
        }

        public virtual void Shoot(Transform shootPoint, float angle)
        {
            gameObject.SetActive(true);
            Collider.enabled = true;
            gameObject.transform.position = shootPoint.position;
            gameObject.transform.rotation =  Quaternion.Euler(0, 0, angle);
            Rigidbody.velocity = new Vector2
                (projectileSpeed*Mathf.Cos(Mathf.Deg2Rad*angle), projectileSpeed*Mathf.Sin(Mathf.Deg2Rad*angle));
        
            Lifetime = 0f;
        }

        protected void OnTriggerEnter2D(Collider2D other)
        {
            if (other.tag is "Player" or "Enemy" or "Ground" or "Door" or "Trap") 
                Deactivate();
        }

        protected void Deactivate() => gameObject.SetActive(false);
    }
}
