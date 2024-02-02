using UnityEngine;

namespace Domain.Repository
{
    public interface IMovementRepository
    {
        public Vector2 MovementOnXAxis(Vector2 transformPosition, float speed, int direction);
        public void Jump(Rigidbody2D rigidbody2D, Vector2 objectPosition, float force);
    }
}
