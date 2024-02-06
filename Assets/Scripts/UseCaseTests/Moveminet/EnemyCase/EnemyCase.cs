using Core.Primitive;
using UnityEngine;

namespace CaseTest.Movement
{
    public class EnemyCase : Character
    {
        private int direction = 1;
        public bool canWalk;

        protected override void Start()
        {
            base.Start();
        }

        protected void FixedUpdate()
        {
            if(canWalk)
            {
                movement.MovementOnXAxis(transform, movementSpeed, direction);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.collider != null && collision.collider.gameObject.layer == LayerMask.NameToLayer("CaseTest"))
            {
                direction *= -1;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.CompareTag("JumpTrigger"))
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
            }
        }
    }

}