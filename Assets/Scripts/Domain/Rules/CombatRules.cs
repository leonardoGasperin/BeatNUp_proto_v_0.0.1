using UnityEngine;

namespace Domain.Rules
{
    public static class CombatRules
    {
        public static bool CanDoDamage(int targetLayer, int characterLayer, bool isAttacking) =>
            characterLayer != targetLayer && targetLayer == LayerMask.NameToLayer("Player")
            || targetLayer == LayerMask.NameToLayer("Enemy") && isAttacking;

        public static bool CanSeePlayer(Transform targetPosition, Transform selfPosition)
        {
            LayerMask targetLayer = targetPosition.gameObject.layer;
            int layerMask = 1 << targetLayer;
            Vector2 directionToPlayer = targetPosition.position - selfPosition.position;
            Vector2 drawEnd = (Vector2)selfPosition.position + directionToPlayer.normalized * 5f;
            RaycastHit2D hit = Physics2D.Raycast(
                selfPosition.position,
                directionToPlayer,
                5f,
                layerMask
            );
            Debug.DrawLine(selfPosition.position, drawEnd, Color.red);

            return hit.collider != null && hit.collider.gameObject.layer == targetLayer;
        }
    }
}
