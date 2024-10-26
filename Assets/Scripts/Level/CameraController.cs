using UnityEngine;

namespace Level
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float smoothTime;
        private Vector3 _target;
        private Vector3 _velocity = Vector3.zero;

        private void Update()
        {
            transform.position = Vector3.SmoothDamp(transform.position,
                new Vector3(_target.x, _target.y, transform.position.z),
                ref _velocity, smoothTime);
        }

        public void Move(Vector3 target) => _target = target;
    }
}
