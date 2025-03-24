using Core.Primitive;
using Core.Rule;
using Infrastructure.Utility;
using UnityEngine;

namespace Core.Entities
{
    public sealed class Player : Character
    {
        protected override void Update()
        {
            base.Update();
            if (isDebugRaycast)
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

        public void MoveHorizontal(int direction)
        {
            movement.MovementOnXAxis(transform, movementSpeed, direction);
        }

        public void TryJump()
        {
            if (canJump && isGrounded)
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
            }
        }

        public void TryAttack()
        {
            RaycastHit2D damageRay = RayCastUtillity.GetRaycast(
                transform,
                transform.position + transform.right,
                1f,
                1 << LayerMask.NameToLayer("Enemy")
            );

            bool canAttack = CombatRule.RaycastHit(damageRay, LayerMask.NameToLayer("Enemy"));

            if (canAttack)
            {
                Character enemyTarget = combat.GetPlayerEnemyTarget(damageRay);
                DoDamage(enemyTarget);
            }
        }
    }
}
