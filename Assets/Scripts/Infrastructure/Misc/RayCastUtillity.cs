using UnityEngine;

namespace Infrastructure.Misc
{
    public static class RayCastUtillity
    {
        public static RaycastHit2D GetHit(
            Transform selfPosition,
            Vector2 rayDirection,
            float raySize,
            int targetLayer
        )
        {
            return Physics2D.Raycast(selfPosition.position, rayDirection, raySize, targetLayer);
        }

        public static void DebugGetHitRaycast(
            Vector2 selfPosition,
            Vector2 rayDirection,
            float raySize,
            float rayPosition,
            Color color
        )
        {
            Vector2 drawEnd = selfPosition + rayDirection.normalized * raySize;
            Debug.DrawLine(
                selfPosition + new Vector2(0, rayPosition),
                drawEnd + new Vector2(0, rayPosition),
                color
            );
        }
    }
}
