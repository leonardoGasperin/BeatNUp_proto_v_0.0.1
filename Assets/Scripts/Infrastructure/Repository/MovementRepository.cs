using Core.Contract;
using UnityEngine;

namespace Infracstructure.Repository
{
    public class MovementRepository : IMovementRepository
    {
        private readonly Vector2 moveAxis = Vector2.right;

        public void MovementOnXAxis(Transform transformPosition, float speed, int direction)
        {
            transformPosition.Translate(moveAxis * speed * Time.deltaTime);
            transformPosition.rotation = Quaternion.Euler(0, direction == 1 ? 0 : 180, 0);
        }

        public void Jump(Rigidbody2D rigidbody2D, Vector2 objectPosition, float force)
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.linearVelocity.x, force));
        }
    }
}
