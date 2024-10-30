
using Basic;
using UnityEngine;

namespace Enemies
{
    public class EnemyPatrol : Movement
    {
        [SerializeField] private float patrolSpeed = 1f;
        [SerializeField] private float followSpeed = 3f;
        [SerializeField] private Transform leftEdge;
        [SerializeField] private Transform rightEdge;
        private Transform _pointToGo;
        private State _state;
        private bool _leftPatrol;
        private enum State
        {
            Stopped = 0,
            Patroling = 1,
            Following = 2
        }

        private void OnEnable()
        {
            _pointToGo = rightEdge;
            _leftPatrol = false;
        }

        internal void Patrol()
        {
            _state = State.Patroling;
            if(transform.position.x < leftEdge.position.x)
                _pointToGo = leftEdge;
            else if (transform.position.x > rightEdge.position.x)
                _pointToGo = rightEdge;
            else if (_leftPatrol)
                _pointToGo = leftEdge;
            else
                _pointToGo = rightEdge;
        }

        internal void FollowEnemy(Transform target)
        {
            _state = State.Following;
            _pointToGo = target;
        }

        internal void Stop()
        {
            _state = State.Stopped;
        }

        private void Update()
        {
            int direction;
            switch (_state)
            {
                case State.Patroling:
                    if (transform.position.x < leftEdge.position.x)
                    {
                        _pointToGo = rightEdge;
                        _leftPatrol = false;
                    }
                    else if (transform.position.x > rightEdge.position.x)
                    {
                        _pointToGo = leftEdge;
                        _leftPatrol = true;
                    }
                    direction = _pointToGo.position.x > transform.position.x ? 1 : -1;
                    transform.localScale = new Vector3(direction*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    transform.Translate(direction*patrolSpeed*Time.deltaTime, 0, 0);
                    break;
                case State.Following:
                    direction = _pointToGo.position.x > transform.position.x ? 1 : -1;
                    transform.localScale = new Vector3(direction*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
                    transform.Translate(direction*followSpeed*Time.deltaTime, 0, 0);
                    break;
            }
        }
    }
}
