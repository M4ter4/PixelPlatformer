using UnityEngine;

namespace Basic
{
    public class PointMovement : MonoBehaviour
    {
        [SerializeField] protected Transform[] points;
        [SerializeField] protected float moveSpeed;
        protected Vector3 CurrentPoint;
        protected Vector2 SpeedVector;
        protected int CurrentPointIndex;
    
        private void Awake()
        {
            gameObject.transform.position = points[0].position;
            CurrentPoint = points[1].position;
            CurrentPointIndex = 1;
            SpeedVector = CalculateSpeedVector(points[0].position, points[1].position);
        }

        protected virtual void Update()
        {
            if (HasPassedThePoint(gameObject.transform.position, CurrentPoint, SpeedVector))
            {
                SetNextPoint();
                SpeedVector = CalculateSpeedVector(gameObject.transform.position, CurrentPoint);
            }
            else
            {
                gameObject.transform.Translate(SpeedVector * Time.deltaTime);
            }
        }

        protected virtual void SetNextPoint()
        {
            CurrentPointIndex = (CurrentPointIndex + 1) % points.Length;
            CurrentPoint = points[CurrentPointIndex].position;
        }

        protected Vector2 CalculateSpeedVector(Vector3 from, Vector3 to)
        {
            float angle = Mathf.Atan2(to.y-from.y, to.x-from.x);
            Vector2 speedVector = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * moveSpeed;
            return speedVector;
        }

        protected bool HasPassedThePoint(Vector3 current, Vector3 to, Vector2 speedVector)
        {
            bool passedX = (speedVector.x > 0) ? (current.x > to.x) : (current.x < to.x);
            bool passedY = (speedVector.y > 0) ? (current.y > to.y) : (current.y < to.y);
            
            return (passedX || passedY);
        }
    }
}
