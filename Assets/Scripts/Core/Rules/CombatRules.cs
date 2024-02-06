using UnityEngine;

namespace Core.Rules
{
    public static class CombatRules
    {
        public static bool RaycastHit(RaycastHit2D hit, int targetLayer) =>
            hit.collider != null && hit.collider.gameObject.layer == targetLayer;

        public static bool IsEnemyStillDesingagePlayer(
            RaycastHit2D hit,
            bool isDesingage,
            int targetLayer
        ) => hit.collider != null && isDesingage && hit.collider.gameObject.layer == targetLayer;
    }
}
