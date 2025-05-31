using Core.Enum;
using Core.Primitive;
using Core.Rule;
using Infrastructure.Utility;
using UnityEngine;

namespace Core.Entities
{
    public class Enemy : Character
    {
        private Transform playerTransform;
        private int playerLayer;
        private bool isDisengage;
        private RaycastHit2D visionHit; //RED
        private RaycastHit2D damageRay; //BLUE
        private RaycastHit2D desingageHit; //YELLOW

        public bool isPermitedJump;
        public EnemyType enemyType;

        //TODO: deletar depois
        public float distanceVision = 5f;
        public float distanceDesingage = 2.5f;
        public float distanceAttack = 1f;
        public string enemyName;

        protected override void Start()
        {
            base.Start();
            playerTransform = GameObject.FindGameObjectWithTag(enemyName).transform;
            playerLayer = playerTransform.gameObject.layer;
        }

        protected override void Update()
        {
            base.Update();
            if (!playerTransform) return;
            var playerDirection = PlayerDirection();
            visionHit = CreateEnemyRaycast(distanceVision, playerDirection);
            desingageHit = CreateEnemyRaycast(distanceDesingage, playerDirection);
            damageRay = CreateEnemyRaycast(distanceAttack, playerDirection);

            if (!isLive) return;

            EnemyBehaviour();
            if (isDebugRaycast)
            {
                RayCastUtillity.DebugGetHitRaycast(transform.position, playerDirection, distanceVision, 0.1f, Color.red); //See player
                RayCastUtillity.DebugGetHitRaycast(transform.position, playerDirection, distanceAttack, 0, Color.blue); //Hit player
                RayCastUtillity.DebugGetHitRaycast(transform.position, playerDirection, distanceDesingage, -0.1f, Color.yellow); //Desingage player
            }
        }

        protected override void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == ("JumpTrigger"))
            {
                isGrounded = true;
                canJump = true;
                isPermitedJump = true;
            }
        }

        protected override void OnTriggerExitr2D(Collider2D collision)
        {
            if (collision.gameObject.tag == ("JumpTrigger"))
            {
                isGrounded = false;
                canJump = false;
                isPermitedJump = false;
            }
        }

        private Vector2 PlayerDirection() => playerTransform.position - transform.position;

        private int EnemyVisionOrientation(int backstep = 1) =>
            (int)Mathf.Sign(playerTransform.transform.position.x - transform.position.x) * backstep;

        private RaycastHit2D CreateEnemyRaycast(float size, Vector2 playerDirection) =>
            RayCastUtillity.GetRaycast(transform, playerDirection, size, 1 << playerLayer);

        private void EnemyBehaviour()
        {
            if (
                !CombatRule.IsEnemyStillDesingagePlayer(desingageHit, isDisengage, playerLayer)
                && CombatRule.RaycastHit(visionHit, playerLayer)
            )
            {
                isBlocking = false;
                isDisengage = false;
                movement.MovementOnXAxis(transform, movementSpeed, EnemyVisionOrientation());
            }
            if (
                CombatRule.RaycastHit(damageRay, playerLayer)
                && !CombatRule.IsEnemyStillDesingagePlayer(desingageHit, isDisengage, playerLayer)
            )
            {
                isDisengage = true;
                DoDamage(playerTransform.gameObject.GetComponent<Character>());
            }
            if (CombatRule.IsEnemyStillDesingagePlayer(desingageHit, isDisengage, playerLayer))
            {
                isBlocking = true;
                movement.MovementOnXAxis(transform, movementSpeed, EnemyVisionOrientation(-1));
            }
            if (isPermitedJump && canJump && isGrounded)
            {
                movement.Jump(rigbody2D, transform.position, jumpForce);
                isPermitedJump = false;
            }
        }
    }
}
