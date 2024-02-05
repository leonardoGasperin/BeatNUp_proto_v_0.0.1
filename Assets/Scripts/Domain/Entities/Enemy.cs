using Domain.Enum;
using Domain.Primitive;
using Domain.Rules;
using UnityEngine;

namespace Domain.Entities
{
    public class Enemy : Character
    {
        private Transform playerTransform;
        private bool isDisengage;
        public bool isPermitedJump;
        public EnemyType enemyType;

        protected override void Start()
        {
            base.Start();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void Update()
        {
            base.Update();
            Vector2 directionToPlayer = playerTransform.position - transform.position;
            ///TODO: Refatorar.
            if (
                !CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDisengage)
                && CombatRules.CanSeePlayer(playerTransform, transform)
            )
            {
                isDisengage = false;
                movement.MovementOnXAxis(
                    transform,
                    movementSpeed,
                    EnemyVisionOrientation()
                );
            }
            if (
                CombatRules.CanHitPlayer(
                    playerTransform.gameObject.layer,
                    directionToPlayer,
                    transform
                ) && !CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDisengage)
            )
            {
                Debug.Log("Enemy " + gameObject.name + " can hit Player");
                isAttacking = true;
            }
            if (
                !isDisengage
                && CombatRules.CanDoDamage(
                    playerTransform.gameObject.layer,
                    gameObject.layer,
                    isAttacking
                )
            )
            {
                isAttacking = false;
                isDisengage = true;
                DoDamage(playerTransform.gameObject.GetComponent<Character>());
            }
            if (CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDisengage))
            {
                isDisengage = true;

                movement.MovementOnXAxis(
                    transform,
                    movementSpeed,
                    EnemyVisionOrientation(-1)
                );
            }
            if (isPermitedJump && canJump && isGrounded)
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
                isPermitedJump = false;
            }
        }

        private int EnemyVisionOrientation(int backstep = 1)
        {
            return (int)Mathf.Sign(playerTransform.transform.position.x - transform.position.x)
                * backstep;
        }
    }
}
