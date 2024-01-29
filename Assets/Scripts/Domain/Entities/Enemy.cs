using Domain.Enum;
using Domain.Primitive;
using Domain.Rules;
using Unity.Burst.CompilerServices;
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

            if (CombatRules.CanSeePlayer(playerTransform, transform))
            {
                OnChasingPlayer();
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
