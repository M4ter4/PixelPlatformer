
using UnityEngine;

namespace Enemies
{
    public class HorizontalMovement : MonoBehaviour
    {
        [SerializeField] private float moveDistance;
        [SerializeField] private float moveSpeed;
        private float _leftBound;
        private float _rightBound;
        private bool _isMovingLeft;
    
        private void Start()
        {
            _leftBound = transform.position.x - moveDistance;
            _rightBound = transform.position.x + moveDistance;
        }

        private void Update()
        {
            if (_isMovingLeft)
            {
                if (transform.position.x > _leftBound)
                    transform.position = new Vector3(transform.position.x - moveSpeed*Time.deltaTime, transform.position.y, transform.position.z);
                else
                    _isMovingLeft = false;
            }
            else
            {
                if (transform.position.x < _rightBound)
                    transform.position = new Vector3(transform.position.x + moveSpeed*Time.deltaTime, transform.position.y, transform.position.z);
                else
                    _isMovingLeft = true;
            }
        }
    }
}
