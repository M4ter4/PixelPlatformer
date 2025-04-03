using UnityEngine;

namespace Basic
{
    [RequireComponent(typeof(Collider2D))]
    public class TriggerHandler : MonoBehaviour
    {
        public delegate void TriggerHandledDelegate(GameObject other);
        private event TriggerHandledDelegate OnTriggerHandledEvent;

        private void OnTriggerEnter2D(Collider2D other) =>
            OnTriggerHandledEvent?.Invoke(other.gameObject);
        
        private void OnTriggerStay2D(Collider2D other) =>
            OnTriggerHandledEvent?.Invoke(other.gameObject);
    
        public void AddListener(TriggerHandledDelegate handler) => 
            OnTriggerHandledEvent += handler;
    
        public void RemoveListener(TriggerHandledDelegate handler) =>
            OnTriggerHandledEvent -= handler;
    }
}