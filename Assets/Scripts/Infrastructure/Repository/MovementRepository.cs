using Domain.Repository;
using UnityEngine;

namespace Infracstructure.Repository
{
    public class MovementRepository : IMovementRepository
    {
        public Vector2 MovementOnXAxis(Vector2 transformPosition, float speed, int direction)
        {
            transformPosition.x += (speed * direction) * Time.deltaTime;
            return transformPosition;
        }
    }
}
