using Domain.Enum;
using Domain.Primitive;
using Domain.Rules;
using UnityEngine;

namespace Domain.Entities
{
    public class Enemy : Character
    {
        private Transform playerTransform;
        private bool isDesingage;
        public EnemyType enemyType;

        protected override void Start()
        {
            base.Start();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        }

        protected override void Update()
        {
            base.Update();

            ///TODO: Futuramente maquina de estado.
            if (
                !CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDesingage)
                && CombatRules.CanSeePlayer(playerTransform, transform)
            )
            {
                OnChasingPlayer();
            }
            if (
                CombatRules.CanHitPlayer(playerTransform.gameObject.layer, transform)
                && !CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDesingage)
            )
            {
                Debug.Log("Enemy " + gameObject.name + " can hit Player");
                isAttacking = true;
            }
            if (
                !isDesingage
                && CombatRules.CanDoDamage(
                    playerTransform.gameObject.layer,
                    gameObject.layer,
                    isAttacking
                )
            )
            {
                isAttacking = false;
                isDesingage = true;
                RecivieDamage(playerTransform.gameObject.GetComponent<SubjectA>());
            }
            if (CombatRules.IsStillDesingagePlayer(playerTransform, transform, isDesingage))
            {
                OnChasingPlayer(-1);
                isDesingage = CombatRules.IsStillDesingagePlayer(
                    playerTransform,
                    transform,
                    isDesingage
                );
            }
        }

        private void OnChasingPlayer(int desingage = 1)
        {
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
