using Domain.Primitive;
using Domain.Repository;
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
    }
}
