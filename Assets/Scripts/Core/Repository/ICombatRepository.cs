using Core.Primitive;
using UnityEngine;

namespace Core.Repository
{
    public interface ICombatRepository
    {
        public int TakeDamage(int healthPoint, int dmg);
        public Character GetPlayerEnemyTarget(RaycastHit2D hit);
        public int BlockingAbsorbDamage(int damage, float rate);
    }
}
