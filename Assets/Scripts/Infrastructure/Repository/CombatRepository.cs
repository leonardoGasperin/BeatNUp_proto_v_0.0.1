using Core.Primitive;
using Core.Repository;
using Unity.Mathematics;
using UnityEngine;

namespace Infracstructure.Repository
{
    public class CombatRepository : ICombatRepository
    {
        public int TakeDamage(int healthPoint, int dmg)
        {
            return healthPoint - dmg;
        }

        public Character GetPlayerEnemyTarget(RaycastHit2D hit)
        {
            if (hit.collider != null)
            {
                return hit.collider.gameObject.GetComponent<Character>();
            }

            return null;
        }

        public int BlockingAbsorbDamage(int damage, float rate)
        {
            return (int)(damage - (damage * rate));
        }
    }
}
