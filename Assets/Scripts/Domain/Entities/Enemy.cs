using Domain.Enum;
using Domain.Primitive;
using Domain.Rules;
using Infrastructure.Misc;
using UnityEngine;

namespace Domain.Entities
{
    public class Enemy : Character
    {
        private Transform playerTransform;
        private int playerLayer;
        private bool isDisengage;
        private RaycastHit2D visionHit;
        private RaycastHit2D damageRay;
        private RaycastHit2D desingageHit;

        public bool isPermitedJump;
        public EnemyType enemyType;

        protected override void Start()
        {
            base.Start();
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            playerLayer = playerTransform.gameObject.layer;
        }

        protected override void Update()
        {
            base.Update();
            var playerDirection = PlayerDirection();
            visionHit = CreateEnemyRaycast(5f, playerDirection);
            desingageHit = CreateEnemyRaycast(2.5f, playerDirection);
            damageRay = CreateEnemyRaycast(1f, playerDirection);

            if (
                !CombatRules.IsEnemyStillDesingagePlayer(desingageHit, isDisengage, playerLayer)
                && CombatRules.RaycastHit(visionHit, playerLayer)
            )
            {
                isDisengage = false;
                movement.MovementOnXAxis(transform, movementSpeed, EnemyVisionOrientation());
            }
            if (
                CombatRules.RaycastHit(damageRay, playerLayer)
                && !CombatRules.IsEnemyStillDesingagePlayer(desingageHit, isDisengage, playerLayer)
            )
            {
                isDisengage = true;
                DoDamage(playerTransform.gameObject.GetComponent<Character>());
            }
            if (CombatRules.IsEnemyStillDesingagePlayer(desingageHit, isDisengage, playerLayer))
            {
                movement.MovementOnXAxis(transform, movementSpeed, EnemyVisionOrientation(-1));
            }
            if (isPermitedJump && canJump && isGrounded)
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
                isPermitedJump = false;
            }

            if (isDebugRaycast)
            {
                DrawRaycast(playerDirection, 5f, Color.red, 0.1f); //See player
                DrawRaycast(playerDirection, 1f, Color.blue); //Hit player
                DrawRaycast(playerDirection, 2.5f, Color.yellow, -0.1f); //Desingage player
            }
        }

        private Vector2 PlayerDirection() => playerTransform.position - transform.position;

        private int EnemyVisionOrientation(int backstep = 1) =>
            (int)Mathf.Sign(playerTransform.transform.position.x - transform.position.x) * backstep;

        private RaycastHit2D CreateEnemyRaycast(float size, Vector2 playerDirection) =>
            RayCastUtillity.GetHit(transform, playerDirection, size, 1 << playerLayer);

        private void DrawRaycast(
            Vector2 playerDirection,
            float size,
            Color color,
            float debugPosition = 0
        ) =>
            RayCastUtillity.DebugGetHitRaycast(
                transform.position,
                playerDirection,
                size,
                debugPosition,
                color
            );
    }
}
