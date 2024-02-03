using UnityEngine;

namespace Domain.Rules
{
    public static class CombatRules
    {
        public static bool CanDoDamage(int targetLayer, int characterLayer, bool isAttacking) =>
            characterLayer != targetLayer
            && (
                targetLayer == LayerMask.NameToLayer("Player")
                || targetLayer == LayerMask.NameToLayer("Enemy")
            )
            && isAttacking;

        ///TODO: refatorar tudo separar raycast das regras de combate
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
            Debug.DrawLine(
                selfPosition.position + new Vector3(0, 0.1f),
                drawEnd + new Vector2(0, 0.1f),
                Color.red
            );

            return hit.collider != null && hit.collider.gameObject.layer == targetLayer;
        }

        public static bool CanHitPlayer(LayerMask targetLayer, Vector2 rayDirection, Transform selfPosition)
        {
            int layerMask = 1 << targetLayer;
            RaycastHit2D hit = Physics2D.Raycast(
                selfPosition.position,
                rayDirection,
                1f,
                layerMask
            );
            Debug.DrawLine(
                selfPosition.position,
                (Vector2)selfPosition.position + rayDirection.normalized,
                Color.blue
            );

            return hit.collider != null && hit.collider.gameObject.layer == targetLayer;
        }

        public static bool IsStillDesingagePlayer(
            Transform targetPosition,
            Transform selfPosition,
            bool isDesingage
        )
        {
            LayerMask targetLayer = targetPosition.gameObject.layer;
            int layerMask = 1 << targetLayer;
            Vector2 directionToPlayer = targetPosition.position - selfPosition.position;
            RaycastHit2D hit = Physics2D.Raycast(
                selfPosition.position,
                directionToPlayer,
                2.5f,
                layerMask
            );
            Vector2 drawEnd = (Vector2)selfPosition.position + directionToPlayer.normalized * 2.5f;
            Debug.DrawLine(
                selfPosition.position + new Vector3(0, -0.1f),
                drawEnd + new Vector2(0, -0.1f),
                Color.yellow
            );

            return hit.collider != null
                && isDesingage
                && hit.collider.gameObject.layer == targetLayer;
        }
    }
}
