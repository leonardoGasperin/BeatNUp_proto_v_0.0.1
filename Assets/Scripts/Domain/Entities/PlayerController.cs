﻿using Domain.Primitive;
using Domain.Rules;
using UnityEngine;

namespace Domain.Entities
{
    public sealed class PlayerController : MonoBehaviour
    {
        private Player player;
        private Character enemyTarget;
        private bool canAttack;

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
            canAttack = CombatRules.CanHitPlayer(
                LayerMask.NameToLayer("Enemy"),
                transform.right,
                transform
            );

            if (Input.GetButtonDown("Jump") && player.canJump && player.isGrounded)
            {
                player.movement.Jump(player.rigbody2D, transform.position, player.jumpForce);
            }
            if (canAttack && Input.GetButtonDown("Fire1"))
            {
                enemyTarget = player.combat.GetPlayerEnemyTarget(
                    LayerMask.NameToLayer("Enemy"),
                    transform
                );
                if (enemyTarget.isLive)
                {
                    player.DoDamage(enemyTarget);
                }
            }
        }
    }
}
