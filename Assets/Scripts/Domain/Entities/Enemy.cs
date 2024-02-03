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

        private void FixedUpdate()
        {
            ///TODO: Refatorar.
            if (
                !CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDisengage)
                && CombatRules.CanSeePlayer(playerTransform, transform)
            )
            {
                OnChasingPlayer();
            }
            if (
                CombatRules.CanHitPlayer(playerTransform.gameObject.layer, transform)
                && !CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDisengage)
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
                DoDamage(playerTransform.gameObject.GetComponent<SubjectA>());
            }
            if (CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDisengage))
            {
                OnChasingPlayer(-1);
                isDisengage = CombatRules.IsStillDesingagePlayer(
                    playerTransform,
                    transform,
                    isDisengage
                );
            }
            if (isPermitedJump && canJump && isGrounded)
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
                isPermitedJump = false;
            }
        }

        private void OnChasingPlayer(int desingage = 1)
        {
            ///TODO: Refatorar.
            int visionOrientation =
                (int)Mathf.Sign(playerTransform.transform.position.x - transform.position.x)
                * desingage;

            transform.position = movement.MovementOnXAxis(
                transform.position,
                movementSpeed,
                visionOrientation
            );
        }
    }
}
