using Core.Primitive;
using Infrastructure.Misc;
using UnityEngine;

namespace Core.Entities
{
    public sealed class Player : Character
    {
        protected override void Update()
        {
            base.Update();
            if(isDebugRaycast)
            {
                RayCastUtillity.DebugGetHitRaycast(
                    transform.position,
                    transform.right,
                    1f,
                    0,
                    Color.blue
                );
            }
        }
    }
}
