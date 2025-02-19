using UnityEngine;

namespace Basic.CharacterMovement.Jumping
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class SurfaceControl : MonoBehaviour
    {
        [SerializeField] BoxCollider2D bodyCollider;
        [SerializeField] private LayerMask groundLayer;
        [SerializeField] private float groundCheckDistance;
        [SerializeField] private float wallCheckDistance;
        
        public bool IsGrounded()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(bodyCollider.bounds.center, 
                bodyCollider.bounds.size, 0f, Vector2.down, groundCheckDistance, groundLayer);
            return raycastHit2D.collider;
        }
        
        public bool IsOnWall()
        {
            RaycastHit2D raycastHit2D = Physics2D.BoxCast(bodyCollider.bounds.center, 
                bodyCollider.bounds.size*0.9f, 0f, 
                new Vector2(Mathf.Sign(transform.localScale.x), 0), wallCheckDistance, groundLayer);
            return raycastHit2D.collider;
        }
    }
}
