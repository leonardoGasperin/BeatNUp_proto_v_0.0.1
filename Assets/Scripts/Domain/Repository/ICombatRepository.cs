using Domain.Primitive;
using UnityEngine;

namespace Domain.Repository
{
    public interface ICombatRepository
    {
        public int TakeDamage(int healthPoint, int dmg);
        public Character GetPlayerEnemyTarget(LayerMask targetLayer, Transform selfPosition);
    }
}
