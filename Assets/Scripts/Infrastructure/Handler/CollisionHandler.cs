using Core.Primitive;
using UnityEngine;

namespace Infracstructure.Handler
{
    public class CollisionHandler
    {
        private readonly Character character;

        public CollisionHandler(Character character)
        {
            this.character = character;
        }

        public void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                character.isGrounded = true;
                character.canJump = true;
            }
        }

        public void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                character.isGrounded = false;
                character.canJump = false;
            }
        }
    }
}