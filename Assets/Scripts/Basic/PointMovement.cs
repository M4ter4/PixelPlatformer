
using UnityEngine;

namespace Basic
{
    public class PointMovement : MonoBehaviour
    {
        [SerializeField] private Transform[] points;
        [SerializeField] private float moveSpeed;
        private Vector3 _currentPoint;
        private Vector2 _speedVector;
        private int _currentPointIndex;
    
        private void Awake()
        {
            gameObject.transform.position = points[0].position;
            _currentPoint = points[1].position;
            _currentPointIndex = 1;
            _speedVector = CalculateSpeedVector(points[0].position, points[1].position);
        }

        private void Update()
        {
            if (HasPassedThePoint(gameObject.transform.position, _currentPoint, _speedVector))
            {
                SetNextPoint();
                _speedVector = CalculateSpeedVector(gameObject.transform.position, _currentPoint);
            }
            else
            {
                gameObject.transform.Translate(_speedVector * Time.deltaTime);
            }
        }

        private void SetNextPoint()
        {
            _currentPointIndex = (_currentPointIndex + 1) % points.Length;
            _currentPoint = points[_currentPointIndex].position;
        }

        private Vector2 CalculateSpeedVector(Vector3 from, Vector3 to)
        {
            float angle = Mathf.Atan2(to.y-from.y, to.x-from.x);
            Vector2 speedVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * moveSpeed;
            return speedVector;
        }

        private bool HasPassedThePoint(Vector3 current, Vector3 to, Vector2 speedVector)
        {
            bool passedX = (speedVector.x > 0) ? (current.x > to.x) : (current.x < to.x);
            bool passedY = (speedVector.y > 0) ? (current.y > to.y) : (current.y < to.y);
            
            return (passedX || passedY);
        }
    }
}
