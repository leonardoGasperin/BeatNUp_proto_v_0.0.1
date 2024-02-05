using Domain.Primitive;
using UnityEngine;

namespace Domain.Repository
{
    public interface ICombatRepository
    {
        public int TakeDamage(int healthPoint, int dmg);
        public Character GetPlayerEnemyTarget(RaycastHit2D hit);
    }
}
