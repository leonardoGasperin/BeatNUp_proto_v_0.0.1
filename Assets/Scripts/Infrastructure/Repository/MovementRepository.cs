using Domain.Repository;
using UnityEngine;

namespace Infracstructure.Repository
{
    public class MovementRepository : IMovementRepository
    {
        ///TODO: refatorar
        public void MovementOnXAxis(Transform transformPosition, float speed, int direction)
        {
            transformPosition.Translate(Vector3.right * (speed * direction) * Time.deltaTime);
        }

        public void Jump(Rigidbody2D rigidbody2D, Vector2 objectPosition, float force)
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, force));
        }
    }
}
