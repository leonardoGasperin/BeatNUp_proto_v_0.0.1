using UnityEngine;

namespace Core.Repository
{
    public interface IMovementRepository
    {
        public void MovementOnXAxis(Transform transformPosition, float speed, int direction);
        public void Jump(Rigidbody2D rigidbody2D, Vector2 objectPosition, float force);
    }
}
