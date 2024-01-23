using UnityEngine;

namespace Domain.Rules
{
    public static class CombatRules
    {
        public static bool CanDoDamage(int targetLayer, int characterLayer, bool isAttacking)
        {
            return characterLayer != targetLayer && targetLayer == LayerMask.NameToLayer("Player")
                || targetLayer == LayerMask.NameToLayer("Enemy") && isAttacking;
        }
    }
}
