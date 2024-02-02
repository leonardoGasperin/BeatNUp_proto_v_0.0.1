using Domain.Repository;
using UnityEngine;

namespace Infracstructure.Repository
{
    public class MovementRepository : IMovementRepository
    {
        public void Jump(Rigidbody2D rigidbody2D, Vector2 objectPosition, float force)
        {
            rigidbody2D.AddForce(new Vector2(rigidbody2D.velocity.x, force));
        }

        public Vector2 MovementOnXAxis(Vector2 transformPosition, float speed, int direction)
        {
            transformPosition.x += (speed * direction) * Time.deltaTime;
            return transformPosition;
        }
    }
}
