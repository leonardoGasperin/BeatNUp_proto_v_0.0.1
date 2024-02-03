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

        public Character GetPlayerEnemyTarget(LayerMask targetLayer, Transform selfPosition)
        {
            int layerMask = 1 << targetLayer;
            RaycastHit2D hit = Physics2D.Raycast(
                selfPosition.position,
                selfPosition.right,
                1f,
                layerMask
            );

            if(hit.collider != null)
            {
                return hit.collider.gameObject.GetComponent<Character>();
            }

            return null;
        }
    }
}
