using Basic;
using UnityEngine;
using UnityEngine.Events;

namespace Enemies.MeleeEnemy
{
    public class MeleeEnemyPointMovement : PointMovement
    {
        [SerializeField] private UnityEvent callback;

        protected override void SetNextPoint()
        {
            base.SetNextPoint();
            callback?.Invoke();
        }

        protected override void Update()
        {
            base.Update();
            if (!HasPassedThePoint(gameObject.transform.position, CurrentPoint, SpeedVector) &&
                !Mathf.Approximately(Mathf.Sign(gameObject.transform.localScale.x), Mathf.Sign(SpeedVector.x)))
            {
                gameObject.transform.localScale = new Vector3(gameObject.transform.localScale.x*-1f, gameObject.transform.localScale.y, gameObject.transform.localScale.z);
            }
        } 
    }
}