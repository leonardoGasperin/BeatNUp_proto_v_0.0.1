using Core.Primitive;
using Core.Rules;
using Infrastructure.Misc;
using UnityEngine;

namespace CaseTest.Combat
{
    public class PlayerCase : Character
    {
        private RaycastHit2D damageRay;
        private int enemyLayer;

        protected override void Start()
        {
            base.Start();
            enemyLayer = LayerMask.NameToLayer("Enemy");
        }

        private void FixedUpdate()
        {
            if (Input.GetButton("Horizontal"))
            {
                movement.MovementOnXAxis(
                    transform,
                    movementSpeed,
                    (int)Input.GetAxisRaw("Horizontal")
                );
            }
        }

        protected override void Update()
        {
            base.Update();
            damageRay = RayCastUtillity.GetRaycast(transform, transform.right, 1f, 1 << enemyLayer);
            RayCastUtillity.DebugGetHitRaycast(
                transform.position,
                transform.right,
                1f,
                0,
                Color.blue
            );
            var allowAttackDamage = CombatRules.RaycastHit(damageRay, enemyLayer);

            if (allowAttackDamage && Input.GetButtonDown("Fire1"))
            {
                var target = combat.GetPlayerEnemyTarget(damageRay);
                DoDamage(target);
            }
        }
    }
}
