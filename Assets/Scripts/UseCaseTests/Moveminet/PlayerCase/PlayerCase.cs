using Core.Entities;
using Core.Primitive;
using UnityEngine;

namespace CaseTest.Movement
{
    public class PlayerMovmentCase : Character
    {
        protected override void Start()
        {
            base.Start();
        }

        private void FixedUpdate()
        {
            if (Input.GetButton("Horizontal"))
            {
                movement.MovementOnXAxis(
                    transform,
                    movementSpeed,
                    (int)Input.GetAxisRaw("Horizontal")
                );
            }
        }

        protected override void Update()
        {
            if (Input.GetButtonDown("Jump") && canJump && isGrounded)
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
            }
        }
    }
}
