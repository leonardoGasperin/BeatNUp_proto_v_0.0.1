using Core.Primitive;
using Core.Rules;
using Infrastructure.Misc;
using UnityEngine;

namespace Core.Entities
{
    public sealed class PlayerController : MonoBehaviour
    {
        private Player player;
        private Character enemyTarget;
        private bool canAttack;
        private RaycastHit2D damageRay;

        private void Start()
        {
            player = gameObject.GetComponent<Player>();
        }

        private void FixedUpdate()
        {
            if (Input.GetButton("Horizontal"))
            {
                player.movement.MovementOnXAxis(
                    transform,
                    player.movementSpeed,
                    (int)Input.GetAxisRaw("Horizontal")
                );
            }
        }

        private void Update()
        {
            damageRay = RayCastUtillity.GetHit(
                transform,
                transform.position + transform.right,
                1f,
                1 << LayerMask.NameToLayer("Enemy")
            );
            canAttack = CombatRules.RaycastHit(damageRay, LayerMask.NameToLayer("Enemy"));

            if (Input.GetButtonDown("Jump") && player.canJump && player.isGrounded)
            {
                player.movement.Jump(player.rigbody2D, transform.position, player.jumpForce);
            }
            if (canAttack && Input.GetButtonDown("Fire1"))
            {
                enemyTarget = player.combat.GetPlayerEnemyTarget(damageRay);

                player.DoDamage(enemyTarget);
            }
        }
    }
}
