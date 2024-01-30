using Domain.Enum;
using Domain.Primitive;
using Domain.Rules;
using UnityEngine;

namespace Domain.Entities
{
    public class Enemy : Character
    {
        private Transform playerTransform;
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
            if (!CombatRules.CanHitPlayer(playerTransform, transform) && CombatRules.CanSeePlayer(playerTransform, transform))
            {
                OnChasingPlayer();
            }
            if (CombatRules.CanHitPlayer(playerTransform, transform))
            {
                Debug.Log("Enemy " + gameObject.name + " can hit Player");
                isAttacking = true;
            }
            if (CombatRules.CanDoDamage(playerTransform.gameObject.layer, gameObject.layer, isAttacking))
            {
                RecivieDamage(playerTransform.gameObject.GetComponent<SubjectA>());
                isAttacking = !isAttacking;
            }
        }

        private void OnChasingPlayer()
        {
            int visionOrientation = (int)
                Mathf.Sign(playerTransform.transform.position.x - transform.position.x);

            transform.position = movement.MovementOnXAxis(
                transform.position,
                movementSpeed,
                visionOrientation
            );
        }
    }
}
