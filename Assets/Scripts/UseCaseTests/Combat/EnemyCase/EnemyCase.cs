using Core.Primitive;
using Core.Rules;
using Infrastructure.Misc;
using UnityEngine;

namespace CaseTest.Combat
{
    public class EnemyCase : Character
    {
        public string targetName;
        private Character target;
        private RaycastHit2D visionHit;
        private RaycastHit2D damageRay;
        private RaycastHit2D desingageHit;
        private int playerLayer;
        private bool isDisengage;

        public float seeSize;
        public float attackSize;
        public float desingageSize;
        public bool isPermitedJump;

        protected override void Start()
        {
            base.Start();
            target = GameObject.Find(targetName).GetComponent<Character>();
            playerLayer = target.gameObject.layer;
        }

        void FixedUpdate()
        {
            var playerDirection = PlayerDirection();
            visionHit = CreateEnemyRaycast(seeSize, playerDirection);
            desingageHit = CreateEnemyRaycast(desingageSize, playerDirection);
            damageRay = CreateEnemyRaycast(attackSize, playerDirection);

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
                DoDamage(target.gameObject.gameObject.GetComponent<Character>());
            }
            if (CombatRules.IsEnemyStillDesingagePlayer(desingageHit, isDisengage, playerLayer))
            {
                movement.MovementOnXAxis(transform, movementSpeed, EnemyVisionOrientation(-1));
            }

            if (isDebugRaycast)
            {
                DrawRaycast(playerDirection, seeSize, Color.red, 0.1f); //See player
                DrawRaycast(playerDirection, attackSize, Color.blue); //Hit player
                DrawRaycast(playerDirection, desingageSize, Color.yellow, -0.1f); //Desingage player
            }
        }

        private RaycastHit2D CreateEnemyRaycast(float size, Vector2 playerDirection) =>
            RayCastUtillity.GetRaycast(transform, playerDirection, size, 1 << playerLayer);

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

        private Vector2 PlayerDirection() =>
            target.gameObject.transform.position - transform.position;

        private int EnemyVisionOrientation(int backstep = 1) =>
            (int)Mathf.Sign(target.gameObject.transform.position.x - transform.position.x)
            * backstep;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("JumpTrigger"))
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
            }
        }
    }
}
