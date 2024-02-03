using UnityEngine;

namespace Domain.Entities
{
    public sealed class PlayerController : MonoBehaviour
    {
        public SubjectA subjectA;

        private void Start()
        {
            subjectA = gameObject.GetComponent<SubjectA>();
        }

        private void FixedUpdate()
        {
            if (Input.GetButton("Horizontal"))
            {
                transform.position = subjectA.movement.MovementOnXAxis(transform.position, subjectA.movementSpeed, (int)Input.GetAxisRaw("Horizontal"));
            }
        }

        private void Update()
        {
            if (Input.GetButtonDown("Jump") && subjectA.canJump && subjectA.isGrounded)
            {
                subjectA.movement.Jump(subjectA.rigbody2D, transform.position, subjectA.jumpForce);
            }
        }
    }
}
