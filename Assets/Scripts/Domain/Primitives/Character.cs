using Domain.Repository;
using UnityEngine;

namespace Domain.Primitive
{
    public class Character : MonoBehaviour
    {
        public ICombatRepository combat;
        public IMovementRepository movement;
        public int healthPoint;
        public int damage;
        public float movementSpeed;
        public bool isLive;
        public bool isAttacking;
    }
}
